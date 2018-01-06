using System;
using CPlatypus.Core;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Object
{
    public class PlatypusFunction
    {
        public string Name { get; }

        private readonly Delegate _delegateFunction;
        
        public PlatypusFunction(string name, Delegate delegateFunction)
        {
            Name = name;
            _delegateFunction = delegateFunction;
        }

        public PlatypusFunction(string name, string realName)
        {
            Name = name;
            _delegateFunction = GetType().GetMethod(realName).CreateDelegate(this);
        }

        public PlatypusInstance Execute(PlatypusContext currentContext, PlatypusSymbol currentSymbol, params object[] args)
        {          
            return (PlatypusInstance) _delegateFunction.DynamicInvoke(currentContext, currentSymbol, args);
        }
        
        public PlatypusFunctionSymbol ToSymbol(PlatypusSymbol parent)
        {
            return new PlatypusFunctionSymbol(this, parent);
        }
    }
}