using System.Collections.Generic;

namespace CPlatypus.Semantic
{
    public abstract class PlatypusSymbol
    {
        public string Name { get; protected set; }

        private Dictionary<string, PlatypusSymbol> Symbols { get; }

        public PlatypusSymbol Parent { get; }

        public PlatypusSymbol(PlatypusSymbol parent)
        {
            Symbols = new Dictionary<string, PlatypusSymbol>();
            Parent = parent;
        }

        public void Add(PlatypusSymbol symbol)
        {
            Symbols.Add(symbol.Name, symbol);
        }

        public T Get<T>(string name) where T : PlatypusSymbol
        {
            if (!Symbols.ContainsKey(name)) return null;
            
            var symbol = Symbols[name];
            if (symbol is T platypusSymbol)
            {
                return platypusSymbol;
            }

            //TODO : THROW ERROR
            return null;
        }
    }
}