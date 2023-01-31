using RapidPay.Test.DataAccess;
using RapidPay.Test.Models.Domain;
using RapidPay.Test.Services.Interfaces;

namespace RapidPay.Test.Services
{
    public class FeeService : Repository<Fee>, IFeeService
    {
        public FeeService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<double> GetCurrentFeeExchange(double payment)
        {
            try
            {
                await Task.Delay(1000); // Fake API call

                var latestFee = _context.Fees.Where(x => true).OrderBy(x => x.DateCreated).FirstOrDefault();
                decimal randomDecimal;
                if(latestFee == null || latestFee.DateCreated <= DateTime.Now.AddHours(-1)) 
                {
                    var random = new Random();
                    randomDecimal = (decimal)random.NextDouble() * 2;
                    _context.Fees.Add(new Fee { DateCreated = DateTime.Now, FeeAmmount = (double)randomDecimal});
                } else
                {
                    randomDecimal = (decimal)latestFee.FeeAmmount;
                }
               
                var currentFee = (decimal)payment * randomDecimal;
                return (double)currentFee;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
