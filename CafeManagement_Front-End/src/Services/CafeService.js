import axios from "axios";

let API_BASE_URL = window.config.API_URL+ "Cafe/";

const CafeService = {
  // Fetch all cafes
  getCafes: async () => {
    try {
      const response = await axios.get(API_BASE_URL + "cafes");
      return response.data;
    } catch (error) {
      console.error("Error fetching cafes:", error);
      throw error;
    }
  },

  // Add a new cafe
  addCafe: async (cafe) => {
    try {
      const response = await axios.post(API_BASE_URL+"cafe", cafe, {
        headers: {
          "Content-Type": "application/json",
        },
      });
      return response.data;
    } catch (error) {
      console.error("Error adding cafe:", error);
      throw error;
    }
  },

  // Update an existing cafe
  updateCafe: async (cafe) => {
    try {
      const response = await axios.put(`${API_BASE_URL+"cafe"}`, cafe, {
        headers: {
          "Content-Type": "application/json",
        },
      });
      return response.data;
    } catch (error) {
      console.error("Error updating cafe:", error);
      throw error;
    }
  },

  // Delete a cafe
  deleteCafe: async (id) => {
    try {
      const response = await axios.delete(`${API_BASE_URL+"cafe"}/${id}`);
      return response.data;
    } catch (error) {
      console.error("Error deleting cafe:", error);
      throw error;
    }
  },
};

export default CafeService;
