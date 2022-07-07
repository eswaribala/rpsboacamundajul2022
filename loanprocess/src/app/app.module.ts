import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import {TabMenuModule} from "primeng/tabmenu";

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent
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
