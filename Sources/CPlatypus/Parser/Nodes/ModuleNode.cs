using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class ModuleNode : PlatypusNode
    {
        public IdentifierNode Name => Children[0] as IdentifierNode;

        public CodeNode Body => Children[1] as CodeNode;

        public ModuleNode(int id, IdentifierNode name, CodeNode body, SourceLocation sourceLocation) : base(id, sourceLocation)
        {
            Children.Add(name);
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
                child.Accept(visitor, parent);
            }
        }
    }
}