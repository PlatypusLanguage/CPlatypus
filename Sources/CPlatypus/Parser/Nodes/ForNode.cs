using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class ForNode : PlatypusNode
    {
        public IdentifierNode Identifier => Children[0] as IdentifierNode;

        public PlatypusNode Target => Children[1];

        public CodeNode Body => Children[2] as CodeNode;

        public ForNode(int id, IdentifierNode identifier, PlatypusNode target, CodeNode body,
            SourceLocation sourceLocation) : base(id, sourceLocation)
        {
            Children.Add(identifier);
            Children.Add(target);
            Children.Add(body);
        }

        public override void Accept(IPlatypusVisitor visitor, PlatypusNode parent)
        {
            visitor.Visit(this, parent);
        }

        public override void AcceptChildren(IPlatypusVisitor visitor, PlatypusNode parent)
        {
            foreach (var child in Children)
            {
                child?.Accept(visitor, parent);
            }
        }
    }
}