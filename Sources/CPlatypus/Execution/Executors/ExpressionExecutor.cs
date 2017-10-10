﻿/*
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
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Execution.Executors
{
    public class ExpressionExecutor : NodeExecutor
    {
        public static ExpressionExecutor Instance { get; } = new ExpressionExecutor();

        private ExpressionExecutor()
        {
        }

        public override PlatypusObject Execute(PlatypusNode node, Context context, Context globalContext)
        {
            if (node is IdentifierNode identifierNode)
            {
                return context.Get(identifierNode.Value);
            }
            if (node is StringNode stringNode)
            {
                return new PlatypusString(stringNode.Value, globalContext);
            }
            if (node is BooleanNode booleanNode)
            {
                return new PlatypusBoolean(booleanNode.Value, globalContext);
            }
            if (node is IntegerNode integerNode)
            {
                return new PlatypusInteger(integerNode.Value, globalContext);
            }
            if (node is FloatNode floatNode)
            {
                return new PlatypusFloat(floatNode.Value, globalContext);
            }
            if (node is CharNode charNode)
            {
                return new PlatypusChar(charNode.Value, globalContext);
            }
            if (node is FunctionCallNode)
            {
                return FunctionExecutor.Instance.Execute(node, context, globalContext);
            }
            return PlatypusNull.Instance;
        }

        public Context ResolveContext(PlatypusNode node, Context context)
        {
            if (node is IdentifierNode)
            {
                return context;
            }
            return ResolveContextInternal(node, context);
        }

        private Context ResolveContextInternal(PlatypusNode node, Context context)
        {
            if (node is IdentifierNode identifierNode)
            {
                return context.Get(identifierNode.Value).ObjectContext;
            }
            if (node is AttributeAccessNode attributeAccessNode)
            {
                return ResolveContextInternal(attributeAccessNode.Left, context);
            }
            return null; //Should never happen
        }

        public Symbol ResolveSymbol(PlatypusNode node)
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