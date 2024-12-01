import axios from "axios";

let API_BASE_URL = window.config.API_URL+ "Employee/";

const EmployeeService = {
  // Fetch all Employees
  getAllEmployees: async (cafeId) => {
    try {
      const response = await axios.get(API_BASE_URL +"employees?cafe="+cafeId);
      return response.data;
    } catch (error) {
      console.error("Error fetching employees:", error);
      throw error;
    }
  },

  // Add a new employee
  addEmployee: async (employee) => {
    try {
      const response = await axios.post(`${API_BASE_URL+"employee/"}`, employee);
      return response.data;
    } catch (error) {
      console.error("Error adding employee:", error);
      throw error;
    }
  },

  // Update an existing employee.
  updateEmployee: async (employee) => {
    try {
      const response = await axios.put(`${API_BASE_URL+"employee/"}`, employee);
      return response.data;
    } catch (error) {
      console.error(`Error updating employee with ID ${id}:`, error);
      throw error;
    }
  },

  // Delete an employee.
  deleteEmployee: async (id) => {
    try {
      const response = await axios.delete(`${API_BASE_URL+"employee/"}${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error deleting employee with ID ${id}:`, error);
      throw error;
    }
  },
};

export default EmployeeService;
