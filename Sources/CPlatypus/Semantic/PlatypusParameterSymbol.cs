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

using CPlatypus.Framework.Semantic;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Semantic
{
    public class PlatypusParameterSymbol : Symbol
    {
        public ParameterListNode ParameterListNode { get; }

        public IdentifierNode IdentifierNode { get; }

        public PlatypusParameterSymbol(ParameterListNode paramterListNode, IdentifierNode identifierNode, IScope scope)
            : base(identifierNode.Value, scope)
        {
            ParameterListNode = paramterListNode;
            IdentifierNode = identifierNode;
        }
    }
}