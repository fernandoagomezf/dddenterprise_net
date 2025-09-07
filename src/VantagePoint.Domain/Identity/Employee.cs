using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class Employee
    : AggregateRoot {
    private PersonName _name;
    private EmployeeStatus _status;
    private Employee? _manager;
    private PhoneNumber _phoneNumber;
    private Address _address;
    private Email _email;
    private string _position;
    private string _department;
    private DateTime _birthDate;
    private DateTime? _hiredDate;
    private EmployeeCollection _teamMembers;

    public Employee(PersonName name, DateTime birthDate)
        : base() {
        ArgumentNullException.ThrowIfNull(name);
        if (birthDate > DateTime.Now.AddYears(-18)) {
            throw new ArgumentException("Employee must be at least 18 years old.", nameof(birthDate));
        }
        _name = name;
        _manager = null;
        _phoneNumber = PhoneNumber.Empty;
        _birthDate = birthDate;
        _address = Address.Empty;
        _email = Email.Empty;
        _position = String.Empty;
        _department = String.Empty;
        _birthDate = birthDate;
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
                Publish(EmployeeEvents.Updated);
            }
        }
    }

    public Employee? Manager => _manager;
    public string Department => _department;
    public string Position => _position;

    public DateTime BirthDate {
        get => _birthDate;
        set {
            if (value > DateTime.Now.AddYears(-16)) {
                throw new ArgumentException("The birth date is invalid, too young.");
            }
            _birthDate = value;
            Publish(EmployeeEvents.Updated);
        }
    }

    public Address Address {
        get => _address;
        set {
            if (_address != value) {
                _address = value;
                Publish(EmployeeEvents.Updated);
            }
        }
    }

    public Email Email {
        get => _email;
        set {
            if (_email != value) {
                _email = value;
                Publish(EmployeeEvents.Updated);
            }
        }
    }

    public PhoneNumber PhoneNumber {
        get => _phoneNumber;
        set {
            if (_phoneNumber != value) {
                _phoneNumber = value;
                Publish(EmployeeEvents.Updated);
            }
        }
    }

    public Team GetColleagues() {
        if (_manager == null) {
            throw new InvalidOperationException("The employee does not have a manager assigned.");
        }

        var members = new EmployeeCollection(new[] { this });
        return new Team(_manager, members);
    }

    public Team GetTeam() {
        var members = new EmployeeCollection(_teamMembers);
        return new Team(this, members);
    }

    public void AssignToTeam(Employee manager, string position) {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentException.ThrowIfNullOrWhiteSpace(position);

        if (_manager != null) {
            _manager._teamMembers.Remove(this);
            Publish(EmployeeEvents.TeamMemberRemoved);
        }

        _manager = manager;
        _department = manager.Department;
        _position = position;
        _manager._teamMembers.Add(this);
        _manager.Publish(EmployeeEvents.TeamMemberAssigned);
        Publish(EmployeeEvents.AssignedToTeam);
        Publish(EmployeeEvents.Updated);
    }

    public void TransferTeamTo(Employee newManager) {
        ArgumentNullException.ThrowIfNull(newManager);
        if (_manager == null) {
            throw new DomainException("Employee does not have a manager to transfer from.");
        }
        if (_manager == newManager) {
            throw new DomainException("New manager is the same as the current manager.");
        }

        foreach (Employee member in _teamMembers) {
            member.AssignToTeam(newManager, member.Position);
        }
        _teamMembers.Clear();
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
        Publish(EmployeeEvents.Updated);
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
        Publish(EmployeeEvents.Updated);
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
        Publish(EmployeeEvents.Updated);
        Publish(EmployeeEvents.StatusChanged);
    }

}