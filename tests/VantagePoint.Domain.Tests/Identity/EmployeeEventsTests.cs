using VantagePoint.Domain.Identity;
using VantagePoint.Domain.Common;
using Xunit;

namespace VantagePoint.Domain.Identity.Tests;

public class EmployeeEventsTests {
    [Fact]
    public void UpdatedEvent_HasCorrectValues() {
        Assert.Equal("Identity", EmployeeEvents.Updated.Context);
        Assert.Equal("Employee.Updated", EmployeeEvents.Updated.Code);
    }

    [Fact]
    public void PromotedEvent_HasCorrectValues() {
        Assert.Equal("Identity", EmployeeEvents.Promoted.Context);
        Assert.Equal("Employee.Promoted", EmployeeEvents.Promoted.Code);
    }

    [Fact]
    public void StatusChangedEvent_HasCorrectValues() {
        Assert.Equal("Identity", EmployeeEvents.StatusChanged.Context);
        Assert.Equal("Employee.StatusChanged", EmployeeEvents.StatusChanged.Code);
    }

    [Fact]
    public void AssignedToTeamEvent_HasCorrectValues() {
        Assert.Equal("Identity", EmployeeEvents.AssignedToTeam.Context);
        Assert.Equal("Employee.AssignedToTeam", EmployeeEvents.AssignedToTeam.Code);
    }

    [Fact]
    public void TeamTransferredEvent_HasCorrectValues() {
        Assert.Equal("Identity", EmployeeEvents.TeamTransferred.Context);
        Assert.Equal("Employee.TeamTransferred", EmployeeEvents.TeamTransferred.Code);
    }
}
