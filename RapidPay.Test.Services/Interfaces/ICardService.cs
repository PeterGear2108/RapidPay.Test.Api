using RapidPay.Test.Models.Domain;
using RapidPay.Test.Models.Dto;
using RapidPay.Test.Services.Helpers;

namespace RapidPay.Test.Services.Interfaces
{
    public interface ICardService
    {
        ServiceResponse<Card> CreateCard(CardDto card);
        ServiceResponse<Card> PayWithCard(int cardNumber, double transactionAmmount);
        double GetCardBalance(int cardNumber);
    }
}
