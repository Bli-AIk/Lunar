namespace Lunar.Interfaces;

public interface ILogger
{
    void Log(object message, object? context = null);
    void LogWarning(object message, object? context = null);
    void LogError(object message, object? context = null);
}