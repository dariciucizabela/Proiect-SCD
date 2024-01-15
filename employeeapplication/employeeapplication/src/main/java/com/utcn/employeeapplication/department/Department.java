package com.utcn.employeeapplication.department;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Data
@Getter
@Setter


public class Department {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "department_id")
    private Integer ID;

    private String description;
    private Integer parentID;
    private Integer managerID;
}
