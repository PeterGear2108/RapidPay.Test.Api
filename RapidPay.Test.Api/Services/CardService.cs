using RapidPay.Test.DataAccess;
using RapidPay.Test.Models.Domain;
using RapidPay.Test.Models.Dto;
using RapidPay.Test.Services.Helpers;
using RapidPay.Test.Services.Interfaces;

namespace RapidPay.Test.Services
{
    public class CardService : Repository<Card>, ICardService
    {
        private readonly IBinService _binService;
        private readonly IFeeService _feeService;

        public CardService(
            IConfiguration configuration,
            IBinService binService,
            IFeeService feeService
         ) : base(configuration)
        {
            _binService = binService;
            _feeService = feeService;
        }

        public ServiceResponse<Card> CreateCard(CardDto card)
        {
            var sr = new ServiceResponse<Card>();
            try
            {
                var newCard = new Card();
                var newCardNumber = _binService.CreateCardNumber();
                newCard.CardHolder = card.CardHolder;
                newCard.CreditLimit= card.CreditLimit;
                newCard.CardNumber = newCardNumber;
                AddEntity(newCard);
                SaveChanges();
                sr.Data = newCard;
            }
            catch (Exception e)
            {
               sr.Result = e.Message; return sr;
            }
            return sr;
        }

        public double GetCardBalance(double cardNumber)
        {
            try
            {
                return _context.Cards.FirstOrDefault(x => x.CardNumber == cardNumber)?.CurrentBalance ?? 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServiceResponse<Card> PayWithCard(CardTransactionDto cardTransaction)
        {
            var sr = new ServiceResponse<Card>();
            try
            {
                var cardSelected = _context.Cards.FirstOrDefault(x => x.CardNumber == cardTransaction.CardNumber);
                if (cardSelected == null)
                    throw new KeyNotFoundException("The card number does not exist");

                var balance = cardSelected.CurrentBalance + cardTransaction.PaymentAmmount;
                cardSelected.CurrentBalance = balance;
                if (balance > cardSelected.CreditLimit)
                    throw new NotSupportedException("Credit limit is not enough for complete the transaction");

                var feeApplied = _feeService.GetCurrentFeeExchange(cardTransaction.PaymentAmmount).Result;
                
                _context.Transactions.Add(new Transaction {
                    CardId = cardSelected.Id, 
                    Payment = cardTransaction.PaymentAmmount, 
                    Fee = (feeApplied - cardTransaction.PaymentAmmount * -1) 
                });
                                
                SaveChanges();
                sr.Data = cardSelected;
            }
            catch (Exception e)
            {
                sr.Result = e.Message; return sr;
            }
            return sr;
        }
    }
}