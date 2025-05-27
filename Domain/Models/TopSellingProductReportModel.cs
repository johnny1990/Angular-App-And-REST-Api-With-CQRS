namespace Domain.Models
{
    public class TopSellingProductReportModel
    {
        public string ProductName { get; set; } = default!;
        public int UnitsSold { get; set; }
        public decimal TotalSales { get; set; }
    }
}
