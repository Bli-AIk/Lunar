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
        private readonly string _sourceText;
        private int _index;
        private CancellationTokenSource? _internalCts;

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


        /// <summary>
        ///     Start the TypeWriter.
        /// </summary>
        public async Task StartAsync(bool isForce = true, CancellationToken token = default)
        {
            lock (_lock)
            {
                if (!isForce && (IsPlaying || IsPaused))
                {
                    return;
                }

                _internalCts?.Dispose();
                _internalCts = CancellationTokenSource.CreateLinkedTokenSource(token);

                State = State.Playing;
            }

            try
            {
                await Update(_internalCts.Token).ConfigureAwait(false);

                if (IsCancelled)
                {
                    return;
                }

                lock (_lock)
                {
                    State = State.Finished;
                }
            }
            finally
            {
                lock (_lock)
                {
                    _internalCts?.Dispose();
                    _internalCts = null;
                }
            }
        }

        private async Task Update(CancellationToken token)
        {
            lock (_lock)
            {
                _builder.Clear();
            }

            try
            {
                await UpdateTypewriter(token);
            }
            catch (OperationCanceledException)
            {
                lock (_lock)
                {
                    State = State.Cancelled;
                }
            }
        }

        private async Task UpdateTypewriter(CancellationToken token)
        {
            for (_index = 0; _index < _sourceText.Length; _index++)
            {
                while (IsPaused)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(50, token).ConfigureAwait(false);
                }

                lock (_lock)
                {
                    _builder.Append(_sourceText[_index]);
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