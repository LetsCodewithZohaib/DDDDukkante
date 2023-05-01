namespace Dukkantek.Inventory.Application.Products.Models.Results
{
    public class GetProductCountQueryResult
    {
        public int Sold { get; set; }
        public int Damaged { get; set; }
        public int InStock { get; set; }
    }
}
