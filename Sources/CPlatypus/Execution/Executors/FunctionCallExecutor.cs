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
using System.Linq;
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Execution;
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class FunctionCallExecutor : PlatypusNodeExecutor
    {
        public static FunctionCallExecutor Instance { get; } = new FunctionCallExecutor();

        private FunctionCallExecutor()
        {
        }

        public PlatypusInstance Execute(PlatypusFunctionSymbol functionSymbol, Context currentContext,
            object[] args, PlatypusInstance objectInstance = null)
        {
            if (functionSymbol.ExternFunction)
            {
                var functionContext = new PlatypusContext("Function Context", currentContext);

                var argsDictionary = new Dictionary<string, object>();

                var parameters = functionSymbol.IsConstructor
                    ? functionSymbol.ConstructorNode.Parameters.Parameters.Select(p => p.Value).ToList()
                    : functionSymbol.FunctionTarget.Parameters;

                for (var i = 0; i < parameters.Count; i++)
                {
                    var name = parameters[i];
                    var argumentValue = args[i];
                    argsDictionary.Add(name, argumentValue);
                }

                if (objectInstance != null)
                {
                    argsDictionary.Add("this", objectInstance);
                }
                
                return functionSymbol.FunctionTarget.Execute(functionContext, functionSymbol, argsDictionary);
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext,
            Symbol currentSymbol)
        {
            if (node is FunctionCallNode functionCallNode)
            {
                Context executionContext;
                Symbol executionSymbol;

                PlatypusInstance targetObject = null;

                if (functionCallNode.HasTarget)
                {
                    targetObject = ExpressionExecutor.Instance.ResolveObject(
                        functionCallNode.TargetNode, currentContext, currentSymbol);
                    executionContext = targetObject.Context;
                    executionSymbol = targetObject.Symbol;
                }
                else
                {
                    executionContext = currentContext;
                    executionSymbol = currentSymbol;
                }

                if (executionContext != null)
                {
                    var functionSymbol =
                        executionSymbol.Get<PlatypusFunctionSymbol>(functionCallNode.FunctionName.Value);

                    if (functionSymbol != null)
                    {
                        if (functionSymbol.ExternFunction)
                        {
                            /*var args = new Dictionary<string, object>();

                            for (var i = 0; i < functionSymbol.FunctionTarget.Parameters.Count; i++)
                            {
                                var argumentValue = ExpressionExecutor.Instance.Execute(
                                    functionCallNode.ArgumentList.Arguments[i], currentContext, currentSymbol);
                                var name = functionSymbol.FunctionNode.ParameterList.Parameters[i].Value;
                                args.Add(name, argumentValue);
                            }*/

                            var args = new List<object>();

                            foreach (var arg in functionCallNode.ArgumentList.Arguments)
                            {
                                args.Add(ExpressionExecutor.Instance.Execute(arg, currentContext, currentSymbol));
                            }

                            return Execute(functionSymbol, currentContext, args.ToArray(), targetObject);
                        }
                        else
                        {
                            var functionContext = new PlatypusContext("Function Context", executionContext);

                            for (var i = 0; i < functionSymbol.FunctionNode.ParameterList.Parameters.Count; i++)
                            {
                                var argumentValue = ExpressionExecutor.Instance.Execute(
                                    functionCallNode.ArgumentList.Arguments[i], currentContext, currentSymbol);
                                var name = functionSymbol.FunctionNode.ParameterList.Parameters[i].Value;
                                functionContext.Add(name, argumentValue);
                            }

                            return BodyExecutor.Instance.Execute(functionSymbol.FunctionNode.Body, functionContext,
                                functionSymbol);
                        }
                    }
                }
            }

            return PlatypusNullInstance.Instance; // Should never happen
        }
    }
}