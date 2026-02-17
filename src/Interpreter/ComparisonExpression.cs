using System;

namespace DesignPatternChallenge.Interpreter
{
    public enum ComparisonOperator
    {
        GreaterThan,
        LessThan,
        Equal,
        NotEqual,
        GreaterThanOrEqual,
        LessThanOrEqual
    }

    /// <summary>
    /// Non-Terminal Expression for comparing two values.
    /// </summary>
    public class ComparisonExpression<T> : IExpression<bool> where T : IComparable
    {
        private readonly IExpression<T> _left;
        private readonly IExpression<T> _right;
        private readonly ComparisonOperator _operator;

        public ComparisonExpression(IExpression<T> left, IExpression<T> right, ComparisonOperator @operator)
        {
            _left = left;
            _right = right;
            _operator = @operator;
        }

        public bool Interpret(Context context)
        {
            var leftVal = _left.Interpret(context);
            var rightVal = _right.Interpret(context);

            var comparison = leftVal.CompareTo(rightVal);

            return _operator switch
            {
                ComparisonOperator.GreaterThan => comparison > 0,
                ComparisonOperator.LessThan => comparison < 0,
                ComparisonOperator.Equal => comparison == 0,
                ComparisonOperator.NotEqual => comparison != 0,
                ComparisonOperator.GreaterThanOrEqual => comparison >= 0,
                ComparisonOperator.LessThanOrEqual => comparison <= 0,
                _ => false
            };
        }
    }
}
