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

using System.IO;
using System.Text;
using CPlatypus.Core;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Graphviz
{
    public class DotCompiler : IPlatypusVisitor
    {
        private StringBuilder _builder;

        private readonly PlatypusNode _tree;

        public DotCompiler(PlatypusNode tree)
        {
            _tree = tree;
        }

        public void Compile(string fileName)
        {
            _builder = new StringBuilder();
            // header
            _builder.Append("digraph g{\nnode[shape=rect,height=0];\n");

            _tree.Accept(this, 0);

            // footer
            _builder.Append("}");

            File.WriteAllText(fileName, _builder.ToString());
        }

        public void Visit(ArgumentListNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Argument List");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(ArrayAccessNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Array Access");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(AttributeAccessNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Attribute Access");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(BinaryOperationNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Binary Operation : " + node.OperationType, "white", "red3");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(BooleanNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Boolean : " + node.Value, "white", "orange");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(CharNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Char : " + node.Value, "white", "orange");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(ClassNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Class", "white", "cyan3");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(CodeNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Code (Body)", "white", "cornflowerblue");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(ConstructorNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Constructor", "white", "cyan3");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(IdentifierNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Identifier : " + node.Value, "white", "steelblue");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(IntegerNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Integer : " + node.Value, "white", "orange");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(FloatNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Float : " + node.Value, "white", "orange");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(FunctionCallNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Function Call", "white", "orange");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(FunctionNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Function", "white", "mediumseagreen");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(NewNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "New");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(ParameterListNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Parameters");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(ThisNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "This");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(UnaryOperationNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Unary Operation : " + node.OperationType, "white", "red3");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(VariableDeclarationNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "Variable Declaration", "white", "limegreen");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        public void Visit(StringNode node, int parentId)
        {
            _builder.AppendNode("node" + node.Id, "String : " + node.Value, "white", "orange");
            CreateLink(parentId, node.Id);
            node.AcceptChildren(this, node.Id);
        }

        private void CreateLink(int first, int second, string label = "")
        {
            if (first != second)
            {
                _builder.AppendNodeLink("node" + first, "node" + second, label);
            }
        }
    }
}