namespace RapidPay.Test.Models.Domain
{
    public class Card
    {
        public Card() 
        {
           CardHolder = string.Empty;
           Transactions = new HashSet<Transaction>();   
        }   
        public int Id { get; set; }
        public double CardNumber { get; set; }
        public string CardHolder { get; set; }
        public double CreditLimit { get; set; }
        public double CurrentBalance { get; set; }

        #region One to Many
        public ICollection<Transaction> Transactions { get; set; }
        #endregion
    }
}
