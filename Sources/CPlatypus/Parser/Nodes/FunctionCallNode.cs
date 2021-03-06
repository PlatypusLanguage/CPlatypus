﻿/*
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

using System;
using CPlatypus.Core;
using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class FunctionCallNode : PlatypusNode
    {
        public IdentifierNode FunctionName => Children[0] as IdentifierNode;

        public PlatypusNode TargetNode => Children[1];

        public bool HasTarget => TargetNode != null;

        public ArgumentListNode ArgumentList => Children[2] as ArgumentListNode;

        public FunctionCallNode(int id, PlatypusNode target, ArgumentListNode arguments, SourceLocation sourceLocation) : base(id, sourceLocation)
        {
            if (target is IdentifierNode)
            {
                Children.Add(target);
                Children.Add(null);
            }
            else if (target is AttributeAccessNode attributeAccessNode)
            {
                Children.Add(attributeAccessNode.Attribute);
                Children.Add(attributeAccessNode.Left);
            }
            else
            {
                //TODO THROW EXCEPTION
                Console.WriteLine("PROBLEM !!!");
            }
            
            Children.Add(arguments);
        }

        public override void Accept(IPlatypusVisitor visitor, PlatypusNode parent)
        {
            visitor.Visit(this, parent);
        }

        public override void AcceptChildren(IPlatypusVisitor visitor, PlatypusNode parent)
        {
            foreach (var child in Children)
            {
                child?.Accept(visitor, parent);
            }
        }
    }
}