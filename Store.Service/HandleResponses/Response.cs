using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandleResponses
{

    public class Response
    {
        public Response(int statusCode,string? message=null)
        {
            StatusCode = statusCode;
            Message =message?? GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int statusCode)
         => statusCode switch
         { 
         100=>"Continue",
             101=>"Switching Protocols",
             200=>"Ok",
             404=>"NotFound",
             500 => "Internal Server Error",
             _ =>"UnKnown Status COde"

         
         };



    }
}
