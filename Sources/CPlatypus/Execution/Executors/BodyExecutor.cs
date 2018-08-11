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

using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Execution;
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Execution.Executors
{
    public class BodyExecutor : PlatypusNodeExecutor
    {
        public bool HasReturnedValue { get; private set; }
        
        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext,
            Symbol currentSymbol)
        {
            HasReturnedValue = false;
            if (node is CodeNode codeNode)
            {
                foreach (var childNode in codeNode.Children)
                {
                    if (childNode is VariableDeclarationNode)
                    {
                        new VariableDeclarationExecutor().Execute(childNode, currentContext, currentSymbol);
                    }
                    else if (childNode is BinaryOperationNode)
                    {
                        new BinaryOperationExecutor().Execute(childNode, currentContext, currentSymbol);
                    }
                    else if (childNode is FunctionCallNode)
                    {
                        new FunctionCallExecutor().Execute(childNode, currentContext, currentSymbol);
                    }
                    else if (childNode is NewNode)
                    {
                        new NewExecutor().Execute(childNode, currentContext, currentSymbol);
                    } 
                    else if (childNode is IfNode)
                    {
                        var executor = new IfExecutor();
                        var result = executor.Execute(childNode, currentContext, currentSymbol);
                        if (executor.HasReturnedValue)
                        {
                            return result;
                        }
                    }
                    else if (childNode is WhileNode)
                    {
                        var executor = new WhileExecutor();
                        var result = executor.Execute(childNode, currentContext, currentSymbol);
                        if (executor.HasReturnedValue)
                        {
                            return result;
                        }
                    }
                    else if (childNode is ReturnNode returnNode)
                    {
                        HasReturnedValue = true;
                        return new ExpressionExecutor().Execute(returnNode.Expression, currentContext, currentSymbol);
                    }
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}