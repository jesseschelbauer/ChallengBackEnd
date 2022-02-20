namespace SimpleWebAplication.Services
{
    public class ServiceResultResponse409Message : ServiceResultResponseMessage
    {
        public static ServiceResultResponseMessage Create(string message = "Conflict")
        {
            return new ServiceResultResponse409Message() { Message = message };
        }
        public override int StatusCode => 409;
    }
}
