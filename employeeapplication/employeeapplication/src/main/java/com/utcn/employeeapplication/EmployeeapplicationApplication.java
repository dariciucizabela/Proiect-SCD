package com.utcn.employeeapplication;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
//@EntityScan({ "com.utcn.employeeapplication.*" })
public class EmployeeapplicationApplication {

	public static void main(String[] args) {
		SpringApplication.run(EmployeeapplicationApplication.class, args);
	}

}
