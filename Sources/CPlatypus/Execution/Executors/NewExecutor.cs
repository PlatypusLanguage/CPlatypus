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

using System.Collections.Generic;
using CPlatypus.Execution.Object;
using CPlatypus.Framework.Execution;
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class NewExecutor : PlatypusNodeExecutor
    {
        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext, Symbol currentSymbol)
        {
            var newNode = (NewNode) node;

            var functionCallNode = (FunctionCallNode) newNode.Target;

            var classSymbol = currentSymbol.Get<PlatypusClassSymbol>(functionCallNode.FunctionName.Value);

            var newInstance = new PlatypusInstance(classSymbol, currentContext);
            
            var constructorSymbol = classSymbol.GetLocal<PlatypusFunctionSymbol>("_constructor");

            var args = new List<object>();

            foreach (var argument in functionCallNode.ArgumentList.Arguments)
            {
                args.Add(new ExpressionExecutor().Execute(argument, newInstance.Context, currentSymbol));
            }
            
            new FunctionCallExecutor().Execute(constructorSymbol, newInstance.Context, args.ToArray(), newInstance);

            return newInstance;
        }
    }
}