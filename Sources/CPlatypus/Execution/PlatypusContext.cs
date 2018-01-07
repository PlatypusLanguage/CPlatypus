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
    public class PlatypusContext
    {
        public string Name { get; }

        public PlatypusContext Parent { get; }

        private readonly Dictionary<string, PlatypusInstance> _variables;

        public PlatypusContext GlobalContext
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

        public PlatypusContext(string name, PlatypusContext parent)
        {
            Name = name;
            Parent = parent;
            _variables = new Dictionary<string, PlatypusInstance>();
        }

        public void Add(string name)
        {
            _variables.Add(name, null);
        }

        public void Add(string name, PlatypusInstance instance)
        {
            Add(name);
            Set(name, instance);
        }

        public PlatypusInstance Set(string name, PlatypusInstance instance)
        {
            return _variables[name] = instance;
        }

        public PlatypusInstance Get(string name)
        {
            return GetLocal(name) ?? Parent?.GetLocal(name);
        }

        public PlatypusInstance GetLocal(string name)
        {
            return _variables.ContainsKey(name) ? _variables[name] : null;
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