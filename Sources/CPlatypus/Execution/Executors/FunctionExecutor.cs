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

using System;
using CPlatypus.Execution.Object;
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

        public override PlatypusObject Execute(PlatypusNode node, Context currentContext)
        {
            if (node is FunctionCallNode functionCallNode)
            {
                Context executionContext = null;

                switch (functionCallNode.Target)
                {
                    case IdentifierNode _:
                        executionContext = currentContext;
                        break;
                    case AttributeAccessNode attributeAccessNode:
                        executionContext =
                            ExpressionExecutor.Instance.ResolveObjectContext(attributeAccessNode.Left, currentContext);
                        break;
                }

                //TODO : REMOVE HACK
                if (((IdentifierNode) functionCallNode.Target).Value.Equals("print"))
                {
                    var ctx = new Context("Function Context", executionContext);
                    if (functionCallNode.ArgumentList.Arguments.Count > 0)
                    {
                        Console.WriteLine(ExpressionExecutor.Instance.Execute(functionCallNode.ArgumentList.Arguments[0], currentContext));
                    }

                    return null;
                }
                
                if (!(executionContext is null))
                {
                    var symbol = ExpressionExecutor.Instance.ResolveSymbol(functionCallNode.Target);
                    if (symbol is PlatypusFunctionSymbol functionSymbol)
                    {
                        var ctx = new Context("Function Context", executionContext);
                        for (var i = 0; i < functionSymbol.Node.ParameterList.Parameters.Count; i++)
                        {
                            var argumentValue = ExpressionExecutor.Instance.Execute(
                                functionCallNode.ArgumentList.Arguments[i], currentContext);

                            ctx.Add(functionSymbol.Node.ParameterList.Parameters[i].Value, argumentValue);
                        }

                        return BodyExecutor.Instance.Execute(functionSymbol.Node.Body, ctx);
                    }
                }
                else
                {
                    // TODO Throw error
                }
            }

            return PlatypusNull.Instance; // Should never happen
        }
    }
}