namespace DesignPatternChallenge.Interpreter
{
    /// <summary>
    /// The AbstractExpression defines the interface for executing an operation.
    /// </summary>
    /// <typeparam name="T">The type of the result of the expression evaluation.</typeparam>
    public interface IExpression<out T>
    {
        /// <summary>
        /// Interprets the expression given the specified context.
        /// </summary>
        /// <param name="context">The context containing information about the shopping cart.</param>
        /// <returns>The result of the interpretation.</returns>
        T Interpret(Context context);
    }
}
