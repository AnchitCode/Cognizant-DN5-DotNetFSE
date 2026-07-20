using MagicFilesLib;
using Moq;
using NUnit.Framework;

namespace DirectoryExplorer.Tests;

[TestFixture]
public class DirectoryExplorerTests
{
    private Mock<IDirectoryExplorer> _mockDirectoryExplorer;

    private readonly string _file1 = "file.txt";
    private readonly string _file2 = "file2.txt";

    [SetUp]
    public void Setup()
    {
        _mockDirectoryExplorer = new Mock<IDirectoryExplorer>();
    }

    [Test]
    public void GetFiles_ShouldReturnMockedFiles()
    {
        // Arrange
        var files = new List<string>
        {
            _file1,
            _file2
        };

        _mockDirectoryExplorer
            .Setup(x => x.GetFiles(It.IsAny<string>()))
            .Returns(files);

        // Act
        var result = _mockDirectoryExplorer.Object.GetFiles("C:\\Temp");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Contains(_file1), Is.True);

        _mockDirectoryExplorer.Verify(
            x => x.GetFiles(It.IsAny<string>()),
            Times.Once);
    }
}
