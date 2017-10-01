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
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Executors
{
    public class ExpressionExecutor : NodeExecutor
    {
        public static ExpressionExecutor Instance { get; } = new ExpressionExecutor();

        private ExpressionExecutor()
        {
        }

        public override PlatypusObject Execute(PlatypusNode node, ExecutionContext context)
        {
            if (node is IdentifierNode identifierNode)
            {
                return context.Get(identifierNode.Value);
            }
            if (node is StringNode stringNode)
            {
                return new PlatypusString(stringNode.Value, context);
            }
            if (node is FunctionCallNode functionCallNode)
            {
                var symbol = ResolveSymbol(functionCallNode.Target);
                var functionContext = ResolveContext(functionCallNode.Target, context);
                if (symbol is PlatypusFunctionSymbol functionSymbol)
                {
                    Console.WriteLine(functionContext);
                }
                else
                {
                    // TODO Throw error
                }
            }
            return new PlatypusNull(null);
        }

        private Context ResolveContext(PlatypusNode node, ExecutionContext context)
        {
            if (node is IdentifierNode)
            {
                return context.CurrentContext;
            }
            return ResolveContextInternal(node, context);
        }

        private Context ResolveContextInternal(PlatypusNode node, ExecutionContext context)
        {
            if (node is IdentifierNode identifierNode)
            {
                return context.Get(identifierNode.Value).Context;
            }
            if (node is AttributeAccessNode attributeAccessNode)
            {
                return ResolveContextInternal(attributeAccessNode.Left, context);
            }
            return null; //Should never happen
        }

        private Symbol ResolveSymbol(PlatypusNode node)
        {
            if (node is IdentifierNode identifierNode)
            {
                return ResolveScope(identifierNode).Lookup(identifierNode.Value);
            }
            if (node is AttributeAccessNode attributeAccessNode)
            {
                return ResolveScope(attributeAccessNode).Lookup(attributeAccessNode.Attribute.Value);
            }
            return null; //Should never happen
        }

        private IScope ResolveScope(PlatypusNode node)
        {
            if (node is IdentifierNode identifierNode)
            {
                return identifierNode.Scope;
            }
            if (node is AttributeAccessNode attributeAccessNode)
            {
                return ResolveScope(attributeAccessNode.Left);
            }
            return null; //Should never happen
        }
    }
}