/*
 * Copyright (c) 2017 Platypus Language http://platypus.vfrz.fr/
 *  This file is part of CPlatypus.
 *
 *     CPlatypus is free software: you can redistribute it and/or modify
 *     it under the terms of the GNU General Public License as published by
 *     the Free Software Foundation, either version 3 of the License, or
 *     (at your option) any later version.
 *
 *     CPlatypus is distributed in the hope that it will be useful,
 *     but WITHOUT ANY WARRANTY; without even the implied warranty of
 *     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *     GNU General Public License for more details.
 *
 *     You should have received a copy of the GNU General Public License
 *     along with CPlatypus.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Semantic;
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
            Symbol currentSymbol)
        {
            if (node is FunctionCallNode functionCallNode)
            {
                PlatypusContext executionContext;
                Symbol executionSymbol;

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