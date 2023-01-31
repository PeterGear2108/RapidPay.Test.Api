using RapidPay.Test.DataAccess;
using RapidPay.Test.Models.Domain;
using RapidPay.Test.Services.Interfaces;

namespace RapidPay.Test.Services
{
    public sealed class BinService : Repository<Card>, IBinService
    {
        public BinService(IConfiguration configuration) : base(configuration)
        {
        }

        public double CreateCardNumber()
        {
            var cardPrefix = 565912;
            var randomSequence = new Random();
            var isCardValid = false;
            double cardNumberGenerated = 0;
            while (!isCardValid) 
            {
                var cardStringGenerated = cardPrefix.ToString() + randomSequence.Next(100000000, 999999999).ToString();
                _ = double.TryParse(cardStringGenerated, out cardNumberGenerated);
                isCardValid = !_context.Cards.Any(x => x.CardNumber == cardNumberGenerated);
            }
            return cardNumberGenerated;
        }
    }
}
