import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { StartProcessInstanceComponent } from '../general/start-process-instance.component'
import { MyProcessData } from '../../schemas/MyProcessData';
import {CamundaBPMService} from "../../camundabpm.service";

@Component({
  selector: 'startNewProcess',
  templateUrl: './startNewProcess.component.html',
  styleUrls: []
})
export class startNewProcessComponent extends StartProcessInstanceComponent {
  submitted:boolean = false;
  model = new MyProcessData('','',false);

  constructor(route: ActivatedRoute,
    camundaBPMService: CamundaBPMService,) {
    super(route, camundaBPMService);
  }

}
