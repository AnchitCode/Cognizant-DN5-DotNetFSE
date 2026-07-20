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
}
