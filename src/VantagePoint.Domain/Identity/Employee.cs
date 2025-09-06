using System;
using System.Buffers;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class Employee
    : AggregateRoot {
    private PersonName _name;
    private Employee? _manager;
    private PhoneNumber? _phoneNumber;
    private Address? _address;
    private Email? _email;
    private string _position;
    private string _department;
    private DateTime? _birthDate;
    private DateTime? _hiredDate;

    public Employee(PersonName name)
        : base() {
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
        _manager = null;
        _phoneNumber = null;
        _address = null;
        _email = null;
        _position = String.Empty;
        _department = String.Empty;
        _birthDate = null;
        _hiredDate = null;
    }

    public PersonName Name => _name;
    public PhoneNumber? PhoneNumber => _phoneNumber;
    public Employee? Manager => _manager;
    public Address? Address => _address;
    public Email? Email => _email;
    public string Position => _position;
    public string Department => _department;
    public DateTime? BirthDate => _birthDate;
    public DateTime? HiredDate => _hiredDate;

    public void UpdatePersonalInfo(PersonName name, DateTime birthDate) {
        ArgumentNullException.ThrowIfNull(name);
        if (birthDate > DateTime.Now.AddYears(-16)) {
            throw new ArgumentException("The birth date is invalid, too young.");
        }

        var updated = false;
        if (_name != name) {
            _name = name;
            updated = true;
        }
        if (_birthDate != birthDate) {
            _birthDate = birthDate;
            updated = true;
        }

        if (updated) {
            Publish(EmployeeEvents.Updated);
        }
    }

    public void UpdateContactInfo(Address? address, Email? email, PhoneNumber? phoneNumber) {
        var updated = false;
        address ??= _address;
        email ??= _email;
        phoneNumber ??= _phoneNumber;

        if (_address != address) {
            _address = address;
            updated = true;
        }
        if (_email != email) {
            _email = email;
            updated = true;
        }
        if (_phoneNumber != phoneNumber) {
            _phoneNumber = phoneNumber;
            updated = true;
        }

        if (updated) {
            Publish(EmployeeEvents.Updated);
        }
    }

    public void Promote(Employee manager, string newPosition) {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentException.ThrowIfNullOrWhiteSpace(newPosition);

        _manager = manager;
        _department = manager.Department;
        _position = newPosition;
        if (!_hiredDate.HasValue) {
            _hiredDate = DateTime.Now;
        }
    }
}