import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CompleteTaskComponent } from '../general/complete-task.component';
import { MyProcessData } from '../../schemas/MyProcessData';
import {CamundaBPMService} from "../../camundabpm.service";

@Component({
  selector: 'approveDataTask',
  templateUrl: './approveDataTask.component.html',
  styleUrls: []
})
export class approveDataTaskComponent extends CompleteTaskComponent {
  submitted:boolean = false;
  model = new MyProcessData('','',false);

  constructor(route: ActivatedRoute,
    router: Router,
    camundaBPMService: CamundaBPMService) {
    super(route, router, camundaBPMService);
    this.route.params.subscribe(params => {
      const taskId = params['id'];
      const variableNames = Object.keys(this.model).join(',');
      this.loadExistingVariables(taskId, variableNames);
    });
  }

  changedExtraHandler($event: Event) {
    
  }
}
