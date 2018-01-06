using CPlatypus.Parser.Nodes;

namespace CPlatypus.Semantic
{
    public class PlatypusModuleSymbol : PlatypusSymbol
    {        
        public ModuleNode ModuleNode { get; }
        
        public PlatypusModuleSymbol(ModuleNode moduleNode, PlatypusSymbol parent) : base(parent)
        {
            Name = moduleNode.Name.Value;
            ModuleNode = moduleNode;
        }

        public static PlatypusModuleSymbol CreateGlobalModule(string name = "Global Module")
        {
            return new PlatypusModuleSymbol(name);
        }
        
        private PlatypusModuleSymbol(string name) : base(null)
        {
            Name = name;
        }
    }
}