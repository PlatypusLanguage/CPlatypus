using System;
using CPlatypus.Execution;
using CPlatypus.Execution.Executors;
using CPlatypus.Execution.Object;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Semantic
{
    public class PlatypusFunctionSymbol : PlatypusSymbol
    {
        public FunctionNode FunctionNode { get; }

        private PlatypusFunction FunctionTarget { get; }

        public bool ExternFunction => FunctionNode is null;

        public PlatypusFunctionSymbol(FunctionNode functionNode, PlatypusSymbol parent) : base(parent)
        {
            Name = functionNode.Name.Value;
            FunctionNode = functionNode;
        }

        public PlatypusFunctionSymbol(PlatypusFunction functionTarget, PlatypusSymbol parent) : base(parent)
        {
            Name = functionTarget.Name;
            FunctionTarget = functionTarget;
        }

        public PlatypusInstance Execute(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args)
        {
            if (ExternFunction)
            {
                return FunctionTarget.Execute(currentContext, currentSymbol, args);
            }

            return FunctionExecutor.Instance.Execute(FunctionNode, currentContext, currentSymbol);
        }
    }
}