import { Component, OnInit } from '@angular/core';
import {CamundaBPMService} from "../camundabpm.service";

@Component({
  selector: 'app-processes',
  templateUrl: './processes.component.html',
  styleUrls: ['./processes.component.css']
})
export class ProcessesComponent implements OnInit {

  processDefinitions:any;

  constructor(private camundaBPMService: CamundaBPMService) { }

  ngOnInit() {
    this.getProcessDefinitions();
  }

  getProcessDefinitions(): void {
    this.camundaBPMService
      .getProcessDefinitions()
      .subscribe((processDefinitions: any) => this.processDefinitions = processDefinitions);
  }

}
