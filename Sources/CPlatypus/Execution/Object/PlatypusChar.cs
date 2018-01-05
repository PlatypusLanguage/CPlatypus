namespace CPlatypus.Execution.Object
{
    public class PlatypusChar : PlatypusObject
    {
        public char Value { get; }

        public PlatypusChar(char value, Context enclosingContext) : base(enclosingContext)
        {
            Value = value;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}