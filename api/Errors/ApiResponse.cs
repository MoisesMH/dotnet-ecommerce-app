using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Errors;

public class ApiResponse
{
    
    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }

    public string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request you've made." ,
            401 => "Authorized, you're not.",
            404 => "Resource found, it was not.",
            500 => "Errors are the path to the dark side. Errors leads to anger. Anger leads to hate. Hate leads to carrer change.",
            _ => null
        };
    }
}