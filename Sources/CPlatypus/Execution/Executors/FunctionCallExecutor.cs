/*
 * Copyright (c) 2018 Platypus Language http://platypus.vfrz.fr/
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

using System;
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
        public PlatypusInstance Execute(PlatypusFunctionSymbol functionSymbol, Context currentContext, object[] args, PlatypusInstance objectInstance = null)
        {
            if (functionSymbol.ExternFunction)
            {
                var functionContext = new PlatypusContext(PlatypusContextType.Function, currentContext);

                var argsDictionary = new Dictionary<string, object>();

                var parameters = functionSymbol.FunctionTarget.Parameters;

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
            else
            {
                var argsDictionary = new Dictionary<string, object>();

                var parameters = functionSymbol.FunctionNode is IParameterizedNode parameterizedNode ? parameterizedNode.ParameterList.Parameters.Select(p => p.Value).ToList() : new List<string>();

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

                return Execute(functionSymbol.FunctionNode, currentContext, functionSymbol);
            }
        }

        private PlatypusInstance ExecuteInternal(IBodiedNode node, Context currentContext, Symbol functionSymbol)
        {
            var functionContext = new PlatypusContext(PlatypusContextType.Function, currentContext);

            
            

            throw new NotImplementedException();
            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext, Symbol currentSymbol)
        {
            var executionContext = currentContext;
            var executionSymbol = currentSymbol;

            if (node is FunctionCallNode functionCallNode)
            {
                var expressionExecutor = new ExpressionExecutor();

                PlatypusInstance targetObject = null;

                if (functionCallNode.HasTarget)
                {
                    targetObject = expressionExecutor.ResolveInstance(functionCallNode.TargetNode, currentContext, currentSymbol);
                    executionContext = targetObject.Context;
                    executionSymbol = targetObject.Symbol;
                }

                var functionSymbol = executionSymbol.Get<PlatypusFunctionSymbol>(functionCallNode.FunctionName.Value);

                if (functionSymbol != null)
                {
                    if (functionSymbol.ExternFunction)
                    {
                        var args = new List<object>();

                        foreach (var arg in functionCallNode.ArgumentList.Arguments)
                        {
                            args.Add(expressionExecutor.Execute(arg, currentContext, currentSymbol));
                        }

                        return Execute(functionSymbol, currentContext, args.ToArray(), targetObject);
                    }
                    else
                    {
                        var functionContext = new PlatypusContext(PlatypusContextType.Function, executionContext);

                        if (functionSymbol.FunctionNode is IParameterizedNode parameterizedNode)
                        {
                            for (var i = 0; i < parameterizedNode.ParameterList.Parameters.Count; i++)
                            {
                                var argumentValue = expressionExecutor.Execute(functionCallNode.ArgumentList.Arguments[i], currentContext, currentSymbol);
                                var name = parameterizedNode.ParameterList.Parameters[i].Value;
                                functionContext.Add(name, argumentValue);
                            }
                        }

                        if (functionSymbol.FunctionNode is IBodiedNode bodiedNode)
                        {
                            return new BodyExecutor().Execute(bodiedNode.Body, functionContext, functionSymbol);
                        }
                    }
                }
            }

            return PlatypusNullInstance.Instance; // Should never happen
        }
    }
}