using RapidPay.Test.Models.Domain;
using RapidPay.Test.Services.Helpers;

namespace RapidPay.Test.Services.Interfaces
{
    public interface IFeeService
    {
        double GetCurrentFeeExchange();
        ServiceResponse<Fee> SaveFee();
    }
}
