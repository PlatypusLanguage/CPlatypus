using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class BodyExecutor : NodeExecutor
    {
        public static BodyExecutor Instance { get; } = new BodyExecutor();

        private BodyExecutor()
        {
        }

        public override PlatypusInstance Execute(PlatypusNode node, PlatypusContext currentContext,
            PlatypusSymbol currentSymbol)
        {
            if (node is CodeNode codeNode)
            {
                foreach (var n in codeNode.Children)
                {
                    if (n is VariableDeclarationNode)
                    {
                        VariableDeclarationExecutor.Instance.Execute(n, currentContext, currentSymbol);
                    }
                    else if (n is BinaryOperationNode)
                    {
                        BinaryOperationExecutor.Instance.Execute(n, currentContext, currentSymbol);
                    }
                    else if (n is ReturnNode returnNode)
                    {
                        return ExpressionExecutor.Instance.Execute(returnNode.Expression, currentContext, currentSymbol);
                    }
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}