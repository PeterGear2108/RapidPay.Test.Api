using RapidPay.Test.Models.Domain;
using RapidPay.Test.Models.Dto;
using RapidPay.Test.Services.Helpers;
using RapidPay.Test.Services.Interfaces;

namespace RapidPay.Test.Services
{
    public class CardService : ICardService
    {
        public ServiceResponse<Card> CreateCard(CardDto card)
        {
            throw new NotImplementedException();
        }

        public double GetCardBalance(int cardNumber)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Card> PayWithCard(int cardNumber, double transactionAmmount)
        {
            throw new NotImplementedException();
        }
    }
}