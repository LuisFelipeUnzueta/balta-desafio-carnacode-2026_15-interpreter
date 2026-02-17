namespace DesignPatternChallenge.Interpreter
{
    /// <summary>
    /// Terminal Expression for constant values (numbers, strings, etc.).
    /// </summary>
    public class ConstantExpression<T> : IExpression<T>
    {
        private readonly T _value;

        public ConstantExpression(T value)
        {
            _value = value;
        }

        public T Interpret(Context context)
        {
            return _value;
        }
    }
}
