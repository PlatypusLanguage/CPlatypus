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

using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class BooleanNode : PlatypusNode
    {
        public readonly bool Value;

        public BooleanNode(int id, bool value, SourceLocation sourceLocation) : base(id, sourceLocation)
        {
            Value = value;
        }

        public override void Accept(IPlatypusVisitor visitor, int parentId)
        {
            visitor.Visit(this, parentId); 
        }

        public override void AcceptChildren(IPlatypusVisitor visitor, int parentId)
        {
            foreach (var child in Children)
            {
                child.Accept(visitor, parentId);
            }
        }
    }
}