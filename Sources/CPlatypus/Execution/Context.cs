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
using CPlatypus.Execution.Object;

namespace CPlatypus.Execution
{
    public class Context
    {
        public string Name { get; }

        public Context Parent { get; }

        private readonly Dictionary<string, PlatypusObject> _variables;

        public Context(string name, Context parent)
        {
            Name = name;
            Parent = parent;
            _variables = new Dictionary<string, PlatypusObject>();
        }

        public Context GlobalContext
        {
            get
            {
                var ctx = this;
                while (ctx.Parent != null)
                {
                    ctx = ctx.Parent;
                }

                return ctx;
            }
        }

        public void Add(string name, PlatypusObject value = null)
        {
            _variables.Add(name, value);
        }

        public void Set(string name, PlatypusObject value)
        {
            _variables[name] = value;
        }

        public PlatypusObject Get(string name)
        {
            if (_variables.ContainsKey(name))
            {
                return _variables[name];
            }

            if (Parent != null)
            {
                return Parent.Get(name);
            }

            return PlatypusNull.Instance;
        }

        public bool ContainsLocal(string name)
        {
            return _variables.ContainsKey(name);
        }

        public bool Contains(string name)
        {
            if (_variables.ContainsKey(name))
            {
                return true;
            }

            if (Parent != null)
            {
                return Parent.Contains(name);
            }

            return false;
        }
    }
}