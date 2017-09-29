using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class AttributeAccessNode : PlatypusNode
    {
        public PlatypusNode Left => Children[0];

        public IdentifierNode Attribute => Children[1] as IdentifierNode;

        public AttributeAccessNode(PlatypusNode left, IdentifierNode attribute, SourceLocation sourceLocation) : base(sourceLocation)
        {
            Children.Add(left);
            Children.Add(attribute);
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