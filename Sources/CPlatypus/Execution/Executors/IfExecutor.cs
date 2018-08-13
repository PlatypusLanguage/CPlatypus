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
    public class IfExecutor : PlatypusNodeExecutor
    {
        public bool HasReturnedValue { get; private set; }

        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext, Symbol currentSymbol)
        {
            HasReturnedValue = false;
            if (node is IfNode ifNode)
            {
                var conditionResult = new ExpressionExecutor().Execute(ifNode.Condition, currentContext, currentSymbol);
                if (conditionResult.HasValue && conditionResult.GetValue() is bool)
                {
                    if (conditionResult.GetValue<bool>())
                    {
                        var executor = new BodyExecutor();
                        var result = executor.Execute(ifNode.Body, currentContext, currentSymbol);
                        HasReturnedValue = executor.HasReturnedValue;
                        return result;
                    }

                    if (ifNode.ElseIfNode != null)
                    {
                        var executor = new IfExecutor();
                        var result = executor.Execute(ifNode.ElseIfNode, currentContext, currentSymbol);
                        HasReturnedValue = executor.HasReturnedValue;
                        return result;
                    }
                    
                    if (ifNode.ElseBody != null)
                    {
                        var executor = new BodyExecutor();
                        var result = executor.Execute(ifNode.ElseBody, currentContext, currentSymbol);
                        HasReturnedValue = executor.HasReturnedValue;
                        return result;
                    }
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}