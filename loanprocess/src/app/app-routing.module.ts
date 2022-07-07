import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {StartprocessComponent} from "./startprocess/startprocess.component";
import {ProcessesComponent} from "./processes/processes.component";
import {TasklistComponent} from "./tasklist/tasklist.component";

const routes: Routes = [{
  path:'home',
  component:HomeComponent
},
  {
    path:'start',
    component:StartprocessComponent
  },
  {
    path:'process',
    component:ProcessesComponent
  },
  {
    path:'tasklist',
    component:TasklistComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
