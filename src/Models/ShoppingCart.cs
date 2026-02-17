namespace DesignPatternChallenge.Models
{
    /// <summary>
    /// Represents a shopping cart in the e-commerce system.
    /// Used as the data source for rule evaluation.
    /// </summary>
    public class ShoppingCart
    {
        public decimal TotalValue { get; set; }
        public int ItemQuantity { get; set; }
        public string CustomerCategory { get; set; } = string.Empty;
        public bool IsFirstPurchase { get; set; }
    }
}
