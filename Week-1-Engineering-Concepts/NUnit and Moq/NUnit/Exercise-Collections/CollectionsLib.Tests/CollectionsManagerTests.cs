using System.Linq;
using CollectionsLib;
using NUnit.Framework;

namespace CollectionsLib.Tests;

[TestFixture]
public class CollectionsManagerTests
{
    private CollectionsManager _manager;

    [SetUp]
    public void Setup()
    {
        _manager = new CollectionsManager();
    }

    // Scenario 1
    [Test]
    public void GetEmployees_ShouldNotContainNullValues()
    {
        var employees = _manager.GetEmployees();

        CollectionAssert.AllItemsAreNotNull(employees);
    }

    // Scenario 2
    [Test]
    public void GetEmployees_ShouldContainEmployeeWithId100()
    {
        var employees = _manager.GetEmployees();

        Assert.That(
            employees.Any(e => e.Id == 100),
            Is.True);
    }

    // Scenario 3
    [Test]
    public void GetEmployees_ShouldReturnUniqueEmployees()
    {
        var employees = _manager.GetEmployees();

        CollectionAssert.AllItemsAreUnique(employees);
    }

    // Scenario 4 (Constraint Model)
    [Test]
    public void EmployeeCollections_ShouldBeEqual()
    {
        var currentEmployees = _manager.GetEmployees();
        var previousEmployees = _manager.GetEmployeesWhoJoinedInPreviousYears();

        CollectionAssert.AreEqual(currentEmployees, previousEmployees);
    }
}
