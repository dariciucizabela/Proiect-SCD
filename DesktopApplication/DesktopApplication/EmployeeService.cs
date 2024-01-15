using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace desktopApplication
{
    class EmployeeService
    {
        static HttpClient client = new HttpClient();

        public void createConnection()
        {
            client.BaseAddress = new Uri("http://localhost:8083/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("accept", "application/json");
        }

        public List<Employee> getEmployeesAsync()
        {
            List<Employee> employees = null;
            HttpResponseMessage response = client.GetAsync("api/employee/getAll").Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Lista de angajați: " + resultString);

                employees = JsonConvert.DeserializeObject<List<Employee>>(resultString);
                return employees;
            }
            else
            {
                Console.WriteLine("Eroare la solicitarea datelor: " + response.StatusCode);
            }

            return null;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            try
            {
                string employeeJson = JsonConvert.SerializeObject(employee);
                StringContent content = new StringContent(employeeJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/employee/create", content);

                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Employee added successfully. Details: " + resultString);

                    Employee addedEmployee = JsonConvert.DeserializeObject<Employee>(resultString);
                    return addedEmployee;
                }
                else
                {
                    Console.WriteLine("Error adding employee: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"api/employee/delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Employee deleted successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Error deleting employee: " + response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }


        public async Task<Employee> ChangeEmployeeNameByIdAsync(int id, string newName)
        {
            try
            {
                // Create a new object to hold the updated name
                var updatedEmployeeName = new
                {
                    name = newName
                };

                // Serialize the updated name object
                string updatedNameJson = JsonConvert.SerializeObject(updatedEmployeeName);
                StringContent content = new StringContent(updatedNameJson, Encoding.UTF8, "application/json");

                // Send a PUT request to update the employee's name
                HttpResponseMessage response = await client.PutAsync($"api/employee/updateName/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Employee's name updated successfully. Details: " + resultString);

                    Employee updatedEmployee = JsonConvert.DeserializeObject<Employee>(resultString);
                    return updatedEmployee;
                }
                else
                {
                    Console.WriteLine("Error updating employee's name: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<Employee> ChangeEmployeeEmailByIdAsync(int id, string newEmail)
        {
            try
            {
                var emailUpdate = new
                {
                    email = newEmail
                };

                string emailUpdateJson = JsonConvert.SerializeObject(emailUpdate);
                StringContent content = new StringContent(emailUpdateJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"api/employee/updateEmail/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Employee's email updated successfully. Details: " + resultString);

                    Employee updatedEmployee = JsonConvert.DeserializeObject<Employee>(resultString);
                    return updatedEmployee;
                }
                else
                {
                    Console.WriteLine("Error updating employee's email: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }


    }
}
