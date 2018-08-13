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

using CPlatypus.Core;
using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class BinaryOperationNode : PlatypusNode
    {
        public readonly BinaryOperator OperatorType;

        public PlatypusNode Left => Children[0];

        public PlatypusNode Right => Children[1];

        public BinaryOperationNode(int id, BinaryOperator operatorType, PlatypusNode left, PlatypusNode right,
            SourceLocation sourceLocation) : base(id, sourceLocation)
        {
            OperatorType = operatorType;
            Children.Add(left);
            Children.Add(right);
        }

        public override void Accept(IPlatypusVisitor visitor, PlatypusNode parent)
        {
            visitor.Visit(this, parent);
        }

        public override void AcceptChildren(IPlatypusVisitor visitor, PlatypusNode parent)
        {
            foreach (var child in Children)
            {
                child.Accept(visitor, parent);
            }
        }
    }
}