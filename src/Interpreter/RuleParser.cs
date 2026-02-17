using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternChallenge.Interpreter
{
    /// <summary>
    /// A simple parser to convert rule strings into IExpression trees.
    /// Supports: IF, THEN, E (AND), OU (OR), >, <, =, !=, >=, <=
    /// </summary>
    public class RuleParser
    {
        public static IExpression<decimal> Parse(string rule)
        {
            // Simple parsing logic: "condition THEN discount"
            // Example: "itemQuantity > 10 AND totalValue > 1000 THEN 15"

            var parts = rule.Split(new[] { "THEN" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                throw new ArgumentException("Invalid rule format. Expected: <condition> THEN <discount>");

            var conditionPart = parts[0].Trim();
            var discountPart = parts[1].Trim();

            var conditionExpr = ParseCondition(conditionPart);
            var discountValue = decimal.Parse(discountPart);
            var discountExpr = new ConstantExpression<decimal>(discountValue);

            return new DiscountActionExpression(conditionExpr, discountExpr);
        }

        private static IExpression<bool> ParseCondition(string condition)
        {
            // Handle E (AND) - very basic split (does not handle precedence correctly without a real lexer, but fits the challenge)
            if (condition.Contains(" AND "))
            {
                var parts = condition.Split(new[] { " AND " }, 2, StringSplitOptions.None);
                return new LogicalExpression(ParseCondition(parts[0]), ParseCondition(parts[1]), LogicalOperator.And);
            }

            if (condition.Contains(" OR "))
            {
                var parts = condition.Split(new[] { " OR " }, 2, StringSplitOptions.None);
                return new LogicalExpression(ParseCondition(parts[0]), ParseCondition(parts[1]), LogicalOperator.Or);
            }

            // Handle Single Comparison
            return ParseComparison(condition);
        }

        private static IExpression<bool> ParseComparison(string expression)
        {
            var operators = new Dictionary<string, ComparisonOperator>
            {
                { ">=", ComparisonOperator.GreaterThanOrEqual },
                { "<=", ComparisonOperator.LessThanOrEqual },
                { ">", ComparisonOperator.GreaterThan },
                { "<", ComparisonOperator.LessThan },
                { "=", ComparisonOperator.Equal },
                { "!=", ComparisonOperator.NotEqual }
            };

            foreach (var op in operators)
            {
                if (expression.Contains(op.Key))
                {
                    var parts = expression.Split(new[] { op.Key }, StringSplitOptions.None);
                    var left = parts[0].Trim();
                    var right = parts[1].Trim();

                    // Detect types (string vs number)
                    if (decimal.TryParse(right, out decimal numValue))
                    {
                        return new ComparisonExpression<decimal>(
                            new VariableExpression<decimal>(left),
                            new ConstantExpression<decimal>(numValue),
                            op.Value);
                    }
                    else if (bool.TryParse(right, out bool boolValue))
                    {
                        return new ComparisonExpression<bool>(
                            new VariableExpression<bool>(left),
                            new ConstantExpression<bool>(boolValue),
                            op.Value);
                    }
                    else
                    {
                        return new ComparisonExpression<string>(
                            new VariableExpression<string>(left),
                            new ConstantExpression<string>(right.Trim('\'', '\"')),
                            op.Value);
                    }
                }
            }

            throw new ArgumentException($"Unsupported comparison: {expression}");
        }
    }
}
