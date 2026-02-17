using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interpreter
{
    /// <summary>
    /// The Context contains information that is global to the interpreter.
    /// Here it wraps the ShoppingCart to provide access to its properties during evaluation.
    /// </summary>
    public class Context
    {
        public ShoppingCart Cart { get; }

        public Context(ShoppingCart cart)
        {
            Cart = cart;
        }

        /// <summary>
        /// Helper method to get a value from the cart by its name.
        /// This allows the interpreter to be more dynamic.
        /// </summary>
        public object? GetValue(string name)
        {
            return name.ToLower() switch
            {
                "totalvalue" or "valor" => Cart.TotalValue,
                "itemquantity" or "quantidade" => Cart.ItemQuantity,
                "customercategory" or "categoria" => Cart.CustomerCategory,
                "isfirstpurchase" or "primeiracompra" => Cart.IsFirstPurchase,
                _ => null
            };
        }
    }
}
