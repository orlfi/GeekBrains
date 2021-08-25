using System;
using System.Net;

namespace Timesheets.Exceptions
{
    public class TaskIsPaidException : Exception
    {
        public int TaskId {get; set;}
        private const string  DefaultMessageTemplate = "For a task with id={0} an invoice has already been issued";

        public TaskIsPaidException (int taskId):base(string.Format(DefaultMessageTemplate, taskId)) => TaskId = taskId;
    }
}