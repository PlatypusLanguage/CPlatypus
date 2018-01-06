using CPlatypus.Execution.Object;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Semantic
{
    public class PlatypusClassSymbol : PlatypusSymbol
    {
        public ClassNode ClassNode { get; }
        
        public PlatypusClass ClassTarget { get; }

        public bool ExternClass => ClassNode is null;
        
        public PlatypusClassSymbol(ClassNode classNode,  PlatypusSymbol parent) : base(parent)
        {
            Name = classNode.Name.Value;
            ClassNode = classNode;
        }

        public PlatypusClassSymbol(PlatypusClass classTarget, PlatypusSymbol parent) : base(parent)
        {
            Name = classTarget.Name;
            ClassTarget = classTarget;
        }
    }
}