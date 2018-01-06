using CPlatypus.Execution.Object;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusIntegerInstance : PlatypusInstance
    {
        public int Value { get; }

        public PlatypusIntegerInstance(int value, PlatypusSymbol currentSymbol, PlatypusContext parentContext) : base(
            currentSymbol.Get<PlatypusClassSymbol>("Integer"), parentContext)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}