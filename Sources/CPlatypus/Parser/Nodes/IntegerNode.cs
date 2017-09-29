using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class IntegerNode : PlatypusNode
    {
        public readonly int Value;

        public IntegerNode(int value, SourceLocation sourceLocation) : base(sourceLocation)
        {
            Value = value;
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