using System.Net.Mime;

namespace SimpleWebAplication.Services
{
    public class ServiceResult : IResult
    {
        private ServiceResultResponseMessage? _serviceResultResponseMessage;

        public ServiceResult(ServiceResultResponseMessage? serviceResultResponseMessage)
        {
            _serviceResultResponseMessage = serviceResultResponseMessage;
        }
        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = _serviceResultResponseMessage!.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(_serviceResultResponseMessage);
        }
    }

    public class ServiceResult<T> where T : class
    {
        public T? Result { get; set; }
        public ServiceResultResponseMessage? ErrorResponse { get; set; }

        public static implicit operator ServiceResult<T>(T response)
        {
            return new ServiceResult<T> { Result = response };
        }

        public static implicit operator ServiceResult<T>(ServiceResultResponseMessage message)
        {
            return new ServiceResult<T> { ErrorResponse = message };
        }

        public bool IsOk => ErrorResponse == null;
    }
}
