using System.Runtime.CompilerServices;
using CPlatypus.Core;
using CPlatypus.Execution.Object;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusString : PlatypusClass
    {
        public PlatypusString() : base("String")
        {
        }

        [PlatypusFunction("_constructor")]
        public override PlatypusInstance Constructor(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args)
        {
            var value = args.Join();
            return new PlatypusStringInstance(value, currentSymbol, currentContext);
        }

        [PlatypusFunction("_addition")]
        public override PlatypusInstance Addition(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args)
        {
            var left = (PlatypusInstance) args[0];
            var right = (PlatypusInstance) args[1];

            return new PlatypusStringInstance(
                ((PlatypusStringInstance) left.Symbol.Get<PlatypusFunctionSymbol>("_tostring")
                    .Execute(currentContext, currentSymbol, left)).Value +
                ((PlatypusStringInstance) right.Symbol.Get<PlatypusFunctionSymbol>("_tostring")
                    .Execute(currentContext, currentSymbol, right)).Value,
                currentSymbol, currentContext);
        }

        [PlatypusFunction("_tostring")]
        public override PlatypusStringInstance ToStringInstance(PlatypusContext currentContext,
            PlatypusSymbol currentSymbol,
            params object[] args)
        {
            if (args[0] is PlatypusStringInstance platypusStringInstance)
            {
                return platypusStringInstance;
            }

            return new PlatypusStringInstance("", currentSymbol, currentContext);
        }
    }
}