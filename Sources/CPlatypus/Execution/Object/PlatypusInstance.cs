using CPlatypus.Semantic;

namespace CPlatypus.Execution.Object
{
    public class PlatypusInstance
    {
        public PlatypusClassSymbol Symbol { get; }
        
        public PlatypusContext InstanceContext { get; }

        public PlatypusInstance(PlatypusClassSymbol symbol, PlatypusContext parentContext)
        {
            Symbol = symbol;
            InstanceContext = new PlatypusContext(symbol is null ? "c_null" : "c_" + symbol.Name, parentContext);
        }
    }
}