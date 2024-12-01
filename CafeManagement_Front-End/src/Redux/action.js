export const addCafe = (cafeData) => ({
    type: 'ADD_CAFE',
    payload: cafeData,
  });
  
  export const updateCafe = (cafe) => {
    return {
      type: 'UPDATE_CAFE',
      payload: cafe,
    };
  };