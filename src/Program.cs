using System;
using System.Collections.Generic;
using DesignPatternChallenge.Models;
using DesignPatternChallenge.Interpreter;

namespace DesignPatternChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Interpreter ===");
            Console.WriteLine("=== Strategic Discount Rule Engine ===\n");

            var carts = new List<ShoppingCart>
            {
                new ShoppingCart { TotalValue = 1500.00m, ItemQuantity = 15, CustomerCategory = "Regular", IsFirstPurchase = false },
                new ShoppingCart { TotalValue = 500.00m, ItemQuantity = 5, CustomerCategory = "VIP", IsFirstPurchase = false },
                new ShoppingCart { TotalValue = 200.00m, ItemQuantity = 2, CustomerCategory = "Regular", IsFirstPurchase = true }
            };

            var ruleStrings = new List<string>
            {
                "itemQuantity > 10 AND totalValue > 1000 THEN 15",
                "customerCategory = VIP THEN 20",
                "isFirstPurchase = true THEN 10"
            };

            // Parse rules once (Modern Strategy: Optimization)
            var compiledRules = new List<IExpression<decimal>>();
            foreach (var ruleText in ruleStrings)
            {
                compiledRules.Add(RuleParser.Parse(ruleText));
            }

            int cartIndex = 1;
            foreach (var cart in carts)
            {
                Console.WriteLine($"--- Processing Cart {cartIndex++} ---");
                Console.WriteLine($"Value: ${cart.TotalValue}, Quantity: {cart.ItemQuantity}, Category: {cart.CustomerCategory}, First: {cart.IsFirstPurchase}");

                var context = new Context(cart);
                decimal maxDiscount = 0;

                foreach (var expression in compiledRules)
                {
                    decimal discount = expression.Interpret(context);
                    if (discount > 0)
                    {
                        Console.WriteLine($"[APPLIED] Rule resulted in {discount}% discount.");
                        maxDiscount = Math.Max(maxDiscount, discount);
                    }
                }

                Console.WriteLine($"Final Applicable Discount: {maxDiscount}%\n");
            }

            // "=== Reflection Questions Answered ===",
            // "1. How to interpret language grammar? By mapping syntax elements to Expression classes.",
            // "2. How to represent expressions as a syntax tree? Using objects that reference other expressions (Non-Terminal)."
            // "3. How to evaluate expressions recursively? Each Interpret() call triggers Interpret() on child expressions."
            // "4. How to create an extensible DSL? By adding new IExpression implementations without changing core engine."   
        }
    }
}
