const initialState = {
    cafes: [],
  };
  
  const cafeReducer = (state = initialState, action) => {
    switch (action.type) {
      case 'ADD_CAFE':
        return {
          ...state,
          cafes: [...state.cafes, action.payload],
        };
      default:
        return state;
    }
  };
  
  export default cafeReducer;
  