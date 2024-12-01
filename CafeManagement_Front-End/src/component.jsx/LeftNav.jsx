import React from "react";
import { Link } from "react-router-dom";
import { Drawer, List, ListItem, ListItemIcon, ListItemText, Toolbar, Typography } from "@mui/material";
import CoffeeMakerTwoToneIcon from '@mui/icons-material/CoffeeMakerTwoTone';
import PeopleIcon from "@mui/icons-material/People"; // Icon for Employees

const drawerWidth = 200;

const LeftNav = () => {
  return (
    <Drawer
      variant="permanent"
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: "border-box",  backgroundColor: "wheat", // Set your desired background color
          color: "black" },
      }}
    >
      <Toolbar>
        <Typography variant="h6" noWrap>
          Cafe Manager
        </Typography>
      </Toolbar>
      <List>
        <ListItem button component={Link} to="/cafes">
          <ListItemIcon>
            <CoffeeMakerTwoToneIcon style={{ color: "brown" }}/>
          </ListItemIcon>
          <ListItemText style={{ color: "brown" }} primary="Cafes" />
        </ListItem>
        <ListItem button component={Link} to="/employees">
          <ListItemIcon>
            <PeopleIcon style={{ color: "brown" }}/>
          </ListItemIcon>
          <ListItemText style={{ color: "brown" }} primary="Employees" />
        </ListItem>
      </List>
    </Drawer>
  );
};

export default LeftNav;
