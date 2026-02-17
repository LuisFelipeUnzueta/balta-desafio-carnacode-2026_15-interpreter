namespace DesignPatternChallenge.Interpreter
{
    /// <summary>
    /// Non-Terminal Expression that evaluates a condition and returns a discount value if true.
    /// This represents the "IF condition THEN discount" structure.
    /// </summary>
    public class DiscountActionExpression : IExpression<decimal>
    {
        private readonly IExpression<bool> _condition;
        private readonly IExpression<decimal> _discount;

        public DiscountActionExpression(IExpression<bool> condition, IExpression<decimal> discount)
        {
            _condition = condition;
            _discount = discount;
        }

        public decimal Interpret(Context context)
        {
            if (_condition.Interpret(context))
            {
                return _discount.Interpret(context);
            }

            return 0;
        }
    }
}
