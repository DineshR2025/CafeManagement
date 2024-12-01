import React, { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  Box,
  Typography,
} from "@mui/material";

const AddCafePopup = ({ open, onClose, onSave, cafeData }) => {
  const [cafe, setCafe] = useState({
    name: "",
    description: "",
    location: "",
    logo: null, 
  });

  const [isDirty, setIsDirty] = useState(false);

  useEffect(() => {
    if (cafeData) {
      setCafe(cafeData);
    } else {
      setCafe({
        name: "",
        description: "",
        location: "",
        logo: null,
      });
    }
  }, [cafeData]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCafe({ ...cafe, [name]: value });
    setIsDirty(true);
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      if (file.size <= 2 * 1024 * 1024) { // Ensure file size is less than 2MB
        const reader = new FileReader();
        reader.onloadend = () => {
          // Convert the file to base64 and set it to the cafe state
          const base64Logo = reader.result.split(',')[1]; // Remove the prefix "data:image/png;base64,"
          setCafe({ ...cafe, logo: base64Logo });
          setIsDirty(true);
        };
        reader.readAsDataURL(file); // This will read the file as a base64 string
      } else {
        alert("Logo file size must be less than 2MB!");
      }
    }
  };
  const refreshform = () => {
    if(!cafe.id) {
        setCafe({
          name: "",
          description: "",
          location: "",
          logo: null, 
        });
    }
    else{
        setCafe(cafeData);
    }
  }
  const handleSubmit = () => {
    onSave(cafe); 
    refreshform();
    onClose(); 
    setIsDirty(false);
  };

  const handleCancel = () => {
    if (isDirty) {
      const confirmLeave = window.confirm(
        "You have unsaved changes. Do you really want to cancel?"
      );
      if (!confirmLeave) return;
      refreshform();
    }
    onClose();
  };

  return (
    <Dialog open={open} onClose={handleCancel} fullWidth maxWidth="sm">
      <DialogTitle>{cafeData ? "Edit Cafe" : "Add New Cafe"}</DialogTitle>
      <DialogContent>
        <TextField
          label="Name"
          name="name"
          value={cafe.name}
          onChange={handleChange}
          fullWidth
          margin="normal"
          e={{ minLength: 6, maxLength: 10 }}
          required
          error={cafe.name.length < 6 || cafe.name.length > 10}
          helperText="Name must be between 6 and 10 characters."
        />
        <TextField
          label="Description"
          name="description"
          value={cafe.description}
          onChange={handleChange}
          fullWidth
          margin="normal"
          e={{ maxLength: 256 }}
          required
          helperText="Maximum 256 characters allowed."
        />
        <TextField
          label="Location"
          name="location"
          value={cafe.location}
          e={{ maxLength: 60 }}
          onChange={handleChange}
          fullWidth
          margin="normal"
          required
          helperText="Maximum 60 characters allowed."
        />
        <Box sx={{ marginTop: 2 }}>
          <Button variant="contained" component="label">
            Upload Logo
            <input type="file" hidden accept="image/*" onChange={handleFileChange} />
          </Button>
          {cafe.logo && (
            <Typography variant="body2" sx={{ marginTop: "8px" }}>
              {cafe.logo.name || "Logo selected"} {/* Display file name */}
            </Typography>
          )}
        </Box>
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
            cafe.name.length < 6 ||
            cafe.name.length > 10 ||
            !cafe.description ||
            !cafe.location ||
            !cafe.logo
          }
        >
          Submit
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AddCafePopup;
