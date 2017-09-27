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

using System.Collections.Generic;
using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class FunctionNode : PlatypusNode
    {
        public IdentifierNode Name => Children[0] as IdentifierNode;

        public PlatypusNode Body => Children[1];

        public List<IdentifierNode> Parameters =>
            Children.GetRange(2, Children.Count - 3).ConvertAll(x => (IdentifierNode) x);


        public FunctionNode(IdentifierNode name, List<IdentifierNode> parameters, PlatypusNode body,
            SourceLocation sourceLocation) : base(sourceLocation)
        {
            Children.Add(name);
            Children.Add(body);
            Children.AddRange(parameters);
        }

        public override void Accept(IPlatypusVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void AcceptChildren(IPlatypusVisitor visitor)
        {
            foreach (var child in Children)
            {
                child.Accept(visitor);
            }
        }
    }
}