
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class EmployeeCollection
    : IEntityCollection, IEnumerable<Employee> {
    private readonly Dictionary<Guid, Employee> _employees;

    public EmployeeCollection() {
        _employees = new();
    }

    public EmployeeCollection(IEnumerable<Employee> employees) {
        ArgumentNullException.ThrowIfNull(employees);
        _employees = new(employees.ToDictionary(e => ((IEntity)e).Id));
    }

    public int Count => _employees.Count;

    public void Add(Employee employee) {
        ArgumentNullException.ThrowIfNull(employee);
        var id = ((IEntity)employee).Id;
        if (_employees.ContainsKey(id)) {
            throw new ArgumentException($"Employee with ID '{id}' already exists in the collection.");
        }
        _employees[id] = employee;
    }

    public void Remove(Employee employee) {
        ArgumentNullException.ThrowIfNull(employee);
        var id = ((IEntity)employee).Id;
        if (!_employees.ContainsKey(id)) {
            throw new ArgumentException($"Employee with ID '{id}' does not exist in the collection.");
        }
        _employees.Remove(id);
    }

    public void Clear() => _employees.Clear();

    public Employee? Find(Guid id)
        => _employees.TryGetValue(id, out var employee) ? employee : null;

    public Employee Get(Guid id) {
        var employee = Find(id);
        if (employee == null) {
            throw new ArgumentException($"Employee with ID '{id}' was not found in the collection.");
        }
        return employee;
    }

    public IEnumerator<IEntity> GetEnumerator()
        => _employees.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _employees.Values.GetEnumerator();

    IEnumerator<Employee> IEnumerable<Employee>.GetEnumerator()
        => _employees.Values.GetEnumerator();
}