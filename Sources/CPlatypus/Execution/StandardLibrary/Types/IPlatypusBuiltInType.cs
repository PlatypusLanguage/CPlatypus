using CPlatypus.Execution.Object;
using CPlatypus.Framework.Semantic;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public interface IPlatypusBuiltInType<T>
    {
        PlatypusInstance Create(PlatypusContext currentContext, Symbol currentSymbol, T value);
    }
}