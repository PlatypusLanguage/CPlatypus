using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class ExpressionExecutor : NodeExecutor
    {
        public static ExpressionExecutor Instance { get; } = new ExpressionExecutor();

        private ExpressionExecutor()
        {
        }

        public override PlatypusInstance Execute(PlatypusNode node, PlatypusContext currentContext,
            PlatypusSymbol currentSymbol)
        {
            if (node is IdentifierNode identifierNode)
            {
                return currentContext.Get(identifierNode.Value);
            }
            
            if (node is IntegerNode integerNode)
            {
                return currentSymbol.Get<PlatypusClassSymbol>("Integer").Get<PlatypusFunctionSymbol>("_constructor")
                    .Execute(currentContext, currentSymbol, integerNode.Value);
            }

            if (node is StringNode stringNode)
            {
                return currentSymbol.Get<PlatypusClassSymbol>("String").Get<PlatypusFunctionSymbol>("_constructor")
                    .Execute(currentContext, currentSymbol, stringNode.Value);
            }

            if (node is FunctionCallNode functionCallNode)
            {
                return FunctionExecutor.Instance.Execute(functionCallNode, currentContext, currentSymbol);
            }

            if (node is BinaryOperationNode binaryOperationNode)
            {
                return BinaryOperationExecutor.Instance.Execute(binaryOperationNode, currentContext, currentSymbol);
            }
            
            return PlatypusNullInstance.Instance;
        }

        public PlatypusContext ResolveContext(PlatypusNode node, PlatypusContext context)
        {
            if (node is IdentifierNode identifierNode)
            {
                if (context.ContainsLocal(identifierNode.Value))
                {
                    return context;
                }

                if (context.Parent is null)
                {
                    return null;
                }

                return ResolveContext(identifierNode, context.Parent);
            }

            if (node is AttributeAccessNode attributeAccessNode)
            {
                var ctx = ResolveContext(attributeAccessNode.Left, context);
                return ResolveContext(attributeAccessNode.Attribute, ctx);
            }

            return null;
        }

        public PlatypusContext ResolveObjectContext(PlatypusNode node, PlatypusContext context, PlatypusSymbol symbol)
        {
            if (node is IdentifierNode identifierNode)
            {
                if (context.Contains(identifierNode.Value))
                {
                    return context.Get(identifierNode.Value).InstanceContext;
                }

                return null;
            }

            if (node is FunctionCallNode functionCallNode)
            {
                return FunctionExecutor.Instance.Execute(functionCallNode, context, symbol).InstanceContext;
            }

            if (node is AttributeAccessNode attributeAccessNode)
            {
                return ResolveContext(attributeAccessNode.Attribute,
                    ResolveObjectContext(attributeAccessNode.Left, context, symbol));
            }

            return null;
        }
    }
}