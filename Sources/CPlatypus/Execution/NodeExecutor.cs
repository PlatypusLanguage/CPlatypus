using CPlatypus.Execution.Object;
using CPlatypus.Parser;
using CPlatypus.Semantic;

namespace CPlatypus.Execution
{
    public abstract class NodeExecutor
    {
        public abstract PlatypusInstance Execute(PlatypusNode node, PlatypusContext currentContext,
            PlatypusSymbol currentSymbol);
    }
}