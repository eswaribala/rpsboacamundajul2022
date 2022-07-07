import { Component, OnInit } from '@angular/core';
import {CamundaBPMService} from "../camundabpm.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-startprocess',
  templateUrl: './startprocess.component.html',
  styleUrls: ['./startprocess.component.css']
})
export class StartprocessComponent implements OnInit {

  processDefinitionKey: String;
  formKey: String;
  rootViewContainer = null;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private camundaBPMService: CamundaBPMService
  ) { }

  ngOnInit() {
    if (this.route.params != null) {
      this.route.params.subscribe(params => {
        this.processDefinitionKey = params['processdefinitionkey'];
        this.loadTaskKey();
      });
    }
  }

  loadTaskKey(): void {
    this.camundaBPMService.getProcessDefinitionTaskKey(this.processDefinitionKey)
      .subscribe((formKey:any) => {
        this.formKey = formKey.key
      });
  }

}
