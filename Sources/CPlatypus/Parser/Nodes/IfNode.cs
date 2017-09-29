using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class IfNode : PlatypusNode
    {
        public PlatypusNode Condition => Children[0];

        public CodeNode Body => Children[1] as CodeNode;

        public IfNode ElseIfNode => Children[2] as IfNode;

        public CodeNode ElseBody => Children[3] as CodeNode;

        public IfNode(int id, PlatypusNode condition, CodeNode body, IfNode elseIfNode, CodeNode elseBody, SourceLocation sourceLocation) : base(id, sourceLocation)
        {
            Children.Add(condition);
            Children.Add(body);
            Children.Add(elseIfNode);
            Children.Add(elseBody);
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