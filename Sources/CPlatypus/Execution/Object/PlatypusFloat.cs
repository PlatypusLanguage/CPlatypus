using System.Globalization;

namespace CPlatypus.Execution.Object
{
    public class PlatypusFloat : PlatypusObject
    {
        public float Value { get; }

        public PlatypusFloat(float value, Context enclosingContext) : base(enclosingContext)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}