using NUnit.Framework;
using AccountsManagerLib;
using System;

namespace AccountsManagerLib.Tests;

[TestFixture]
public class AccountManagerTests
{
    private AccountManager _accountManager;

    [SetUp]
    public void Setup()
    {
        _accountManager = new AccountManager();
    }

    [Test]
    public void Login_ValidUser1_ReturnsWelcomeMessage()
    {
        string result = _accountManager.Login("user_1", "secret@user11");

        Assert.That(result, Is.EqualTo("Welcome user_1!!!"));
    }

    [Test]
    public void Login_ValidUser2_ReturnsWelcomeMessage()
    {
        string result = _accountManager.Login("user_2", "secret@user22");

        Assert.That(result, Is.EqualTo("Welcome user_2!!!"));
    }

    [Test]
    public void Login_InvalidCredentials_ReturnsErrorMessage()
    {
        string result = _accountManager.Login("user_1", "wrongpassword");

        Assert.That(result, Is.EqualTo("Invalid user id/password"));
    }

    [Test]
    public void Login_EmptyUserId_ThrowsArgumentException()
    {
        Assert.That(
            () => _accountManager.Login("", "secret@user11"),
            Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Login_EmptyPassword_ThrowsArgumentException()
    {
        Assert.That(
            () => _accountManager.Login("user_1", ""),
            Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Login_NullUserId_ThrowsArgumentException()
    {
        Assert.That(
            () => _accountManager.Login(null, "secret@user11"),
            Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Login_NullPassword_ThrowsArgumentException()
    {
        Assert.That(
            () => _accountManager.Login("user_1", null),
            Throws.TypeOf<ArgumentException>());
    }
}
