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

using System.Net.Mail;
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
        public static BinaryOperationExecutor Instance { get; } = new BinaryOperationExecutor();

        private BinaryOperationExecutor()
        {
        }

        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext,
            Symbol currentSymbol)
        {
            if (node is BinaryOperationNode binaryOperationNode)
            {
                var left = binaryOperationNode.Left;
                var right = binaryOperationNode.Right;

                if (binaryOperationNode.OperationType == BinaryOperation.Assignment)
                {
                    var ctx = ExpressionExecutor.Instance.ResolveContext(left, currentContext);
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
                        ExpressionExecutor.Instance.Execute(right, currentContext, currentSymbol));
                }

                var op = binaryOperationNode.OperationType;

                if (op == BinaryOperation.Addition || op == BinaryOperation.Subtraction ||
                    op == BinaryOperation.Division || op == BinaryOperation.Multiplication)
                {
                    var l = ExpressionExecutor.Instance.Execute(left, currentContext, currentSymbol);
                    var r = ExpressionExecutor.Instance.Execute(right, currentContext, currentSymbol);

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
                    }

                    return FunctionCallExecutor.Instance.Execute(l.Symbol.Get<PlatypusFunctionSymbol>(opString),
                        currentContext, new object[] {l, r});
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}