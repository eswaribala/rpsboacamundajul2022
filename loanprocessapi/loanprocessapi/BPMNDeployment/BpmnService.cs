using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.ProcessInstance;

namespace loanprocessapi.BPMNDeployment
{
    public class BpmnService
    {
        private readonly CamundaClient camunda;
        public BpmnService(String RestApiService)
        {
            camunda = CamundaClient.Create(RestApiService);
        }
        public async Task DeployProcessDefinition()
        {
            var bpmnResourceStream = this.GetType()
                .Assembly
                .GetManifestResourceStream("loanprocessapi.Processes.loanprocess.bpmn");
            var bpmnResourceInsuranceStream = this.GetType()
               .Assembly
               .GetManifestResourceStream("loanprocessapi.Processes.insuranceprocess.bpmn");
            var bpmnResourceSWRequestStream = this.GetType()
               .Assembly
               .GetManifestResourceStream("loanprocessapi.Processes.softwarerequestprocess.bpmn");

            var bpmnResourceKafkaRequestStream = this.GetType()
               .Assembly
               .GetManifestResourceStream("loanprocessapi.Processes.kafkaprocess.bpmn");


            var bpmnResourceStreamHtml = this.GetType()
                .Assembly
                .GetManifestResourceStream("loanprocessapi.Forms.LoginForm.html");
            var bpmnResourceStreamLoanHtml = this.GetType()
               .Assembly
               .GetManifestResourceStream("loanprocessapi.Forms.LoanForm.html");
            var bpmnResourceStreamLoanDMN = this.GetType()
             .Assembly
             .GetManifestResourceStream("loanprocessapi.Decisions.loansanctionrules.dmn");
            var bpmnResourceStreamApprovalHtml = this.GetType()
             .Assembly
             .GetManifestResourceStream("loanprocessapi.Forms.LoanAcceptanceForm.html");
            var bpmnResourceStreamAgeApprovalHtml = this.GetType()
             .Assembly
             .GetManifestResourceStream("loanprocessapi.Forms.AgeAcceptanceForm.html");
            var bpmnResourceStreamPDFUploadHtml = this.GetType()
             .Assembly
             .GetManifestResourceStream("loanprocessapi.Forms.Task-Form-PDF-Upload.html");
            var bpmnResourceStreamPDFViewHtml = this.GetType()
             .Assembly
             .GetManifestResourceStream("loanprocessapi.Forms.Task-Form-PDF-Viewer.html");
            var bpmnResourceStreamPDFAdditionalUploadHtml = this.GetType()
             .Assembly
             .GetManifestResourceStream("loanprocessapi.Forms.Task-Form-PDF-Additional-Upload.html");
            var bpmnResourceStreamPDFUWUploadHtml = this.GetType()
                       .Assembly
                       .GetManifestResourceStream("loanprocessapi.Forms.Task-Form-PDF-UW-Upload.html");



            try
            {
                await camunda.Deployments.Create(
                    "Loan Process Deployment",
                    true,
                    true,
                    null,
                    null,
                    new ResourceDataContent(bpmnResourceStream, "loanprocess.bpmn"),
                     new ResourceDataContent(bpmnResourceInsuranceStream, "insuranceprocess.bpmn"),
                     new ResourceDataContent(bpmnResourceSWRequestStream, "softwarerequestprocess.bpmn"),

                     new ResourceDataContent(bpmnResourceKafkaRequestStream, "kafkarequestprocess.bpmn"),
                     new ResourceDataContent(bpmnResourceStreamHtml, "LoginForm.html"),
                         new ResourceDataContent(bpmnResourceStreamLoanHtml, "LoanForm.html"),
                          new ResourceDataContent(bpmnResourceStreamLoanDMN, "loansanctionrules.dmn"),
                          new ResourceDataContent(bpmnResourceStreamApprovalHtml, "LoanAcceptanceForm.html"),
                         new ResourceDataContent(bpmnResourceStreamPDFUploadHtml, "Task-Form-PDF-Upload.html"),
                          new ResourceDataContent(bpmnResourceStreamPDFViewHtml, "Task-Form-PDF-Viewer.html"),
                         new ResourceDataContent(bpmnResourceStreamPDFAdditionalUploadHtml, "Task-Form-PDF-Additional-Upload.html"),
                          new ResourceDataContent(bpmnResourceStreamPDFUWUploadHtml, "Task-Form-PDF-UW-Upload.html"),
                           new ResourceDataContent(bpmnResourceStreamAgeApprovalHtml, "AgeAcceptanceForm.html")
                    ); ;

            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to deploy process definition", e);
            }
        }

        public async Task CleanupProcessInstances()
        {
            var instances = await camunda.ProcessInstances
                .Query(new ProcessInstanceQuery
                {
                    ProcessDefinitionKey = "Process_"
                })
                .List();

            if (instances.Count > 0)
            {
                await camunda.ProcessInstances.Delete(new DeleteProcessInstances
                {
                    ProcessInstanceIds = instances.Select(i => i.Id).ToList()
                });
            }
        }
    }
}
