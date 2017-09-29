using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class BooleanNode : PlatypusNode
    {
        public readonly bool Value;

        public BooleanNode(bool value, SourceLocation sourceLocation) : base(sourceLocation)
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