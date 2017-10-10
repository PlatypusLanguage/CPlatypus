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

namespace CPlatypus.Execution
{
    public class Context
    {
        public string Name { get; }

        public Context Parent { get; }

        private readonly Dictionary<string, PlatypusVariable> _variables;

        public Context(string name, Context parent)
        {
            Name = name;
            Parent = parent;
            _variables = new Dictionary<string, PlatypusVariable>();
        }

        public void Add(PlatypusVariable variable)
        {
            _variables[variable.Name] = variable;
        }

        public PlatypusVariable Get(string name)
        {
            if (_variables.ContainsKey(name))
            {
                return _variables[name];
            }

            if (Parent != null)
            {
                return Parent.Get(name);
            }

            return new PlatypusVariable("__null", null);
        }

    }
}