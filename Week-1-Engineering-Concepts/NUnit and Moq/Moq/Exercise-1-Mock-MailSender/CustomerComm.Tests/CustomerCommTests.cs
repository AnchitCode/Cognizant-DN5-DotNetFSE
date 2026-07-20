using CustomerCommLib;
using Moq;
using NUnit.Framework;

namespace CustomerComm.Tests;

[TestFixture]
public class CustomerCommTests
{
    private Mock<IMailSender> _mockMailSender;
    private CustomerComm _customerComm;

    [SetUp]
    public void Setup()
    {
        _mockMailSender = new Mock<IMailSender>();
        _customerComm = new CustomerComm(_mockMailSender.Object);
    }

    [Test]
    public void SendMailToCustomer_ShouldReturnTrue_WhenMailIsSent()
    {
        // Arrange
        _mockMailSender
            .Setup(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        // Act
        bool result = _customerComm.SendMailToCustomer();

        // Assert
        Assert.That(result, Is.True);

        _mockMailSender.Verify(
            x => x.SendMail(It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);
    }
}
