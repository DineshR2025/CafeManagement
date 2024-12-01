import React, { useState, useEffect } from "react";
import {  Dialog,  DialogTitle,  DialogContent,  DialogActions,  TextField,  Button,  RadioGroup,  FormControlLabel,
  Radio,  FormControl,  FormLabel,  MenuItem,  Select,  InputLabel} from "@mui/material";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const AddEmployeePopup = ({ open, onClose, onSave, employeeData, empList = [], cafes = [] }) => {
  const [employee, setEmployee] = useState({
    name: "",
    email: "",
    phoneNumber: "",
    gender: "Male",
    assignedCafeId: "",
  });

  const [isDirty, setIsDirty] = useState(false);

  useEffect(() => {
    if (employeeData) {
      setEmployee(employeeData);
    } else {
      setEmployee({
        name: "",
        email: "",
        phoneNumber: "",
        gender: "Male",
        assignedCafeId: "",
      });
    }
  }, [employeeData]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEmployee({ ...employee, [name]: value });
    setIsDirty(true);
  };

  const validatePhoneNumber = (phone) => {
    const regex = /^[89]\d{7}$/;
    return regex.test(phone);
  };

  const validateEmail = (email) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
  };

  const handleSubmit = () => {
    const duplicateEmail = empList.some(
      (emp) => emp.email === employee.email && emp.id !== employee.id
    );
  
    const duplicatePhone = empList.some(
      (emp) => emp.phoneNumber === employee.phoneNumber && emp.id !== employee.id
    );    
    if(duplicateEmail){
      toast.error("Email Address already exists.");
      return;
    }
    if(duplicatePhone){
      toast.error("Phone number already exists.");
      return;
    }
    employee.cafe = cafes.find((cafe) => cafe.id === employee.assignedCafeId)?.name;
    onSave(employee); 
    refreshform();
    onClose();
    setIsDirty(false); 
  };
  
  const refreshform = () => {
    if(!employee.id) {
        setEmployee({
            name: "",
            email: "",
            phoneNumber: "",
            gender: "Male",
            assignedCafeId: "",
        });
    }
    else{
        setEmployee(employeeData);
    }
  }
  const handleCancel = () => {
    if (isDirty) {
      const confirmLeave = window.confirm(
        "You have unsaved changes. Do you really want to cancel?"
      );
      if (!confirmLeave) return
      refreshform();
    }
    onClose();
  };
  
  return (
    <Dialog open={open} onClose={handleCancel} fullWidth maxWidth="sm">
      <DialogTitle>
        {employeeData ? "Edit Employee" : "Add New Employee"}
      </DialogTitle>
      <DialogContent>
        <TextField
          label="Name"
          name="name"
          value={employee.name}
          onChange={handleChange}
          fullWidth
          margin="normal"
          e={{ minLength: 6, maxLength: 10 }}
          required
          error={employee.name.length < 6 || employee.name.length > 10}
          helperText="Name must be between 6 and 10 characters."
        />
        <TextField
          label="Email Address"
          name="email"
          value={employee.email}
          onChange={handleChange}
          fullWidth
          margin="normal"
          required
          error={!validateEmail(employee.email)}
          helperText={!validateEmail(employee.email) ? "Invalid email format." : ""}
        />
        <TextField
          label="Phone Number"
          name="phoneNumber"
          value={employee.phoneNumber}
          onChange={handleChange}
          fullWidth
          margin="normal"
          required
          error={!validatePhoneNumber(employee.phoneNumber)}
          helperText={
            !validatePhoneNumber(employee.phoneNumber)
              ? "Phone number must start with 8 or 9 and be 8 digits long."
              : ""
          }
        />
        <FormControl component="fieldset" margin="normal">
          <FormLabel component="legend">Gender</FormLabel>
          <RadioGroup
            row
            name="gender"
            value={employee.gender}
            onChange={handleChange}
          >
            <FormControlLabel value="Male" control={<Radio />} label="Male" />
            <FormControlLabel value="Female" control={<Radio />} label="Female" />
          </RadioGroup>
        </FormControl>
        <FormControl fullWidth margin="normal">
        <InputLabel id="cafe-label">Assigned Cafe</InputLabel>
        <Select
            labelId="cafe-label"
            name="assignedCafeId"
            label="Assigned Cafe"
            value={employee.assignedCafeId}
            onChange={handleChange}
            required
        >
            {cafes && cafes.length > 0 ? (
            cafes.map((cafe) => (
                <MenuItem key={cafe.id} value={cafe.id}>
                {cafe.name}
                </MenuItem>
            ))
            ) : (
            <MenuItem disabled>No cafes available</MenuItem>
            )}
        </Select>
        </FormControl>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleCancel} color="secondary">
          Cancel
        </Button>
        <Button
          onClick={handleSubmit}
          color="primary"
          variant="contained"
          disabled={
            employee.name.length < 6 ||
            employee.name.length > 10 ||
            !validateEmail(employee.email) ||
            !validatePhoneNumber(employee.phoneNumber) ||
            !employee.assignedCafeId
          }
        >
          Submit
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AddEmployeePopup;
