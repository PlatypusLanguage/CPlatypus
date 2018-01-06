using System;
using CPlatypus.Core;
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.IO
{
    public class PlatypusPrintFunction : PlatypusFunction
    {
        public PlatypusPrintFunction() : base("print", "Print")
        {
        }
        
        public PlatypusInstance Print(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args)
        {
            Console.WriteLine(args.Join());
            return PlatypusNullInstance.Instance;
        }
    }
}