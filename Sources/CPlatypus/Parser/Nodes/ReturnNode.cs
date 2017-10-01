using System;
using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class ReturnNode : PlatypusNode
    {
        public PlatypusNode Expression => Children[0];

        public ReturnNode(int id, PlatypusNode expression, SourceLocation sourceLocation) : base(id, sourceLocation)
        {
            Children.Add(expression);
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