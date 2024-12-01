import React, { useEffect, useState } from "react";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-quartz.css";
import { Button, Container, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import AddCafePopup from "../component.jsx/AddCafePopup";
import CafeService from "../Services/CafeService";
import {CircularProgress} from "@mui/material";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function CafesPage() {
  const [cafes, setCafes] = useState([]);
  const navigate = useNavigate();
  const [openPopup, setOpenPopup] = useState(false);
  const [editingCafe, setEditingCafe] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
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
    fetchCafes();
  }, []);

  const columns = [
    {
      headerName: "Logo",
      field: "logo",
      flex: 1,
      cellRenderer: ({ value }) =>
        value ? (
          <img
            src={`data:image/png;base64,${value}`}
            alt="logo"
            style={{ width: "40px", height: "40px", borderRadius: "5px" }}
          />
        ) : (
          "No Logo"
        ),
    },
    { headerName: "Name", field: "name", flex: 1,  filter: true },
    { headerName: "Description", field: "description", flex: 2,
      cellRenderer: ({ value }) => (
        <div
          style={{
            overflow: "hidden",
            textOverflow: "ellipsis",
            whiteSpace: "nowrap",
            maxWidth: "100%",
            cursor: "pointer",
          }}
          title={value}
        >
          {value}
        </div>)
     },
    {
      headerName: "Employees",
      field: "employees",
      flex: 1,
      cellRenderer: ({ data }) => (
        <Button onClick={() => navigate(`/employees?cafeId=${data.id}`)}>
          {data.employees}
        </Button>
      ),
    },
    { headerName: "Location", field: "location", flex: 1, filter: true,
      cellRenderer: ({ value }) => (
        <div
          style={{
            overflow: "hidden",
            textOverflow: "ellipsis",
            whiteSpace: "nowrap",
            maxWidth: "100%",
            cursor: "pointer",
          }}
          title={value}
        >
          {value}
        </div>)
     },
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

  const handleEdit = (cafe) => {
    setEditingCafe(cafe);
    setOpenPopup(true);
  };

  const handleAdd = () => {
    setEditingCafe(null);
    setOpenPopup(true);
  };

  const handleSave = async (cafe) => {
    if (editingCafe) {
      try {
        await CafeService.updateCafe(cafe);
        setCafes((prev) =>
          prev.map((c) => (c.id === cafe.id ? { ...c, ...cafe } : c))
        );
        toast.success("Cafe updated successfully");
      } catch (error) {
        console.error("Error updating cafe:", error);
        toast.error("Error in updating cafe. Please try again.");
      }
    } else {
      try {
        const newCafe = await CafeService.addCafe(cafe);
        const data = await CafeService.getCafes();
        toast.success("Cafe added successfully");
        setCafes(data);
      } catch (error) {
        console.error("Error adding cafe:", error);
        toast.error("Error in adding cafe. Please try again.");
      }
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this cafe?")) {
      try {
        await CafeService.deleteCafe(id);
        setCafes((prev) => prev.filter((cafe) => cafe.id !== id));
        toast.success("Cafe deleted successfully");
      } catch (error) {
        console.error("Error deleting cafe:", error);
        toast.error("Error in deleting cafe. Please try again.");
      }
    }
  };

  return (
    <div className="cafe-page">
      <Container maxWidth="lg">
        <div>
          <Typography variant="h4" gutterBottom>
            Cafes
          </Typography>
          <Button
            variant="contained"
            onClick={handleAdd}
            style={{ marginBottom: "10px" }}
          >
            Add New Cafe
          </Button>
        </div>
        <div className="ag-theme-quartz" style={{ height: 500, width: "1000px" }}>
        {loading ? <CircularProgress /> : (<AgGridReact
            rowData={cafes}
            columnDefs={columns}
            domLayout="autoHeight"
          />)}
        </div>
        <AddCafePopup
          open={openPopup}
          onClose={() => setOpenPopup(false)}
          onSave={handleSave}
          cafeData={editingCafe}
        />
      </Container>
    </div>
  );
}

export default CafesPage;
