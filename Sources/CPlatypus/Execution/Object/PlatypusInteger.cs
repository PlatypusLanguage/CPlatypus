namespace CPlatypus.Execution.Object
{
    public class PlatypusInteger : PlatypusObject
    {
        public int Value { get; }

        public PlatypusInteger(int value, Context enclosingContext) : base(enclosingContext)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}