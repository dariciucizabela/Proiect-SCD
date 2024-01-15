using DesktopApplication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace desktopApplication
{
    class DepartmentService
    {
        static HttpClient client = new HttpClient();

        public void createConnection()
        {
            client.BaseAddress = new Uri("http://localhost:8083/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("accept", "application/json");
        }

        public List<Department> GetDepartmentsAsync()
        {
            List<Department> departments = null;

            // Use a relative URI without a leading slash
            string requestUri = "api/department/getAll";
            HttpResponseMessage response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                departments = JsonConvert.DeserializeObject<List<Department>>(resultString);
                return departments;
            }
            else
            {
                Console.WriteLine("Error retrieving departments: " + response.StatusCode);
            }

            return null;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/department/get/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Department details: " + resultString);

                    Department department = JsonConvert.DeserializeObject<Department>(resultString);
                    return department;
                }
                else
                {
                    Console.WriteLine("Error getting department details: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<Department> AddDepartmentAsync(Department newDepartment)
        {
            try
            {
                string departmentJson = JsonConvert.SerializeObject(newDepartment);
                StringContent content = new StringContent(departmentJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/department/create", content);

                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Department added successfully. Details: " + resultString);

                    Department addedDepartment = JsonConvert.DeserializeObject<Department>(resultString);
                    return addedDepartment;
                }
                else
                {
                    Console.WriteLine("Error adding department: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<Department> UpdateDepartmentAsync(int id, Department updatedDepartment)
        {
            try
            {
                string departmentJson = JsonConvert.SerializeObject(updatedDepartment);
                StringContent content = new StringContent(departmentJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"api/department/update/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Department updated successfully. Details: " + resultString);

                    Department updatedDep = JsonConvert.DeserializeObject<Department>(resultString);
                    return updatedDep;
                }
                else
                {
                    Console.WriteLine("Error updating department: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"api/department/delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Department deleted successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Error deleting department: " + response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
