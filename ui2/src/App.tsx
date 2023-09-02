import { Button } from "@material-tailwind/react";
import MainLayout from "./MainLayout";
import Table from "./Table";
import Grid  from "./Grid"; 
export default function Example() {
  return  (
    <MainLayout>
            <Button color="blue">Button</Button>
            {/* <Table/> */}
            <Grid/>
        </MainLayout>
  )
        
  
}