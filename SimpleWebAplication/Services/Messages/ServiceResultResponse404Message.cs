namespace SimpleWebAplication.Services
{
    public class ServiceResultResponse404Message : ServiceResultResponseMessage
    {
        public static ServiceResultResponseMessage Create(string message = "Not found") 
        {
            return new ServiceResultResponse404Message() { Message = message };
        }

        public override int StatusCode => 404;
    }
}
