import { legacy_createStore, combineReducers } from 'redux';
import { reducer as formReducer } from 'redux-form';
import cafeReducer from './reducer';

const rootReducer = combineReducers({
  form: formReducer,
  cafes: cafeReducer,  // Add your cafeReducer to the store
});

const store = legacy_createStore(rootReducer);

export default store;
