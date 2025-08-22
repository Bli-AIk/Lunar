using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lunar.Modules.TypeWriter
{
    public enum State
    {
        Idle,
        Playing,
        Paused,
        Cancelled,
        Finished
    }

    public partial class TypeWriter
    {
        private readonly StringBuilder _builder = new();
        private readonly TimeSpan _delay;
        private readonly object _lock = new();
        private CancellationTokenSource? _internalCts;
        private string _sourceText;


        public TypeWriter(string sourceText, TimeSpan delay)
        {
            _sourceText = sourceText ?? throw new ArgumentNullException(nameof(sourceText));
            _delay = delay;
            State = State.Idle;
        }

        /// <summary>
        ///     Current status of typewriter. (read-only)
        /// </summary>
        public State State { get; private set; }

        /// <summary>
        ///     The results of the text. (read-only)
        /// </summary>
        public string ResultText
        {
            get
            {
                lock (_lock)
                {
                    return _builder.ToString();
                }
            }
        }

        public async Task StartAsync(string sourceText, bool isForce = true, CancellationToken token = default)
        {
            _sourceText = sourceText;
            await StartAsync(isForce, token);
        }

        /// <summary>
        ///     Start the TypeWriter.
        /// </summary>
        public async Task StartAsync(bool isForce = true, CancellationToken token = default)
        {
            CancellationTokenSource newCts;

            lock (_lock)
            {
                if (!isForce && (IsPlaying || IsPaused))
                {
                    return;
                }

                _internalCts?.Cancel();
                _internalCts?.Dispose();

                newCts = CancellationTokenSource.CreateLinkedTokenSource(token);
                _internalCts = newCts;
                _builder.Clear();
                State = State.Playing;
            }

            try
            {
                await Update(newCts).ConfigureAwait(false);

                lock (_lock)
                {
                    if (_internalCts == newCts && State != State.Cancelled)
                    {
                        State = State.Finished;
                    }
                }
            }
            finally
            {
                lock (_lock)
                {
                    if (_internalCts == newCts)
                    {
                        _internalCts?.Dispose();
                        _internalCts = null;
                    }
                }
            }
        }

        private async Task Update(CancellationTokenSource localCts)
        {
            try
            {
                await UpdateTypewriter(localCts.Token);
            }
            catch (OperationCanceledException)
            {
                lock (_lock)
                {
                    if (_internalCts == localCts)
                    {
                        State = State.Cancelled;
                    }
                }
            }
        }

        private async Task UpdateTypewriter(CancellationToken token)
        {
            foreach (var t in _sourceText)
            {
                token.ThrowIfCancellationRequested();

                while (IsPaused)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(50, token).ConfigureAwait(false);
                }

                lock (_lock)
                {
                    _builder.Append(t);
                }

                await Task.Delay(_delay, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Pause the TypeWriter.
        /// </summary>
        public void Pause()
        {
            if (State != State.Playing)
            {
                return;
            }

            lock (_lock)
            {
                State = State.Paused;
            }
        }

        /// <summary>
        ///     Resume the TypeWriter.
        /// </summary>
        public void Resume()
        {
            if (State != State.Paused)
            {
                return;
            }

            lock (_lock)
            {
                State = State.Playing;
            }
        }

        /// <summary>
        ///     Cancel the TypeWriter.
        /// </summary>
        public void Cancel()
        {
            lock (_lock)
            {
                if (State is State.Idle or State.Finished or State.Cancelled)
                {
                    return;
                }

                State = State.Cancelled;

                _internalCts?.Cancel();
            }
        }
    }

    public partial class TypeWriter
    {
        /// <summary>
        ///     Is the typewriter playing? (read-only)
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                lock (_lock)
                {
                    return State == State.Playing;
                }
            }
        }

        /// <summary>
        ///     Is the typewriter paused? (read-only)
        /// </summary>
        public bool IsPaused
        {
            get
            {
                lock (_lock)
                {
                    return State == State.Paused;
                }
            }
        }

        /// <summary>
        ///     Is the typewriter cancelled? (read-only)
        /// </summary>
        public bool IsCancelled
        {
            get
            {
                lock (_lock)
                {
                    return State == State.Cancelled;
                }
            }
        }
    }
}