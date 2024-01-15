// DepartmentList.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';

const DepartmentList = () => {
    const [departments, setDepartments] = useState([]);

    useEffect(() => {
        fetchDepartments();
    }, []);

    const fetchDepartments = async () => {
        try {
            const response = await axios.get('http://localhost:8083/api/department/getAll');
            setDepartments(response.data);
        } catch (error) {
            console.error('Error fetching departments:', error);
        }
    };

    return (
        <div>
            <h2>Departments</h2>
            <ul>
                {departments.map((department) => (
                    <li key={department.id}>{department.name}</li>
                ))}
            </ul>
        </div>
    );
};

export default DepartmentList;
