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
        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext,
            Symbol currentSymbol)
        {
            if (node is BinaryOperationNode binaryOperationNode)
            {
                var left = binaryOperationNode.Left;
                var right = binaryOperationNode.Right;

                if (binaryOperationNode.OperationType == BinaryOperation.Assignment)
                {
                    var ctx = new ExpressionExecutor().ResolveContext(left, currentContext);
                    var name = "";
                    if (left is IdentifierNode identifierNodeName)
                    {
                        name = identifierNodeName.Value;
                    }
                    else if (left is BinaryOperationNode binaryOperationNodeName)
                    {
                        name = ((IdentifierNode) binaryOperationNodeName.Right).Value;
                    }

                    return ctx.Set(name,
                        new ExpressionExecutor().Execute(right, currentContext, currentSymbol));
                }

                var op = binaryOperationNode.OperationType;

                if (op == BinaryOperation.Addition || op == BinaryOperation.Subtraction ||
                    op == BinaryOperation.Division || op == BinaryOperation.Multiplication ||
                    op == BinaryOperation.Equal || op == BinaryOperation.Greater ||
                    op == BinaryOperation.Less || op == BinaryOperation.GreaterEqual ||
                    op == BinaryOperation.LessEqual)
                {
                    var l = new ExpressionExecutor().Execute(left, currentContext, currentSymbol);
                    var r = new ExpressionExecutor().Execute(right, currentContext, currentSymbol);

                    var opString = "";

                    switch (op)
                    {
                        case BinaryOperation.Addition:
                            opString = "_plusoperator";
                            break;
                        case BinaryOperation.Subtraction:
                            opString = "_minusoperator";
                            break;
                        case BinaryOperation.Multiplication:
                            opString = "_multiplyoperator";
                            break;
                        case BinaryOperation.Division:
                            opString = "_divideoperator";
                            break;
                        case BinaryOperation.Equal:
                            opString = "_equaloperator";
                            break;
                        case BinaryOperation.Greater:
                            opString = "_greateroperator";
                            break;
                        case BinaryOperation.GreaterEqual:
                            opString = "_greaterequaloperator";
                            break;
                        case BinaryOperation.Less:
                            opString = "_lessoperator";
                            break;
                        case BinaryOperation.LessEqual:
                            opString = "_lessequaloperator";
                            break;
                    }

                    return new FunctionCallExecutor().Execute(l.Symbol.Get<PlatypusFunctionSymbol>(opString),
                        currentContext, new object[] {r}, l);
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}