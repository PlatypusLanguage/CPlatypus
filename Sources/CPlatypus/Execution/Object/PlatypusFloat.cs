namespace CPlatypus.Execution.Object
{
    public class PlatypusFloat : PlatypusObject
    {
        public float Value { get; }

        public PlatypusFloat(float value, Context enclosingContext) : base(enclosingContext)
        {
            Value = value;
        }
    }
}