using RapidPay.Test.Models.Dto;

namespace RapidPay.Test.Api.Services.Interfaces
{
    public interface IAuthService
    {
        UserToken Login(string username, string password);
        void CreateUser(string username, string password);  

    }
}
