import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface DataState {
  data: any;
  isLoading: boolean;
  error: string | null;
}

const initialState: DataState = {
  data: null,
  isLoading: false,
  error: null,
};

const dataSlice = createSlice({
  name: "data",
  initialState,
  reducers: {
    fetchDataStart: (state) => {
      state.isLoading = true;
    },
    fetchDataSuccess: (state, action: PayloadAction<any>) => {
      state.data = action.payload;
      state.isLoading = false;
    },
    fetchDataFailure: (state, action: PayloadAction<string>) => {
      state.error = action.payload;
      state.isLoading = false;
    },
  },
});

export const { fetchDataStart, fetchDataSuccess, fetchDataFailure } =
  dataSlice.actions;
export default dataSlice.reducer;
