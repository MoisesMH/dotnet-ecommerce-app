using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Errors;

public class ApiValidationErrorResponse : ApiResponse
{
    // Using validation error status code for this response, which is 400
    public ApiValidationErrorResponse() : base(400)
    {
    }

    public IEnumerable<string> Errors { get; set; }
}