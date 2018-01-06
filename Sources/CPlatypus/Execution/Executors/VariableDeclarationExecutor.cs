using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class VariableDeclarationExecutor : NodeExecutor
    {
        public static VariableDeclarationExecutor Instance { get; } = new VariableDeclarationExecutor();

        private VariableDeclarationExecutor()
        {
        }

        public override PlatypusInstance Execute(PlatypusNode node, PlatypusContext currentContext,
            PlatypusSymbol currentSymbol)
        {
            if (node is VariableDeclarationNode variableDeclarationNode)
            {
                currentContext.Add(variableDeclarationNode.VariableNameNode.Value);
                return currentContext.GetLocal(variableDeclarationNode.VariableNameNode.Value);
            }

            return PlatypusNullInstance.Instance;
        }
    }
}