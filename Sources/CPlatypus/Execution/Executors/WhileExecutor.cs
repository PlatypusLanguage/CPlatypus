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
    public class WhileExecutor : PlatypusNodeExecutor
    {
        public bool HasReturnedValue { get; private set; }

        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext, Symbol currentSymbol)
        {
            HasReturnedValue = false;
            
            if (node is WhileNode whileNode)
            {
                PlatypusInstance conditionResult;

                var expressionExecutor = new ExpressionExecutor();

                while ((conditionResult =
                           expressionExecutor.Execute(whileNode.Condition, currentContext, currentSymbol)).HasValue &&
                       conditionResult.GetValue() is bool && conditionResult.GetValue<bool>())
                {
                    var executor = new BodyExecutor();
                    var result = executor.Execute(whileNode.Body, currentContext, currentSymbol);
                    HasReturnedValue = executor.HasReturnedValue;
                    if (HasReturnedValue)
                    {
                        return result;
                    }
                }
            }

            return PlatypusNullInstance.Instance;
        }
    }
}