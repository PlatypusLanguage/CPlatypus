using System;
using CPlatypus.Execution.Object;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusInteger : PlatypusClass
    {
        public PlatypusInteger() : base("Integer")
        {
        }

        [PlatypusFunction("_constructor")]
        public override PlatypusInstance Constructor(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args)
        {
            var value = args.Length > 0 ? Convert.ToInt32(args[0]) : 0;
            return new PlatypusIntegerInstance(value, currentSymbol, currentContext);
        }

        [PlatypusFunction("_addition")]
        public override PlatypusInstance Addition(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args)
        {
            var left = args[0];
            var right = args[1];

            if (left is PlatypusIntegerInstance leftInteger && right is PlatypusIntegerInstance rightInteger)
            {
                return new PlatypusIntegerInstance(leftInteger.Value + rightInteger.Value,
                    currentSymbol, currentContext);
            }

            return PlatypusNullInstance.Instance;
        }

        [PlatypusFunction("_tostring")]
        public override PlatypusStringInstance ToStringInstance(PlatypusContext currentContext,
            PlatypusSymbol currentSymbol,
            params object[] args)
        {
            if (args[0] is PlatypusIntegerInstance platypusIntegerInstance)
            {
                return new PlatypusStringInstance(platypusIntegerInstance.ToString(),
                    currentSymbol, currentContext);
            }

            return new PlatypusStringInstance("", currentSymbol, currentContext);
        }
    }
}