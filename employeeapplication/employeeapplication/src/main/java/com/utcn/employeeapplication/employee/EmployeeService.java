package com.utcn.employeeapplication.employee;

import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.parsing.EmptyReaderEventListener;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class EmployeeService {

    @Autowired
    private EmployeeRepository employeeRepository;

    @Transactional
    public Employee create(Employee employee){
        return employeeRepository.save(employee);
    }

    public List<Employee> getAllEmployees(){
        return employeeRepository.findAll();
    }

    @Transactional
    public Employee getEmployee(int id) { return employeeRepository.getEmployeeByID(id); }

    @Transactional
    public Employee updateEmployeeName(int id, Employee employee)
    {
        Employee updatedEmployee = employeeRepository.findById(id).orElseThrow(() -> new EmployeeNotFoundException("Employee not found!"));
        updatedEmployee.setName(employee.getName());
        return employeeRepository.save(updatedEmployee);
    }
    @Transactional
    public Employee updateEmployeeEmail(int id, Employee employee)
    {
        Employee updatedEmployee = employeeRepository.findById(id).orElseThrow(() -> new EmployeeNotFoundException("Employee not found!"));
        updatedEmployee.setEmail(employee.getEmail());
        return employeeRepository.save(updatedEmployee);
    }

    @Transactional
    public void deleteEmployee(int id) {
        employeeRepository.deleteById(id);
    }

    @Transactional
    public List<Employee> getEmployeesByDepartment(int departmentID) {
        return employeeRepository.getEmployeesByDepartmentID(departmentID);
    }


}
