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

            _tree.Accept(this, _tree);

            // footer
            _builder.Append("}");

            File.WriteAllText(fileName, _builder.ToString());
        }

        public void Visit(ArgumentListNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Argument List");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(ArrayAccessNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Array Access");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(AttributeAccessNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Attribute Access");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(BinaryOperationNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Binary Operation : " + node.OperationType, "white", "red3");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(BooleanNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Boolean : " + node.Value, "white", "orange");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(CharNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Char : " + node.Value, "white", "orange");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(ClassNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Class", "white", "cyan3");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(CodeNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Code (Body)", "white", "cornflowerblue");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(ConstructorNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Constructor", "white", "cyan3");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(IdentifierNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Identifier : " + node.Value, "white", "steelblue");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(IfNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "If");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(IntegerNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Integer : " + node.Value, "white", "orange");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(FloatNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Float : " + node.Value, "white", "orange");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(ForNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "For");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(FunctionCallNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Function Call", "white", "orange");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(FunctionNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Function", "white", "mediumseagreen");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(NewNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "New");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(ParameterListNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Parameters");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(ThisNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "This");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(UnaryOperationNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Unary Operation : " + node.OperationType, "white", "red3");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(VariableDeclarationNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "Variable Declaration", "white", "limegreen");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(StringNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "String : " + node.Value, "white", "orange");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
        }

        public void Visit(WhileNode node, PlatypusNode parent)
        {
            _builder.AppendNode("node" + node.Id, "While");
            CreateLink(parent.Id, node.Id);
            node.AcceptChildren(this, node);
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