using CPlatypus.Execution.Object;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Execution.Executors
{
    public class BodyExecutor : NodeExecutor
    {
        public static BodyExecutor Instance { get; } = new BodyExecutor();

        private BodyExecutor()
        {
        }

        public override PlatypusObject Execute(PlatypusNode node, Context context)
        {
            if (node is CodeNode codeNode)
            {
                foreach (var n in codeNode.Children)
                {
                    if (n is VariableDeclarationNode)
                    {
                        VariableDeclarationExecutor.Instance.Execute(n, context);
                    }
                    else if (n is BinaryOperationNode)
                    {
                        BinaryOperationExecutor.Instance.Execute(n, context);
                    }
                    else if (n is ReturnNode returnNode)
                    {
                        return ExpressionExecutor.Instance.Execute(returnNode.Expression, context);
                    }
                }
            }
            return PlatypusNull.Instance;
        }
    }
}