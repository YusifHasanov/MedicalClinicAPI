import { LinearProgress } from '@mui/material';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid'
import React, { FC } from 'react'
import { GridRowClassNameParams } from '@mui/x-data-grid';

interface Props {
    rows: any[],
    columns: GridColDef<any>[],
    loading: boolean,
    getRowClassName?: ((params: GridRowClassNameParams<any>) => string),
}

const MedicalGrid: FC<Props> = ({ rows, columns, getRowClassName, loading }) => {
    return (
        <DataGrid
            slotProps={{ toolbar: { showQuickFilter: true, } }}
            checkboxSelection
            disableRowSelectionOnClick
            slots={{ toolbar: GridToolbar, loadingOverlay: LinearProgress }}
            getRowClassName={getRowClassName ?? (() => "")}
            loading={loading}
            rows={rows}
            columns={columns} />
    )
}


export default MedicalGrid
