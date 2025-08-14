using JetBrains.Annotations;

namespace Lunar.Modules.TypeWriter.Test;

[TestSubject(typeof(TypeWriter))]
public class TypeWriterTest
{
    [Fact]
    public async Task StartAsync_ShouldSetResultTextToSourceText_WhenCompleted() 
    {
        // Arrange
        const string sourceText = "This is a Test";
        var delay = TimeSpan.FromMilliseconds(1); 
        var typeWriter = new TypeWriter(sourceText, delay);
        
        // Act
        await typeWriter.StartAsync(); 
        
        // Assert
        Assert.Equal(typeWriter.ResultText, sourceText);
    }
}