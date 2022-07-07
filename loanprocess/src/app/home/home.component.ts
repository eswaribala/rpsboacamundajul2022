import { Component, OnInit } from '@angular/core';
import {CamundaBPMService} from "../camundabpm.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  private fileToUpload: File;
  SUCCESS: boolean = false;

  constructor(private camundaBPMService: CamundaBPMService) {

  }

  ngOnInit() {
  }

  handleFileInput($event:Event) {
    // @ts-ignore
    this.fileToUpload = $event.target.files.item(0);
    this.uploadFileToActivity();
  }

  uploadFileToActivity() {
    this.camundaBPMService.deployProcess(this.fileToUpload).subscribe(data => {
      this.SUCCESS = true;
    }, error => {
      console.log(error);
    });
  }

}
