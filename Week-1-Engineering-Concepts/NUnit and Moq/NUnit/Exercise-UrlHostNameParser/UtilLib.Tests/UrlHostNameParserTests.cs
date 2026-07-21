using NUnit.Framework;
using UtilLib;

namespace UtilLib.Tests;

[TestFixture]
public class UrlHostNameParserTests
{
    private UrlHostNameParser _parser;

    [SetUp]
    public void Setup()
    {
        _parser = new UrlHostNameParser();
    }

    [Test]
    public void ParseHostName_ValidUrl_ReturnsHostName()
    {
        // Arrange
        string url = "https://www.google.com/search";

        // Act
        string hostName = _parser.ParseHostName(url);

        // Assert
        Assert.That(hostName, Is.EqualTo("www.google.com"));
    }

    [Test]
    public void ParseHostName_InvalidUrl_ReturnsEmptyString()
    {
        // Arrange
        string url = "invalid-url";

        // Act
        string hostName = _parser.ParseHostName(url);

        // Assert
        Assert.That(hostName, Is.EqualTo(string.Empty));
    }
}
