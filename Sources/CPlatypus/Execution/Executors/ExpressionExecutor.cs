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
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Execution;
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class ExpressionExecutor : PlatypusNodeExecutor
    {
        public override PlatypusInstance Execute(PlatypusNode node, Context currentContext,
            Symbol currentSymbol)
        {
            if (node is IdentifierNode identifierNode)
            {
                var value = currentContext.Get(identifierNode.Value);
                if (value is PlatypusInstance platypusInstance)
                {
                    return platypusInstance;
                }
            }

            if (node is IntegerNode integerNode)
            {
                return PlatypusInteger.Singleton.Create(currentContext, currentSymbol,
                    new Dictionary<string, object> {{"value", integerNode.Value}});
            }

            if (node is StringNode stringNode)
            {
                return PlatypusString.Singleton.Create(currentContext, currentSymbol,
                    new Dictionary<string, object> {{"value", stringNode.Value}});
            }

            if (node is BooleanNode booleanNode)
            {
                return PlatypusBoolean.Singleton.Create(currentContext, currentSymbol,
                    new Dictionary<string, object> {{"value", booleanNode.Value}});
            }

            if (node is FunctionCallNode functionCallNode)
            {
                return new FunctionCallExecutor().Execute(functionCallNode, currentContext, currentSymbol);
            }

            if (node is NewNode newNode)
            {
                return new NewExecutor().Execute(newNode, currentContext, currentSymbol);
            }

            if (node is BinaryOperationNode binaryOperationNode)
            {
                return new BinaryOperationExecutor().Execute(binaryOperationNode, currentContext, currentSymbol);
            }

            return PlatypusNullInstance.Instance;
        }

        public Context ResolveContext(PlatypusNode node, Context context)
        {
            if (node is IdentifierNode identifierNode)
            {
                if (context.ContainsLocal(identifierNode.Value))
                {
                    return context;
                }

                if (context.Parent is null)
                {
                    return null;
                }

                return ResolveContext(identifierNode, context.Parent);
            }

            if (node is AttributeAccessNode attributeAccessNode)
            {
                var ctx = ResolveContext(attributeAccessNode.Left, context);
                return ResolveContext(attributeAccessNode.Attribute, ctx);
            }

            return null;
        }

        public PlatypusInstance ResolveObject(PlatypusNode node, Context context, Symbol symbol)
        {
            if (node is IdentifierNode identifierNode)
            {
                if (context.Contains(identifierNode.Value))
                {
                    return (PlatypusInstance) context.Get(identifierNode.Value);
                }
            }

            if (node is FunctionCallNode functionCallNode)
            {
                return new FunctionCallExecutor().Execute(functionCallNode, context, symbol);
            }

            if (node is AttributeAccessNode attributeAccessNode)
            {
                var left = ResolveObject(attributeAccessNode.Left, context, symbol);
                return ResolveObject(attributeAccessNode.Attribute, left.Context, left.Symbol);

                //TODO CHECK THIS IN MULTIPLE CASES
                /*
                return ResolveContext(attributeAccessNode.Attribute,
                    ResolveObjectContext(attributeAccessNode.Left, context, symbol));
                    */
            }

            return null;
        }
    }
}