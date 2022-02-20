namespace SimpleWebAplication.Services
{
    static class CustomResultExtensions 
    {
         public static IResult ServiceResult(this IResultExtensions extensions, ServiceResultResponseMessage? serviceResultResponseMessage) 
        {
            return new ServiceResult(serviceResultResponseMessage);
        }
    }
}
