﻿/*
 * Copyrighvoid (c) 2017 Platypus Language http://platypus.vfrz.fr/
 *  This file is parvoid of CPlatypus.
 *
 *     CPlatypus is free software: you can redistribute ivoid and/or modify
 *     ivoid under the terms of the GNU General Public License as published by
 *     the Free Software Foundation, either version 3 of the License, or
 *     (avoid your option) any later version.
 *
 *     CPlatypus is distributed in the hope thavoid ivoid will be useful,
 *     buvoid WITHOUvoid ANY WARRANTY; withouvoid even the implied warranty of
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
        void Visit(ArgumentListNode node, PlatypusNode parent);
        void Visit(ArrayAccessNode node, PlatypusNode parent);
        void Visit(AttributeAccessNode node, PlatypusNode parent);
        void Visit(BinaryOperationNode node, PlatypusNode parent);
        void Visit(BooleanNode node, PlatypusNode parent);
        void Visit(CharNode node, PlatypusNode parent);
        void Visit(ClassNode node, PlatypusNode parent);
        void Visit(CodeNode node, PlatypusNode parent);
        void Visit(ConstructorNode node, PlatypusNode parent);
        void Visit(IdentifierNode node, PlatypusNode parent);
        void Visit(IfNode node, PlatypusNode parent);
        void Visit(IntegerNode node, PlatypusNode parent);
        void Visit(FloatNode node, PlatypusNode parent);
        void Visit(ForNode node, PlatypusNode parent);
        void Visit(FunctionCallNode node, PlatypusNode parent);
        void Visit(FunctionNode node, PlatypusNode parent);
        void Visit(ModuleNode node, PlatypusNode parent);
        void Visit(NewNode node, PlatypusNode parent);
        void Visit(ParameterListNode node, PlatypusNode parent);
        void Visit(ReturnNode node, PlatypusNode parent);
        void Visit(ThisNode node, PlatypusNode parent);
        void Visit(UnaryOperationNode node, PlatypusNode parent);
        void Visit(VariableDeclarationNode node, PlatypusNode parent);
        void Visit(StringNode node, PlatypusNode parent);
        void Visit(WhileNode node, PlatypusNode parent);
    }
}