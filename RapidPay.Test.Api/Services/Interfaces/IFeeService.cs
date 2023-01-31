using RapidPay.Test.Models.Domain;
using RapidPay.Test.Services.Helpers;

namespace RapidPay.Test.Services.Interfaces
{
    public interface IFeeService
    {
        Task<double> GetCurrentFeeExchange(double payment);
    }
}
