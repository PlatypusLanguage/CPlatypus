﻿/*
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

using CPlatypus.Core;
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Execution;
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class BinaryOperationExecutor : PlatypusNodeExecutor
    {
        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext, Symbol currentSymbol)
        {
            if (node is BinaryOperationNode binaryOperationNode)
            {
                var leftNode = binaryOperationNode.Left;
                var rightNode = binaryOperationNode.Right;

                if (binaryOperationNode.OperatorType == BinaryOperator.Assign)
                {
                    var expressionExecutor = new ExpressionExecutor();
                    
                    var ctx = expressionExecutor.ResolveContext(leftNode, currentContext);
                    var name = "";
                    if (leftNode is IdentifierNode identifierNodeName)
                    {
                        name = identifierNodeName.Value;
                    }
                    else if (leftNode is BinaryOperationNode binaryOperationNodeName)
                    {
                        name = ((IdentifierNode) binaryOperationNodeName.Right).Value;
                    }

                    return ctx.Set(name, expressionExecutor.Execute(rightNode, currentContext, currentSymbol));
                }

                var op = binaryOperationNode.OperatorType;

                if (op == BinaryOperator.Plus || op == BinaryOperator.Minus ||
                    op == BinaryOperator.Divide || op == BinaryOperator.Multiply ||
                    op == BinaryOperator.Equal || op == BinaryOperator.Greater ||
                    op == BinaryOperator.Less || op == BinaryOperator.GreaterEqual ||
                    op == BinaryOperator.LessEqual)
                {
                    var expressionExecutor = new ExpressionExecutor();
                    
                    var leftValue = expressionExecutor.Execute(leftNode, currentContext, currentSymbol);
                    var rightValue = expressionExecutor.Execute(rightNode, currentContext, currentSymbol);

                    var functionSymbol = leftValue.Symbol.Get<PlatypusFunctionSymbol>(op.ToOperatorCode());
                    
                    return new FunctionCallExecutor().Execute(functionSymbol, currentContext, new[] {rightValue}, leftValue);
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}