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
        public static BodyExecutor Instance { get; } = new BodyExecutor();

        private BodyExecutor()
        {
        }

        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext,
            Symbol currentSymbol)
        {
            if (node is CodeNode codeNode)
            {
                foreach (var n in codeNode.Children)
                {
                    if (n is VariableDeclarationNode)
                    {
                        VariableDeclarationExecutor.Instance.Execute(n, currentContext, currentSymbol);
                    }
                    else if (n is BinaryOperationNode)
                    {
                        BinaryOperationExecutor.Instance.Execute(n, currentContext, currentSymbol);
                    }
                    else if (n is FunctionCallNode)
                    {
                        FunctionCallExecutor.Instance.Execute(n, currentContext, currentSymbol);
                    }
                    else if (n is ReturnNode returnNode)
                    {
                        return ExpressionExecutor.Instance.Execute(returnNode.Expression, currentContext, currentSymbol);
                    }
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}