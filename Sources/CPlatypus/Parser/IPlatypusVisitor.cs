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

using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser
{
    public interface IPlatypusVisitor
    {
        void Visit(ArgumentListNode node);
        void Visit(ArrayAccessNode node);
        void Visit(AttributeAccessNode node);
        void Visit(BinaryOperationNode node);
        void Visit(BooleanNode node);
        void Visit(CharNode node);
        void Visit(ClassNode node);
        void Visit(CodeNode node);
        void Visit(ConstructorNode node);
        void Visit(IdentifierNode node);
        void Visit(IntegerNode node);
        void Visit(FloatNode node);
        void Visit(FunctionCallNode node);
        void Visit(FunctionNode node);
        void Visit(NewNode node);
        void Visit(ThisNode node);
        void Visit(UnaryOperationNode node);
        void Visit(VariableDeclarationNode node);
        void Visit(StringNode node);
    }
}