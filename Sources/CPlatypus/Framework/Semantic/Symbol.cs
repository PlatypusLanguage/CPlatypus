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
    public abstract class Symbol
    {
        public string Name { get; protected set; }

        private Dictionary<string, Symbol> Symbols { get; }

        public Symbol Parent { get; }

        public Symbol(Symbol parent)
        {
            Symbols = new Dictionary<string, Symbol>();
            Parent = parent;
        }

        public void Add(Symbol symbol)
        {
            Symbols.Add(symbol.Name, symbol);
        }


        public T GetLocal<T>(string name) where T : Symbol
        {
            if (!Symbols.ContainsKey(name)) return null;
            
            var symbol = Symbols[name];
            if (symbol is T platypusSymbol)
            {
                return platypusSymbol;
            }
            else
            {
                //TODO : THROW ERROR
            }

            //TODO : THROW ERROR
            return null;
        }
        
        public T Get<T>(string name) where T : Symbol
        {
            return GetLocal<T>(name) ?? Parent?.GetLocal<T>(name);
        }
    }
}