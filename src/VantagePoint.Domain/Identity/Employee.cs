using System;
using System.Collections.Generic;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class Employee
    : AggregateRoot {
    private PersonName _name;
    private EmployeeStatus _status;
    private Manager _manager;
    private PhoneNumber _phoneNumber;
    private Address _address;
    private Email _email;
    private JobInformation _jobInfo;
    private DateTime? _birthDate;

    public Employee(Manager manager, PersonName name)
        : base() {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
        _manager = manager;
        _phoneNumber = PhoneNumber.Empty;
        _birthDate = null;
        _address = Address.Empty;
        _email = Email.Empty;
        _jobInfo = JobInformation.Empty;
        _status = EmployeeStatus.Inactive;
    }

    public EmployeeStatus Status => _status;
    public Manager Manager => _manager;
    public Entities<TeamMember> TeamMembers => Entities.AsEntities<TeamMember>();
    public PersonName Name => _name;
    public JobInformation JobInformation => _jobInfo;
    public Address Address => _address;
    public Email Email => _email;
    public PhoneNumber PhoneNumber => _phoneNumber;

    public void UpdatePersonalInfo(PersonName name, DateTime birthDate) {
        ArgumentNullException.ThrowIfNull(name);
        if (birthDate > DateTime.Now.AddYears(-18)) {
            throw new ArgumentException("Employee must be at least 18 years old.", nameof(birthDate));
        }
        if (_name != name || _birthDate != birthDate) {
            _name = name;
            _birthDate = birthDate;
            Events.AddEvent(EmployeeEvents.InfoUpdated);
        }
    }

    public void UpdateJobInfo(JobInformation jobInfo) {
        ArgumentNullException.ThrowIfNull(jobInfo);
        if (_jobInfo != jobInfo) {
            _jobInfo = jobInfo;
            Events.AddEvent(EmployeeEvents.InfoUpdated);
        }
    }

    public void UpdateContactInfo(Address address, Email email, PhoneNumber phoneNumber) {
        ArgumentNullException.ThrowIfNull(phoneNumber);
        ArgumentNullException.ThrowIfNull(address);
        ArgumentNullException.ThrowIfNull(email);
        if (_phoneNumber != phoneNumber || _address != address || _email != email) {
            _phoneNumber = phoneNumber;
            _address = address;
            _email = email;
            Events.AddEvent(EmployeeEvents.InfoUpdated);
        }
    }

    public void AssignManager(Manager manager) {
        ArgumentNullException.ThrowIfNull(manager);
        if (!Manager.Equals(manager)) {
            _manager = manager;
            Events.AddEvent(EmployeeEvents.ManagerAssigned);
        }
    }

    public void AssignTeamMember(TeamMember member) {
        ArgumentNullException.ThrowIfNull(member);
        if (member.Id == Id) {
            throw new DomainException("Employee cannot be their own team member");
        }
        if (Entities.Contains(member.Id)) {
            throw new DomainException("Employee is already a team member");
        }

        Entities.Add(member);
        Events.AddEvent(EmployeeEvents.TeamMemberAssigned);
    }

    public void RemoveTeamMember(TeamMember member) {
        ArgumentNullException.ThrowIfNull(member);
        if (!Entities.Contains(member)) {
            throw new DomainException("Employee is not a team member");
        }
        Entities.Remove(member);
        Events.AddEvent(EmployeeEvents.TeamMemberRemoved);
    }

    public void Activate() {
        if (_status != EmployeeStatus.Inactive) {
            throw new DomainException("Employee must be inactive to activate.");
        }
        _status = EmployeeStatus.Active;
        Events.AddEvent(EmployeeEvents.StatusChanged);
    }

    public void Deactivate() {
        if (_status != EmployeeStatus.Active) {
            throw new DomainException("Employee must be active to deactivate.");
        }
        if (TeamMembers.Count > 0) {
            throw new DomainException("Cannot deactivate an employee who is managing a team.");
        }
        _status = EmployeeStatus.Inactive;
        Events.AddEvent(EmployeeEvents.StatusChanged);
    }

    public void Terminate() {
        if (_status == EmployeeStatus.Terminated) {
            throw new DomainException("Employee is already terminated.");
        }
        if (_status == EmployeeStatus.Active && TeamMembers.Count > 0) {
            throw new DomainException("Cannot terminate an active employee who is managing a team. Transfer the team and try again.");
        }
        _status = EmployeeStatus.Terminated;
        Events.AddEvent(EmployeeEvents.StatusChanged);
    }
}