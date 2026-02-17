namespace DesignPatternChallenge.Interpreter
{
    /// <summary>
    /// Terminal Expression that retrieves a value from the context by name.
    /// </summary>
    public class VariableExpression<T> : IExpression<T>
    {
        private readonly string _name;

        public VariableExpression(string name)
        {
            _name = name;
        }

        public T Interpret(Context context)
        {
            var value = context.GetValue(_name);
            if (value == null)
            {
                throw new System.Exception($"Property '{_name}' not found in context.");
            }

            return (T)System.Convert.ChangeType(value, typeof(T));
        }
    }
}
