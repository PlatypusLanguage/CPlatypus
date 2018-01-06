using System;
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.IO
{
    public class PlatypusReadFunction : PlatypusFunction
    {
        public PlatypusReadFunction() : base("read", "Read")
        {
        }
        
        public PlatypusInstance Read(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args)
        {
            return new PlatypusStringInstance(Console.ReadLine(), currentSymbol, currentContext);
        }
    }
}