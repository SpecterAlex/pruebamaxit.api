using System;
using System.Data.SqlClient;
using System.Net;
using System.Text;


namespace Employees.Models
{
    public class GenericResponse<T>
    {
        public string TransactionMessage { get; set; }
        public bool StatusTransaction { get; set; }
        public string ExceptionMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public int TotalRecords { get; set; }
        public T Data { get; set; }

        public GenericResponse()
        {
            ExceptionMessage = "";
            StatusTransaction = true;
            StatusCode = HttpStatusCode.OK;
            TotalRecords = 0;
            TransactionMessage = "Correct Transaction";
        }
        public GenericResponse(Exception exception)
        {
            var msg = new StringBuilder();
            StatusCode = HttpStatusCode.BadRequest;
            StatusTransaction = false;
            msg.AppendFormat("Error Message: {0} <--> ", exception.Message);
            msg.AppendFormat("Layer Error: {0} <--> ", exception.Source);
            msg.AppendFormat("Method Error: {0} <--> ", exception.StackTrace);
            ExceptionMessage = msg.ToString();
        }
        public GenericResponse(Exception exception, T response)
        {
            var msg = new StringBuilder();
            StatusTransaction = false;
            StatusCode = HttpStatusCode.BadRequest;
            TransactionMessage = "Incorrect Transaction"; 
            Data = response;
            msg.AppendFormat("Error Message: {0} <--> ", exception.Message);
            msg.AppendFormat("Layer Error: {0} <--> ", exception.Source);
            msg.AppendFormat("Method Error: {0} <--> ", exception.StackTrace);
            ExceptionMessage = msg.ToString();
        }
        public GenericResponse(SqlException SqlException)
        {
            var msg = new StringBuilder();
            StatusTransaction = false;
            StatusCode = HttpStatusCode.InternalServerError;
            TransactionMessage = "Incorrect Transaction";
            msg.AppendFormat("Server: {0} <--> ", SqlException.Server);
            msg.AppendFormat("Stored Procedure: {0} <--> ", SqlException.Procedure);
            msg.AppendFormat("Number Error: {0} <--> ", SqlException.Number);
            msg.AppendFormat("Layer Error: {0} <--> ", SqlException.Source);
            msg.AppendFormat("Method Error: {0} <--> ", SqlException.StackTrace);
            ExceptionMessage = msg.ToString();
        }
    }
}
