using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class Employee
    : AggregateRoot {
    private PersonName _name;
    private EmployeeStatus _status;
    private Manager? _manager;
    private PhoneNumber _phoneNumber;
    private Address _address;
    private Email _email;
    private string _position;
    private string _department;
    private DateTime? _birthDate;
    private DateTime? _hiredDate;
    private readonly TeamMemberCollection _teamMembers;

    public Employee(PersonName name)
        : base() {
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
        _manager = null;
        _phoneNumber = PhoneNumber.Empty;
        _birthDate = null;
        _address = Address.Empty;
        _email = Email.Empty;
        _position = String.Empty;
        _department = String.Empty;
        _hiredDate = new();
        _teamMembers = new();
        _status = EmployeeStatus.Inactive;
    }

    public EmployeeStatus Status => _status;

    public PersonName Name {
        get => _name;
        init {
            ArgumentNullException.ThrowIfNull(value);
            if (_name != value) {
                _name = value;
                Publish(EmployeeEvents.InfoUpdated);
            }
        }
    }

    public Manager? Manager => _manager;
    public string Department => _department;
    public string Position => _position;
    public ITeamMembers TeamMembers => _teamMembers;

    public DateTime? BirthDate {
        get => _birthDate;
        set {
            if (value > DateTime.Now.AddYears(-18)) {
                throw new ArgumentException("Employee must be at least 18 years old.", nameof(value));
            }
            _birthDate = value;
            Publish(EmployeeEvents.InfoUpdated);
        }
    }

    public DateTime? HiredDate {
        get => _hiredDate;
        set {
            if (value.HasValue && value > DateTime.Now) {
                throw new ArgumentException("Hired date cannot be in the future.", nameof(value));
            }
            _hiredDate = value;
            Publish(EmployeeEvents.InfoUpdated);
        }
    }

    public Address Address {
        get => _address;
        set {
            if (_address != value) {
                _address = value;
                Publish(EmployeeEvents.InfoUpdated);
            }
        }
    }

    public Email Email {
        get => _email;
        set {
            if (_email != value) {
                _email = value;
                Publish(EmployeeEvents.InfoUpdated);
            }
        }
    }

    public PhoneNumber PhoneNumber {
        get => _phoneNumber;
        set {
            if (_phoneNumber != value) {
                _phoneNumber = value;
                Publish(EmployeeEvents.InfoUpdated);
            }
        }
    }

    public void AssignManager(Manager manager) {
        ArgumentNullException.ThrowIfNull(manager);
        if (_manager != manager) {
            _manager = manager;
            Publish(EmployeeEvents.ManagerAssigned);
        }
    }

    public void AssignTeamMember(TeamMember member) {
        ArgumentNullException.ThrowIfNull(member);
        if (member.Id == Id) {
            throw new DomainException("Employee cannot be their own team member");
        }
        if (_teamMembers.Contains(member.Id)) {
            throw new DomainException("Employee is already a team member");
        }

        _teamMembers.Add(member);
        Publish(EmployeeEvents.TeamMemberAssigned);
    }

    public void RemoveTeamMember(TeamMember member) {
        ArgumentNullException.ThrowIfNull(member);
        if (!_teamMembers.Contains(member.Id)) {
            throw new DomainException("Employee is not a team member");
        }
        _teamMembers.Remove(member.Id);
        Publish(EmployeeEvents.TeamMemberRemoved);
    }

    public void Activate() {
        if (_status != EmployeeStatus.Inactive) {
            throw new DomainException("Employee must be inactive to activate.");
        }
        _status = EmployeeStatus.Active;
        if (!_hiredDate.HasValue) {
            _hiredDate = DateTime.Now;
        }
        Publish(EmployeeEvents.StatusChanged);
    }

    public void Deactivate() {
        if (_status != EmployeeStatus.Active) {
            throw new DomainException("Employee must be active to deactivate.");
        }
        if (_teamMembers.Count > 0) {
            throw new DomainException("Cannot deactivate an employee who is managing a team.");
        }
        _status = EmployeeStatus.Inactive;
        Publish(EmployeeEvents.StatusChanged);
    }

    public void Terminate() {
        if (_status == EmployeeStatus.Terminated) {
            throw new DomainException("Employee is already terminated.");
        }
        if (_status == EmployeeStatus.Active && _teamMembers.Count > 0) {
            throw new DomainException("Cannot terminate an active employee who is managing a team. Transfer the team and try again.");
        }
        _status = EmployeeStatus.Terminated;
        Publish(EmployeeEvents.StatusChanged);
    }

}