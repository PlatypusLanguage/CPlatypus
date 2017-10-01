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

namespace CPlatypus.Execution.Executors
{
    public class BinaryOperationExecutor : NodeExecutor
    {
        public static BinaryOperationExecutor Instance { get; } = new BinaryOperationExecutor();

        private BinaryOperationExecutor()
        {
        }

        public override PlatypusObject Execute(PlatypusNode node, ExecutionContext context)
        {
            if (node is BinaryOperationNode binaryOperationNode)
            {
                switch (binaryOperationNode.OperationType)
                {
                    case BinaryOperation.Assignment:
                        return ExecuteAssignment(binaryOperationNode, context);
                    case BinaryOperation.Addition:
                        break;
                    case BinaryOperation.Subtraction:
                        break;
                    case BinaryOperation.Multiplication:
                        break;
                    case BinaryOperation.Division:
                        break;
                    case BinaryOperation.Or:
                        break;
                    case BinaryOperation.And:
                        break;
                    case BinaryOperation.Equal:
                        break;
                    case BinaryOperation.NotEqual:
                        break;
                    case BinaryOperation.Greater:
                        break;
                    case BinaryOperation.GreaterEqual:
                        break;
                    case BinaryOperation.Is:
                        break;
                    case BinaryOperation.Less:
                        break;
                    case BinaryOperation.LessEqual:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return new PlatypusNull(null); // Should never happen
        }

        private PlatypusObject ExecuteAssignment(BinaryOperationNode node, ExecutionContext context)
        {
            var left = ExpressionExecutor.Instance.Execute(node.Left, context);
            if (left is PlatypusVariable variable)
            {
                variable.Value = ExpressionExecutor.Instance.Execute(node.Right, context);
            }
            return new PlatypusNull(null);
        }
    }
}