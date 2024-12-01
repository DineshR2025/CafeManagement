import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import CafesPage from './pages/CafesPage';
import LeftNav from './component.jsx/LeftNav';
import EmployeesPage from './pages/EmployeesPage';
import { Box, CssBaseline, Toolbar } from "@mui/material";
import { ToastContainer } from "react-toastify";
import { Provider } from 'react-redux';
import store from './Redux/Redux-store';

function App() {
  return (
    <Provider store={store}> 
      <Router>
        <ToastContainer />
        <Box sx={{ display: "flex" }}>
          <CssBaseline />
          <LeftNav />
          <Box
            component="main"
            sx={{
              flexGrow: 1,
              padding: "16px",
              backgroundColor: "beige"
            }}
          >
            <Toolbar />
            <Routes>
              <Route path="/" element={<CafesPage />} />
              <Route path="/cafes" element={<CafesPage />} />
              <Route path="/employees" element={<EmployeesPage />} />
            </Routes>
          </Box>
        </Box>
      </Router>
    </Provider> 
  );
}

export default App;
