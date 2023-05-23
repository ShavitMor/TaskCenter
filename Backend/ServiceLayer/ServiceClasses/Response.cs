/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.ServiceClasses
{
    ///<summary>Class <c>Response</c> represents the result of a call to a void function. 
    ///If an exception was thrown, <c>ErrorOccured = true</c> and <c>ErrorMessage != null</c>. 
    ///Otherwise, <c>ErrorOccured = false</c> and <c>ErrorMessage = null</c>.</summary>
    public class Response
    {
        private string? ErrorMessage;
        private object? ReturnValue;

       public object ErrorOccured { get { if (ErrorMessage != null) return ErrorMessage; if (ReturnValue != null) return ReturnValue; else return "{}"; } }
        public Response() { this.ErrorMessage = null; this.ReturnValue = null; }
        public Response(object obj) { this.ReturnValue = obj; this.ErrorMessage = null; }
        public Response(string messege)
        {
            this.ErrorMessage = messege;
            this.ReturnValue=null;
        }
        


    }
}*/