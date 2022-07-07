import { ActivatedRoute } from '@angular/router';
import {CamundaBPMService} from "../../camundabpm.service";

export class StartProcessInstanceComponent {
  model: { [x: string]: any; }
  submitted: boolean
  route: ActivatedRoute
  camundaBPMService: CamundaBPMService

  constructor(route: ActivatedRoute,
              camundaBPMService: CamundaBPMService
    ) {
      this.route = route;
      this.camundaBPMService=camundaBPMService;
  }
  onSubmit() {
    this.route.params.subscribe(params => {
      const processDefinitionKey = params['processdefinitionkey'];
      const variables = this.generateVariablesFromFormFields();
      this.camundaBPMService.postProcessInstance(processDefinitionKey, variables).subscribe();
      this.submitted = true;
    });
  }
  generateVariablesFromFormFields() {
    const variables = {
      variables: { }
    };
    Object.keys(this.model).forEach((field) => {
      // @ts-ignore
      variables.variables[field] = {
        value: this.model[field]
      };
    });

    return variables;
  }
}
