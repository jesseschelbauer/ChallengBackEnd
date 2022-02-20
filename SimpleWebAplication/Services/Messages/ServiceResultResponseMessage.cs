namespace SimpleWebAplication.Services
{
    public abstract class ServiceResultResponseMessage
    {
        public ServiceResultResponseMessage()
        {
        }

        public string? Message {get; set;}
        public abstract int StatusCode { get; }
    }
}
