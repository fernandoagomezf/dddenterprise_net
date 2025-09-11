using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed class Employee
    : AggregateRoot, IPerson {
    private PersonName _name;
    private Status _status;
    private PhoneNumber _phoneNumber;
    private Address _address;
    private Email _email;
    private JobInformation _jobInfo;
    private DateTime _birthDate;
    private EmployeeCollection _directReports;

    public Employee(PersonName name, DateTime birthDate)
        : base() {
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
        _birthDate = birthDate;
        _phoneNumber = PhoneNumber.Empty;
        _address = Address.Empty;
        _email = Email.Empty;
        _jobInfo = JobInformation.Empty;
        _status = Status.Inactive;
        _directReports = new EmployeeCollection(this);
    }

    public Status Status => _status;
    public PersonName Name => _name;
    public JobInformation JobInformation => _jobInfo;
    public Address Address => _address;
    public Email Email => _email;
    public PhoneNumber PhoneNumber => _phoneNumber;
    public Employees DirectReports => new Employees(_directReports);

    public EmployeeInfo GetInfo()
        => new EmployeeInfo(Id, Name, Status);

    public void UpdatePersonalInfo(PersonName name, DateTime birthDate) {
        ArgumentNullException.ThrowIfNull(name);
        EnsureBirthDate(birthDate);
        if (_name != name || _birthDate != birthDate) {
            _name = name;
            _birthDate = birthDate;
            Events.Add(new InformationUpdatedEvent(Id));
        }
    }

    private void EnsureBirthDate(DateTime birthDate) {
        if (birthDate > DateTime.Now.AddYears(-18)) {
            throw new DomainException("Employee must be at least 18 years old.");
        }
    }

    public void UpdateJobInfo(JobInformation jobInfo) {
        ArgumentNullException.ThrowIfNull(jobInfo);
        if (_jobInfo != jobInfo) {
            _jobInfo = jobInfo;
            Events.Add(new InformationUpdatedEvent(Id));
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
            Events.Add(new InformationUpdatedEvent(Id));
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

    public void Activate() {
        EnsureInactive();
        _status = Status.Active;
        Events.Add(new StatusChangedEvent(Id, Status.Inactive, Status.Active));
    }

    public void Deactivate() {
        EnsureActive();
        _status = Status.Inactive;
        Events.Add(new StatusChangedEvent(Id, Status.Active, Status.Inactive));
    }

    public void Terminate() {
        EnsureNotTerminated();
        _status = Status.Terminated;
        Events.Add(new StatusChangedEvent(Id, Status.Active, Status.Terminated));
    }

    public void AssignDirectReport(EmployeeInfo directReport) {
        ArgumentNullException.ThrowIfNull(directReport);
        EnsureActive();
        if (directReport.Status != Status.Active) {
            throw new DomainException("Direct report must be active.");
        }
        if (directReport.Id == Id) {
            throw new DomainException("Direct report cannot report to themselves.");
        }
        if (_directReports.Contains(directReport.Id)) {
            throw new DomainException("Direct report already reports to this employee.");
        }
        _directReports.Add(directReport);
        Events.Add(new ReportStructureChangedEvent(Id, directReport.Id));
    }

    public void DeassignDirectReport(EmployeeInfo directReport) {
        ArgumentNullException.ThrowIfNull(directReport);
        if (!_directReports.Contains(directReport.Id)) {
            throw new DomainException("This employee is not a direct report.");
        }
        _directReports.Remove(directReport);
        Events.Add(new ReportStructureChangedEvent(Id, directReport.Id));
    }
}