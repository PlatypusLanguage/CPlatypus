using System.Collections.Generic;
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class FunctionExecutor : NodeExecutor
    {
        public static FunctionExecutor Instance { get; } = new FunctionExecutor();

        private FunctionExecutor()
        {
        }

        public override PlatypusInstance Execute(PlatypusNode node, PlatypusContext currentContext,
            PlatypusSymbol currentSymbol)
        {
            if (node is FunctionCallNode functionCallNode)
            {
                PlatypusContext executionContext;
                PlatypusSymbol executionSymbol;

                if (functionCallNode.HasTarget)
                {
                    executionContext =
                        ExpressionExecutor.Instance.ResolveObjectContext(functionCallNode.TargetNode, currentContext,
                            currentSymbol);
                    executionSymbol = currentSymbol;
                }
                else
                {
                    executionContext = currentContext;
                    executionSymbol = currentSymbol;
                }

                var functionContext = new PlatypusContext("Function Context", executionContext);

                if (executionContext != null)
                {
                    var functionSymbol =
                        executionSymbol.Get<PlatypusFunctionSymbol>(functionCallNode.FunctionName.Value);

                    var args = new List<object>();

                    if (functionSymbol.ExternFunction)
                    {
                        foreach (var arg in functionCallNode.ArgumentList.Arguments)
                        {
                            args.Add(ExpressionExecutor.Instance.Execute(arg, currentContext, currentSymbol));
                        }

                        return functionSymbol.Execute(functionContext, currentSymbol, args.ToArray());
                    }
                    else
                    {
                        for (var i = 0; i < functionSymbol.FunctionNode.ParameterList.Parameters.Count; i++)
                        {
                            var argumentValue = ExpressionExecutor.Instance.Execute(
                                functionCallNode.ArgumentList.Arguments[i], currentContext, currentSymbol);
                            var name = functionSymbol.FunctionNode.ParameterList.Parameters[i].Value;
                            functionContext.Add(name, argumentValue);
                        }

                        return BodyExecutor.Instance.Execute(functionSymbol.FunctionNode.Body, currentContext,
                            currentSymbol);
                    }
                }
            }

            return PlatypusNullInstance.Instance; // Should never happen
        }
    }
}