using Moq;
using NUnit.Framework;
using PlayersManagerLib;

namespace PlayerManager.Tests;

[TestFixture]
public class PlayerTests
{
    private Mock<IPlayerMapper> _mockPlayerMapper;

    [SetUp]
    public void Setup()
    {
        _mockPlayerMapper = new Mock<IPlayerMapper>();
    }

    [Test]
    public void RegisterNewPlayer_ShouldCreatePlayer_WhenNameDoesNotExist()
    {
        _mockPlayerMapper
            .Setup(x => x.IsPlayerNameExistsInDb(It.IsAny<string>()))
            .Returns(false);

        Player player = Player.RegisterNewPlayer(
            "Virat",
            _mockPlayerMapper.Object);

        Assert.That(player.Name, Is.EqualTo("Virat"));
        Assert.That(player.Age, Is.EqualTo(23));
        Assert.That(player.Country, Is.EqualTo("India"));
        Assert.That(player.NoOfMatches, Is.EqualTo(30));

        _mockPlayerMapper.Verify(
            x => x.AddNewPlayerIntoDb("Virat"),
            Times.Once);
    }
}
