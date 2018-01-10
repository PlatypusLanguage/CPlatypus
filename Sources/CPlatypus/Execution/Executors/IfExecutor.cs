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
    public class IfExecutor : PlatypusNodeExecutor
    {
        public static IfExecutor Instance { get; } = new IfExecutor();

        private IfExecutor()
        {
        }

        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext, Symbol currentSymbol)
        {
            if (node is IfNode ifNode)
            {
                var conditionResult =
                    ExpressionExecutor.Instance.Execute(ifNode.Condition, currentContext, currentSymbol);
                if (conditionResult.HasValue && conditionResult.GetValue() is bool)
                {
                    if (conditionResult.GetValue<bool>())
                    {
                        var r = BodyExecutor.Instance.Execute(ifNode.Body, currentContext, currentSymbol);
                        return r != PlatypusNullInstance.Instance ? r : null;
                    }
                    else
                    {
                        //TODO Execute else if
                        if (ifNode.ElseBody != null)
                        {
                            var r = BodyExecutor.Instance.Execute(ifNode.ElseBody, currentContext, currentSymbol);
                            return r != PlatypusNullInstance.Instance ? r : null;
                        }
                    }
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}