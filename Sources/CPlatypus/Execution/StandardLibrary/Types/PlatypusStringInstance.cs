using CPlatypus.Execution.Object;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusStringInstance : PlatypusInstance
    {
        public string Value { get; }

        public PlatypusStringInstance(string value, PlatypusSymbol currentSymbol, PlatypusContext parentContext) : base(
            currentSymbol.Get<PlatypusClassSymbol>("String"), parentContext)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}