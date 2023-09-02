import React from 'react'
import ReactDataGrid from '@inovua/reactdatagrid-community'
import '@inovua/reactdatagrid-community/index.css'

const columns = [
  { name: 'name', header: 'Name', minWidth: 50, defaultFlex: 2    },
 
  { name: 'age', header: 'Age', maxWidth: 1000, defaultFlex: 1 }
]

const gridStyle = { minHeight: 550 }

const dataSource = [
  { id: 1, name: 'John McQueen', age: 35 },
  { id: 2, name: 'Mary Stones', age: 25 },
  { id: 3, name: 'Robert Fil', age: 27 },
  { id: 4, name: 'Roger Robson', age: 81 },
  { id: 5, name: 'Billary Konwik', age: 18 },
  { id: 6, name: 'Bob Martin', age: 18 },
  { id: 7, name: 'Matthew Richardson', age: 54 },
  { id: 8, name: 'Ritchie Peterson', age: 54 },
  { id: 9, name: 'Bryan Martin', age: 40 },
  { id: 10, name: 'Mark Martin', age: 44 },
  { id: 11, name: 'Michelle Sebastian', age: 24 },
  { id: 12, name: 'Michelle Sullivan', age: 61 },
  { id: 13, name: 'Jordan Bike', age: 16 },
  { id: 14, name: 'Nelson Ford', age: 34 },
  { id: 15, name: 'Tim Cheap', age: 3 },
  { id: 16, name: 'Robert Carlson', age: 31 },
  { id: 17, name: 'Johny Perterson', age: 40 }
]
const filterValue = [
    { name: 'name', operator: 'contains', type: 'string', value: '' },
    { name: 'age', operator: 'gte', type: 'number', value: 21 },
  ];
  
export default () => <ReactDataGrid
  idProperty="id"
  columns={columns}
  dataSource={dataSource}
  style={gridStyle}
  filterable={true}
  defaultFilterValue={filterValue}
/>


/*

// api/dataApi.js
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const DATASET_URL = 'your_dataset_url_here';

export const dataApi = createApi({
  reducerPath: 'dataApi',
  baseQuery: fetchBaseQuery({ baseUrl: DATASET_URL }),
  endpoints: (builder) => ({
    fetchData: builder.query({
      query: (params) => {
        const { skip, limit, sortInfo, groupBy, filterValue } = params;
        // Construct the URL using the params
        const url = `?skip=${skip}&limit=${limit}&groupBy=${groupBy}&sortInfo=${JSON.stringify(
          sortInfo
        )}&filterBy=${JSON.stringify(filterValue)}`;
        return url;
      },
    }),
  }),
});

// Export hooks for usage in components
export const { useFetchDataQuery } = dataApi;

















import { useFetchDataQuery } from './api/dataApi';

const App = () => {
  const [filterValue, setFilterValue] = useState(defaultFilterValue);
  const [sortInfo, setSortInfo] = useState([]);

  const { data, isLoading, isError } = useFetchDataQuery({
    skip: 0, // Add other parameters as needed
    limit: 10,
    sortInfo,
    groupBy: [],
    filterValue,
  });

  const dataSource = useMemo(() => {
    if (isLoading || isError) {
      return { data: [], count: 0 };
    }

    const totalCount = data.headers.get('X-Total-Count');
    return { data, count: totalCount * 1 };
  }, [data, isLoading, isError]);

  return (
    <div>
      <h3>Remote filtering & sorting example</h3>
      <div style={{ height: 80 }}>
        Current filterValue:{' '}
        {filterValue ? <code>{JSON.stringify(filterValue, null, 2)}</code> : 'none'}.
      </div>
      <ReactDataGrid
        idProperty="id"
        style={gridStyle}
        columns={columns}
        defaultFilterValue={defaultFilterValue}
        defaultGroupBy={[]}
        dataSource={dataSource}
        onSortInfoChange={setSortInfo}
        onFilterValueChange={setFilterValue}
      />
    </div>
  );
};

export default App;




*/