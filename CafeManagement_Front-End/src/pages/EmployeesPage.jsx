import React, { useEffect, useState } from "react";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-quartz.css";
import { Button, Container, Typography } from "@mui/material";
import AddEmployeePopup from "../component.jsx/AddEmployeePopup";
import EmployeeService from "../Services/EmployeeService";
import CafeService from "../Services/CafeService";
import {CircularProgress} from "@mui/material";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";


function EmployeePage() {
  const [employees, setEmployees] = useState([]);
  const [openPopup, setOpenPopup] = useState(false);
  const [editingEmployee, setEditingEmployee] = useState(null);
  const [cafes, setCafes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [cafeId, setCafeId] = useState(null);
  
  useEffect(() => {
    const queryParams = new URLSearchParams(location.search);
    const cafeId = queryParams.get("cafeId");
    setCafeId(cafeId);

    const fetchCafes = async () => {
      setLoading(true);
      try {
        const data = await CafeService.getCafes();
        setCafes(data);
      } catch (error) {
        console.error("Error fetching cafes:", error);
        toast.error("Error in fetching cafes. Please try again.");
      }finally {
        setLoading(false); 
      }
    };
    const fetchEmployees = async () => {
      try {
        const data = await EmployeeService.getAllEmployees(cafeId);
        setEmployees(data);
      } catch (error) {
        console.error("Error fetching employees:", error);
        toast.error("Error in fetching employees. Please try again.");
      }
    };
    fetchCafes();
    fetchEmployees();
  }, []);

  const columns = [
    { headerName: "Employee ID", field: "id", flex: 1 },
    { headerName: "Name", field: "name", flex: 1 },
    { headerName: "Email", field: "email", flex: 2 },
    { headerName: "Phone Number", field: "phoneNumber", flex: 1 },
    { headerName: "Days Worked", field: "daysWorked", flex: 1 },
    { headerName: "Cafe Name", field: "cafe", flex: 1 },
    {
      headerName: "Actions",
      cellRenderer: ({ data }) => (
        <>
          <Button
            size="small"
            onClick={() => handleEdit(data)}
            variant="contained"
            sx={{ marginRight: 1 }}
          >
            Edit
          </Button>
          <Button
            size="small"
            color="error"
            variant="outlined"
            onClick={() => handleDelete(data.id)}
          >
            Delete
          </Button>
        </>
      ),
    },
  ];

  const handleEdit = (employee) => {
    setEditingEmployee(employee);
    setOpenPopup(true);
  };

  const handleAdd = () => {
    setEditingEmployee(null);
    setOpenPopup(true);
  };

  const handleDelete = async(id) => {
    if (window.confirm("Are you sure you want to delete this employee?")) {
     await EmployeeService.deleteEmployee(id)
        .then(() => {
          setEmployees((prev) => prev.filter((employee) => employee.id !== id));
          toast.success("Employee deleted successfully!");
        })
        .catch((error) => {toast.error("Error deleting employee. Please try again.")});
    }
  };
  
  // Add or update employee
  const handleSave = async (employee) => {
    if (editingEmployee) {
      try {
        await EmployeeService.updateEmployee(employee);
        setEmployees((prev) =>
          prev.map((e) => (e.id === editingEmployee.id ? { ...e, ...employee,  } : e))
        );
        toast.success("Employee updated successfully!");
        setOpenPopup(false);
      } catch (error) {
        toast.error("Error in updating employee. Please try again.");
        console.error("Error updating employee:", error);
      }
    } else {
      try {
        const newEmployee = await EmployeeService.addEmployee(employee);
        const data = await EmployeeService.getAllEmployees(cafeId);
        setEmployees(data);
        toast.success("Employee added successfully!");
        setOpenPopup(false);
      } catch (error) {
        toast.error("Error in adding employee. Please try again.");
        console.error("Error adding employee:", error);
      }
    }
  };

  return (
    <div
      className="employee-page">
      <Container maxWidth="lg">
        <Typography variant="h4" gutterBottom>
          Employees
        </Typography>
        <Button
          variant="contained"
          onClick={handleAdd}
          style={{ marginBottom: "10px" }}
        >
          Add New Employee
        </Button>
        <div
          className="ag-theme-quartz"
          style={{ height: 500, width: "1000px" }}
        >
        {loading ? <CircularProgress /> : (<AgGridReact
            rowData={employees}
            columnDefs={columns}
            domLayout="autoHeight"
          />)}
        </div>
        <AddEmployeePopup
          open={openPopup}
          onClose={() => setOpenPopup(false)}
          onSave={handleSave}
          employeeData={editingEmployee}
          empList = {employees}
          cafes={cafes}
        />
      </Container>
    </div>
  );
}

export default EmployeePage;
