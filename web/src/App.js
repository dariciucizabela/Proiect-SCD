import React, { useState } from 'react';
import axios from 'axios';
import './App.css'; // Add your CSS file for styling

function App() {
    const [employees, setEmployees] = useState([]);
    const [departments, setDepartments] = useState([]);
    const [employeeId, setEmployeeId] = useState('');
    const [newEmployeeName, setNewEmployeeName] = useState('');
    const [departmentId, setDepartmentId] = useState('');
    const [newDepartmentName, setNewDepartmentName] = useState('');

    const fetchEmployees = async () => {
        try {
            const response = await axios.get('http://localhost:8083/api/employee/getAll');
            setEmployees(response.data);
        } catch (error) {
            console.error('Error fetching employees:', error);
        }
    };

    const fetchDepartments = async () => {
        try {
            const response = await axios.get('http://localhost:8083/api/department/getAll');
            setDepartments(response.data);
        } catch (error) {
            console.error('Error fetching departments:', error);
        }
    };

    const handleEmployeeNameChange = async () => {
        try {
            const response = await axios.put(`http://localhost:8083/api/employee/updateName/${employeeId}`, {
                name: newEmployeeName,
            });
            console.log('Employee name updated:', response.data);
            fetchEmployees();
        } catch (error) {
            console.error('Error updating employee name:', error);
        }
    };

    const handleDepartmentNameChange = async () => {
        try {
            const response = await axios.put(`http://localhost:8083/api/department/update/${departmentId}`, {
                name: newDepartmentName,
            });
            console.log('Department name updated:', response.data);
            fetchDepartments();
        } catch (error) {
            console.error('Error updating department name:', error);
        }
    };

    return (
        <div className="App">
            <div className="buttons">
                <button onClick={fetchEmployees}>Fetch Employees</button>
                <button onClick={fetchDepartments}>Fetch Departments</button>
            </div>

            <div className="data-section">
                <div className="list">
                    <h2>Employees</h2>
                    <ul>
                        {employees.map((employee) => (
                            <li key={employee.id}>
                                ID: {employee.id}, Name: {employee.name}, Email: {employee.email}
                            </li>
                        ))}
                    </ul>
                </div>

                <div className="list">
                    <h2>Departments</h2>
                    <ul>
                        {departments.map((department) => (
                            <li key={department.id}>ID: {department.id}, Name: {department.name}</li>
                        ))}
                    </ul>
                </div>
            </div>

            <div className="update-section">
                <div className="update-form">
                    <h3>Change Employee Name</h3>
                    <input
                        type="text"
                        placeholder="Employee ID"
                        value={employeeId}
                        onChange={(e) => setEmployeeId(e.target.value)}
                    />
                    <input
                        type="text"
                        placeholder="New Name"
                        value={newEmployeeName}
                        onChange={(e) => setNewEmployeeName(e.target.value)}
                    />
                    <button onClick={handleEmployeeNameChange}>Change Name</button>
                </div>

                <div className="update-form">
                    <h3>Change Department Name</h3>
                    <input
                        type="text"
                        placeholder="Department ID"
                        value={departmentId}
                        onChange={(e) => setDepartmentId(e.target.value)}
                    />
                    <input
                        type="text"
                        placeholder="New Name"
                        value={newDepartmentName}
                        onChange={(e) => setNewDepartmentName(e.target.value)}
                    />
                    <button onClick={handleDepartmentNameChange}>Change Name</button>
                </div>
            </div>
        </div>
    );
}

export default App;
