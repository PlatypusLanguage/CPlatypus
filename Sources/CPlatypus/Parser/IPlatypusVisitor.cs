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
        void Visit(ArgumentListNode node, int parentId);
        void Visit(ArrayAccessNode node, int parentId);
        void Visit(AttributeAccessNode node, int parentId);
        void Visit(BinaryOperationNode node, int parentId);
        void Visit(BooleanNode node, int parentId);
        void Visit(CharNode node, int parentId);
        void Visit(ClassNode node, int parentId);
        void Visit(CodeNode node, int parentId);
        void Visit(ConstructorNode node, int parentId);
        void Visit(IdentifierNode node, int parentId);
        void Visit(IntegerNode node, int parentId);
        void Visit(FloatNode node, int parentId);
        void Visit(FunctionCallNode node, int parentId);
        void Visit(FunctionNode node, int parentId);
        void Visit(NewNode node, int parentId);
        void Visit(ParameterListNode node, int parentId);
        void Visit(ThisNode node, int parentId);
        void Visit(UnaryOperationNode node, int parentId);
        void Visit(VariableDeclarationNode node, int parentId);
        void Visit(StringNode node, int parentId);
    }
}