using System;
using Newtonsoft.Json;
using umvel.challenge.application.Exceptions;

namespace umvel.challenge.api
{
	public class HttpApiResponse
	{
		public HttpApiResponse()
		{
		}

		private HttpApiResponse(int statusCode, ResponseCode code, object response)
		{
			this.StatusCode = statusCode;
			this.RQID = Guid.NewGuid().ToString();
			this.Response = GetResponseBodyContent(response);
			this.Code = code;
			this.SetErrorCodeDetail((int)code);
		}
       
        public int StatusCode { get; set; }

       
        public ResponseCode Code { get; set; }

        
        public string Severity { get; set; }

        
        public string Description { get; set; }

        
        public string RQID { get; set; }

        
        public object Response { get; set; }

      
        public static HttpApiResponse Ok(ResponseCode code, object response)
        {
            return new HttpApiResponse(200, code, response);
        }


        public static HttpApiResponse InternalServerError(ResponseCode code, object response)
        {
            return new HttpApiResponse(500, code, response);
        }

        public static HttpApiResponse BadRequest(ResponseCode code, object response)
        {
            return new HttpApiResponse(400, code, response);
        }

        private void SetErrorCodeDetail(int code)
        {
            switch (code)
            {
                case 0:
                    Severity = "None";
                    Description = "Success";
                    break;
                case int c when c >= 1000 && c < 2000:
                    Severity = "Medium";
                    Description = "Service exception.";
                    break;
                case int c when c >= 2000 && c < 9999:
                    Severity = "Low";
                    Description = "Validation exception.";
                    break;
                case 9999:
                    Severity = "High";
                    Description = "Unhandled exception. Please contact the system administrator for more details";
                    break;
                default:
                    Severity = "Not set";
                    Description = "Not set";
                    break;
            }
        }

        private object GetResponseBodyContent(object response)
        {
            return (response is string) || response.GetType() == typeof(byte[]) ?
                response : JsonConvert.SerializeObject(response);
        }
    }
}

