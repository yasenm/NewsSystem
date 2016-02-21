namespace NewsSystem.Web.Common.Constants
{
    public class CommonToastrSettings
    {
        public const string BeginAction = "Command: toastr['info']('Command has been started!')";
        public const string FailedAction = "Command: toastr['error']('Something went wrong with the action!')";
        public const string CompleteAction = "Command: toastr['success']('Action was successfull!')";
        public const string WarningMsgAction = "Command: toastr['warning']('Something is not right!')";
    }
}
