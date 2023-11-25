import { createSlice } from "@reduxjs/toolkit";
import { RootState } from "../store";
import moment from "moment";

interface IntervalState {
    fromDate: Date | null,
    toDate: Date | null,
    isAll: boolean
}
 
const intervalSlice = createSlice({
    name: 'interval',
    initialState: {
        fromDate:   moment().add(-1, 'month').toDate(),
        toDate: moment().add(1, 'month').toDate(),
        isAll: false
    } as IntervalState,
    reducers: {
        setDates(state, action) {
            state.fromDate = action.payload.fromDate;
            state.toDate = action.payload.toDate;
        },
        setIsAll(state, action) {
            state.isAll = action.payload;
        }
    }

});

export const { setDates, setIsAll } = intervalSlice.actions; 
export const selectInterval = (state: RootState) => state.interval;
 
export default intervalSlice.reducer;