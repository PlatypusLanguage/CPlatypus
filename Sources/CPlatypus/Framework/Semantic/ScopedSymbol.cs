/*
 * Copyright (c) 2017 Platypus Language http://platypus.vfrz.fr/
 *  This file is part of CPlatypus.
 *
 *     CPlatypus is free software: you can redistribute it and/or modify
 *     it under the terms of the GNU General Public License as published by
 *     the Free Software Foundation, either version 3 of the License, or
 *     (at your option) any later version.
 *
 *     CPlatypus is distributed in the hope that it will be useful,
 *     but WITHOUT ANY WARRANTY; without even the implied warranty of
 *     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *     GNU General Public License for more details.
 *
 *     You should have received a copy of the GNU General Public License
 *     along with CPlatypus.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;

namespace CPlatypus.Framework.Semantic
{
    public abstract class ScopedSymbol : Symbol, IScope
    {
        private readonly Dictionary<string, Symbol> _symbols;

        private readonly IScope _parentScope;

        protected ScopedSymbol(string name = "scoped_symbol", IScope parentScope = null) : base(name, parentScope)
        {
            _parentScope = parentScope;
            _symbols = new Dictionary<string, Symbol>();
        }

        public void Insert(Symbol symbol)
        {
            _symbols.Add(symbol.Name, symbol);
        }

        public Symbol Lookup(string name)
        {
            var result = _symbols.ContainsKey(name) ? _symbols[name] : null;

            if (result == null && _parentScope != null)
            {
                return _parentScope.Lookup(name);
            }

            return result;
        }

        public T Lookup<T>(string name) where T : Symbol
        {
            return Lookup(name) as T;
        }

        public T LookupType<T>(string name) where T : Symbol
        {
            var result = _symbols.ContainsKey(name) ? _symbols[name] : null;

            if (!(result is T) && _parentScope != null)
            {
                return _parentScope.LookupType<T>(name);
            }

            return result as T;
        }

        public Symbol LookupLocal(string name)
        {
            return _symbols.ContainsKey(name) ? _symbols[name] : null;
        }

        public T LookupLocal<T>(string name) where T : Symbol
        {
            return LookupLocal(name) as T;
        }

        public string GetName()
        {
            return Name;
        }

        public IScope GetParentScope()
        {
            return _parentScope;
        }
    }
}