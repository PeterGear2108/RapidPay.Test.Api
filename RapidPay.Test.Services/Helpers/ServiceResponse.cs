namespace RapidPay.Test.Services.Helpers
{
    public class ServiceResponse<T> where T : class
    {   
        public string Result { get; set; }
        public string[] Errors { get; set; }
        public T? Data { get; set; }
        public ServiceResponse()
        {
            Result = "Success";
            Errors = Array.Empty<string>();
        }
    }
}
