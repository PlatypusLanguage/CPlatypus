namespace CPlatypus.Execution.Object
{
    public class PlatypusBoolean : PlatypusObject
    {
        public bool Value { get; }

        public PlatypusBoolean(bool value, Context enclosingContext) : base(enclosingContext)
        {
            Value = value;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}