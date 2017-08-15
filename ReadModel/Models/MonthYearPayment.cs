namespace ReadModel.Models
{
    public class MonthYearPayment
    {
        decimal AmountPaid { get; set; }

        public MonthYearPayment(decimal amountPaid)
        {
            AmountPaid = amountPaid;
        }

        public void AddPayment(decimal amountPaid)
        {
            AmountPaid += amountPaid;
        }
    }
}
