using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;

namespace Lunar.Modules.TypeWriter.Test;

[TestSubject(typeof(TypeWriter))]
public class TypeWriterTest
{
    private const string SourceText = "This is a Test";
    private readonly TimeSpan _delay = TimeSpan.FromMilliseconds(1);

    [Fact]
    public async Task StartAsync_ShouldSetResultTextToSourceText_WhenCompleted()
    {
        // Arrange
        var typeWriter = new TypeWriter(SourceText, _delay);

        // Act
        await typeWriter.StartAsync();

        // Assert
        typeWriter.ResultText.Should().Be(SourceText);
        typeWriter.State.Should().Be(State.Finished);
    }


    [Fact]
    public async Task StartAsync_ShouldHandleFrequentCalls_ByRestarting()
    {
        // Arrange
        var typeWriter = new TypeWriter(SourceText, _delay);

        // Act
        for (var i = 0; i < 100; i++)
        {
            if (i != 99)
            {
                _ = typeWriter.StartAsync();
                await Task.Delay(50);
            }
            else
            {
                await typeWriter.StartAsync();
            }
        }


        typeWriter.ResultText.Should().Be(SourceText);
        typeWriter.State.Should().Be(State.Finished);
    }


    [Fact]
    public async Task StartAsync_ShouldHandleFrequentCallsWithDifferentText()
    {
        // Arrange
        var typeWriter = new TypeWriter("Initial text", _delay);
        var cts = new CancellationTokenSource();

        // Act
        await typeWriter.StartAsync("First call", true, cts.Token);
        await typeWriter.StartAsync("Second call", true, cts.Token);
        await typeWriter.StartAsync("Final and correct text", true, cts.Token);

        // Assert
        typeWriter.ResultText.Should().Be("Final and correct text");
        typeWriter.State.Should().Be(State.Finished);
    }


    [Fact]
    public async Task PauseAndResume_ShouldPauseTypingAndContinueAfterResume()
    {
        // Arrange
        var typeWriter = new TypeWriter(SourceText, TimeSpan.FromMilliseconds(50));
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        
        // Act
        var startTask = typeWriter.StartAsync(token: cts.Token);

        await Task.Delay(100, cts.Token);
        typeWriter.Pause();

        typeWriter.State.Should().Be(State.Paused);
        var pausedResult = typeWriter.ResultText;
        pausedResult.Length.Should().BeLessThan(SourceText.Length);

        await Task.Delay(200, cts.Token);
        typeWriter.ResultText.Should().Be(pausedResult); 

        typeWriter.Resume();

        await startTask;

        // Assert
        typeWriter.ResultText.Should().Be(SourceText);
        typeWriter.State.Should().Be(State.Finished);
    }


    [Fact]
    public async Task Cancel_ShouldStopTypingAndSetStateToCancelled()
    {
        // Arrange
        var typeWriter = new TypeWriter(SourceText, TimeSpan.FromMilliseconds(50));
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)); 

        // Act
        var startTask = typeWriter.StartAsync(token: cts.Token);

        await Task.Delay(100, cts.Token);

        typeWriter.Cancel();

        await startTask;

        // Assert
        typeWriter.IsCancelled.Should().BeTrue();
        typeWriter.ResultText.Length.Should().BeLessThan(SourceText.Length);
    }


    [Fact]
    public async Task StartAsync_ShouldNotStartIfPlayingAndIsForceIsFalse()
    {
        // Arrange
        var typeWriter = new TypeWriter(SourceText, _delay);
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        // Act
        var firstTask = typeWriter.StartAsync(token: cts.Token);

        await Task.Delay(10, cts.Token);
        await typeWriter.StartAsync(false, cts.Token);

        await firstTask;

        // Assert
        typeWriter.ResultText.Should().Be(SourceText);
        typeWriter.State.Should().Be(State.Finished);
    }
}