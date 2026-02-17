namespace DesignPatternChallenge.Interpreter
{
    public enum LogicalOperator
    {
        And,
        Or,
        Not
    }

    /// <summary>
    /// Non-Terminal Expression for logical operations (AND, OR, NOT).
    /// </summary>
    public class LogicalExpression : IExpression<bool>
    {
        private readonly IExpression<bool> _left;
        private readonly IExpression<bool>? _right;
        private readonly LogicalOperator _operator;

        public LogicalExpression(IExpression<bool> left, IExpression<bool>? right, LogicalOperator @operator)
        {
            _left = left;
            _right = right;
            _operator = @operator;
        }

        public bool Interpret(Context context)
        {
            return _operator switch
            {
                LogicalOperator.And => _left.Interpret(context) && (_right?.Interpret(context) ?? false),
                LogicalOperator.Or => _left.Interpret(context) || (_right?.Interpret(context) ?? false),
                LogicalOperator.Not => !_left.Interpret(context),
                _ => false
            };
        }
    }
}
