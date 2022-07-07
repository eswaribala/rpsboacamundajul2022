import { Component, OnInit } from '@angular/core';
import {MenuItem} from "primeng/api";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {


  items: MenuItem[]=[];

  ngOnInit() {
    this.items = [
      {label: 'Home', icon: 'pi pi-fw pi-home'},
      {label: 'Start Process', icon: 'pi pi-fw pi-calendar'},
      {label: 'Processes', icon: 'pi pi-fw pi-calendar'},
      {label: 'Task Lists', icon: 'pi pi-fw pi-pencil'},

    ];
  }

}
