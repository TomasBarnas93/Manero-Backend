using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Helpers.Factory
{
    public class HttpResultFactory
    {
        public static CreatedResult Created(string location, object? value)
        {
            return new(location, value);
        }

        public static ConflictResult Conflict()
        {
            return new();
        }

        public static ConflictObjectResult Conflict(object? errorObject)
        {
            return new(errorObject);
        }

        public static BadRequestResult BadRequest()
        {
            return new();
        }

        public static BadRequestObjectResult BadRequest(object? errorObject) 
        { 
            return new(errorObject);
        }

        public static UnauthorizedResult Unauthorized() 
        {
            return new();
        }

        public static UnauthorizedObjectResult Unauthorized(object? errorObject)
        {
            return new(errorObject);
        }

        public static OkResult Ok()
        {
            return new();
        }

        public static OkObjectResult Ok(object? value)
        {
            return new(value);
        }
    }
}
