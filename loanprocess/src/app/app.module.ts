import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import {TabMenuModule} from "primeng/tabmenu";
import { HomeComponent } from './home/home.component';
import { StartprocessComponent } from './startprocess/startprocess.component';
import { ProcessesComponent } from './processes/processes.component';
import { TasklistComponent } from './tasklist/tasklist.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    HomeComponent,
    StartprocessComponent,
    ProcessesComponent,
    TasklistComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        TabMenuModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
