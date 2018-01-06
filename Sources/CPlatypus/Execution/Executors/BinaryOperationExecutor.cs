using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class BinaryOperationExecutor : NodeExecutor
    {
        public static BinaryOperationExecutor Instance { get; } = new BinaryOperationExecutor();

        private BinaryOperationExecutor()
        {
        }

        public override PlatypusInstance Execute(PlatypusNode node, PlatypusContext currentContext,
            PlatypusSymbol currentSymbol)
        {
            if (node is BinaryOperationNode binaryOperationNode)
            {
                var left = binaryOperationNode.Left;
                var right = binaryOperationNode.Right;

                if (binaryOperationNode.OperationType == BinaryOperation.Assignment)
                {
                    var ctx = ExpressionExecutor.Instance.ResolveContext(left, currentContext);
                    var name = "";
                    if (left is IdentifierNode identifierNodeName)
                    {
                        name = identifierNodeName.Value;
                    }
                    else if (left is BinaryOperationNode binaryOperationNodeName)
                    {
                        name = ((IdentifierNode) binaryOperationNodeName.Right).Value;
                    }

                    return ctx.Set(name,
                        ExpressionExecutor.Instance.Execute(right, currentContext, currentSymbol));
                }

                if (binaryOperationNode.OperationType == BinaryOperation.Addition)
                {
                    var l = ExpressionExecutor.Instance.Execute(left, currentContext, currentSymbol);
                    var r = ExpressionExecutor.Instance.Execute(right, currentContext, currentSymbol);
                    return l.Symbol.Get<PlatypusFunctionSymbol>("_addition")
                        .Execute(currentContext, currentSymbol, l, r);
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}