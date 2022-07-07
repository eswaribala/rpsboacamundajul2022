
import { ActivatedRoute, Router } from '@angular/router';
import {CamundaBPMService} from "../../camundabpm.service";

export class CompleteTaskComponent {
  model:any;
  submitted:any;
  route: ActivatedRoute
  router: Router
  camundaBPMService: CamundaBPMService

  constructor(route: ActivatedRoute,
    router: Router,
    camundaBPMService: CamundaBPMService,
    ) {
      this.route = route;
      this.router = router;
      this.camundaBPMService = camundaBPMService;
  }
  onSubmit() {
    this.route.params.subscribe(params => {
      const taskId = params['id'];
      const variables = this.generateVariablesFromFormFields();
      this.camundaBPMService.postCompleteTask(taskId, variables).subscribe();
      this.submitted = true;
      this.router.navigate(['/tasklist']);
    });
  }
  loadExistingVariables(taskId: String, variableNames: String) {
    this.camundaBPMService.getVariablesForTask(taskId, variableNames).subscribe((result) => {
      this.generateModelFromVariables(result);
    });
  }
  generateModelFromVariables(variables: { [x: string]: { value: any; }; }) {
    Object.keys(variables).forEach((variableName) => {
      this.model[variableName] = variables[variableName].value;
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
