using DesktopApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desktopApplication
{
    public partial class Form1 : Form
    {
        EmployeeService employeeService;
        List<Employee> employeeList;
        DepartmentService departmentService;
        List<Department> departmentList;
        public Form1()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            employeeService.createConnection();
            departmentService = new DepartmentService();
            departmentService.createConnection();
        }








//show enmpl
        private void button2_Click(object sender, EventArgs e)
        {
            var employeeList = employeeService.getEmployeesAsync();
            if (employeeList != null)
            {
                dataGridView1.DataSource = employeeList;
            }
            else
            {
                MessageBox.Show("Eroare la obținerea datelor. Verificați consola pentru detalii.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //show dep
        private void button3_Click(object sender, EventArgs e)
        {
            var departmentList = departmentService.GetDepartmentsAsync();
            if (departmentList != null)
            {
                dataGridView1.DataSource = departmentList;
            }
            else
            {
                MessageBox.Show("Eroare la obținerea datelor. Verificați consola pentru detalii.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // buton add
        private async void buttonAdd_Click_1(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            int managerID = int.Parse(textBoxManager_ID.Text);
            string email = textBoxEmail.Text;
            int depid = int.Parse(textBoxdepid.Text);

            Employee newEmployee = new Employee
            {
                name = name,
                managerID = managerID,
                email = email,
                departmentID = depid
            };

            await employeeService.AddEmployeeAsync(newEmployee);
        }


        //buton rename
        private async void buttonUpdate_Click_1(object sender, EventArgs e)
        {
            int employeeId = int.Parse(textBoxID.Text);
            string newName = textBoxName.Text;

            await employeeService.ChangeEmployeeNameByIdAsync(employeeId, newName);
        }

        //change email
        private async void button6_Click_1(object sender, EventArgs e)
        {
            int employeeId = int.Parse(textBoxID.Text);
            string email = textBoxEmail.Text;

            await employeeService.ChangeEmployeeEmailByIdAsync(employeeId, email);
        }

    
        //add depart
        private async void buttonAddDepartment_Click(object sender, EventArgs e)
        {

            string description = textBoxDescription.Text;
            int parentID = int.Parse(textBoxParentID.Text);
            int managerID = int.Parse(textBoxManagerID.Text);

            Department newDepartment = new Department
            {
                description = description,
                parentID = parentID,
                managerID = managerID
            };

            await departmentService.AddDepartmentAsync(newDepartment);
        }
        //rename dep
        private async void button5_Click(object sender, EventArgs e)
        {

            int departmentId = int.Parse(textBoxDepartmentID.Text);
            string newDescription = textBoxDescription.Text;
            int newParentID = int.Parse(textBoxParentID.Text);
            int newManagerID = int.Parse(textBoxManagerID.Text);


            Department newDepartment = new Department
            {
                description = newDescription,
                parentID = newParentID,
                managerID = newManagerID
            };
            await departmentService.UpdateDepartmentAsync(departmentId, newDepartment);
        }
        //del dep
        private async void button4_Click(object sender, EventArgs e)
        {
            int departmentId = int.Parse(textBoxDepartmentID.Text);


            await departmentService.DeleteDepartmentAsync(departmentId);
        }

        // depbyid
        private async void button7_Click(object sender, EventArgs e)
        {
            int iddepp =   int.Parse(textBoxDepartmentID.Text);

            var departmentList = departmentService.GetDepartmentByIdAsync(iddepp);
            if (departmentList != null)
            {
                dataGridView3.DataSource = departmentList;
            }
            else
            {
                MessageBox.Show("Eroare la obținerea datelor. Verificați consola pentru detalii.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

   
        // del empl
        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            int employeeId = int.Parse(textBoxID.Text);

            await employeeService.DeleteEmployeeAsync(employeeId);
        }

    }
}