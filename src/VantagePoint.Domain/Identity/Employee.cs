using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public sealed class Employee
    : Entity {
    private OrganizationUnit _organizationUnit;
    private PersonName _name;
    private Status _status;
    private PhoneNumber _phoneNumber;
    private Address _address;
    private Email _email;
    private JobInformation _jobInfo;
    private DateTime _birthDate;

    internal Employee(OrganizationUnit organizationUnit, PersonName name, DateTime birthDate)
        : base(Identifier.New()) {
        ArgumentNullException.ThrowIfNull(organizationUnit);
        ArgumentNullException.ThrowIfNull(name);
        _organizationUnit = organizationUnit;
        _name = name;
        _birthDate = birthDate;
        _phoneNumber = PhoneNumber.Empty;
        _address = Address.Empty;
        _email = Email.Empty;
        _jobInfo = JobInformation.Empty;
        _status = Status.Inactive;
    }

    public OrganizationUnit OrganizationUnit => _organizationUnit;
    public Status Status => _status;
    public PersonName Name => _name;
    public DateTime BirthDate => _birthDate;
    public JobInformation JobInformation => _jobInfo;
    public Address Address => _address;
    public Email Email => _email;
    public PhoneNumber PhoneNumber => _phoneNumber;

    private void EnsureBirthDate(DateTime birthDate) {
        if (birthDate > DateTime.Now.AddYears(-18)) {
            throw new DomainException("Employee must be at least 18 years old.");
        }
    }

    private void EnsureActive() {
        if (Status != Status.Active) {
            throw new DomainException("Employee must be active to perform this operation.");
        }
    }

    private void EnsureInactive() {
        if (Status != Status.Inactive) {
            throw new DomainException("Employee must be inactive to perform this operation.");
        }
    }

    private void EnsureNotTerminated() {
        if (Status == Status.Terminated) {
            throw new DomainException("Operation cannot be performed on a terminated employee.");
        }
    }

    public void UpdatePersonalInfo(PersonName name, DateTime birthDate) {
        ArgumentNullException.ThrowIfNull(name);
        EnsureBirthDate(birthDate);
        if (_name != name || _birthDate != birthDate) {
            _name = name;
            _birthDate = birthDate;
            OnDomainEventOccurred(new InformationUpdatedEvent(OrganizationUnit.Id, Id));
        }
    }

    public void UpdateJobInfo(JobInformation jobInfo) {
        ArgumentNullException.ThrowIfNull(jobInfo);
        if (_jobInfo != jobInfo) {
            _jobInfo = jobInfo;
            OnDomainEventOccurred(new InformationUpdatedEvent(OrganizationUnit.Id, Id));
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
            OnDomainEventOccurred(new InformationUpdatedEvent(OrganizationUnit.Id, Id));
        }
    }

    public void Activate() {
        EnsureInactive();
        var oldStatus = _status;
        _status = Status.Active;
        OnDomainEventOccurred(new StatusChangedEvent(Id, OrganizationUnit.Id, oldStatus, _status));
    }

    public void Deactivate() {
        EnsureActive();
        var oldStatus = _status;
        _status = Status.Inactive;
        OnDomainEventOccurred(new StatusChangedEvent(Id, OrganizationUnit.Id, oldStatus, _status));
    }

    public void Terminate() {
        EnsureNotTerminated();
        var oldStatus = _status;
        _status = Status.Terminated;
        OnDomainEventOccurred(new StatusChangedEvent(Id, OrganizationUnit.Id, oldStatus, _status));
    }

    public Team GetMyTeam() {
        EnsureActive();
        var team = OrganizationUnit.GetTeamFor(this);
        return team;
    }
}