namespace RapidPay.Test.Models.Domain
{
    public class Transaction
    {
        public Transaction()
        {
            Card = new Card();
        }
        public int Id { get; set; }
        public int CardId { get; set; }
        public double Payment { get; set; }
        public double Fee { get; set; }

        #region nav props
        public Card Card { get; set; }
        #endregion
    }
}
