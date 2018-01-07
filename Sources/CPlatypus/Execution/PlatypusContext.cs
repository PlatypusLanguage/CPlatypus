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
using CPlatypus.Execution.StandardLibrary.Types;

namespace CPlatypus.Execution
{
    public class PlatypusContext
    {
        public string Name { get; }

        public PlatypusContext Parent { get; }

        private readonly Dictionary<string, object> _fields;

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
            _fields = new Dictionary<string, object>();
        }

        public object Add(string name)
        {
            _fields.Add(name, PlatypusNullInstance.Instance);
            return PlatypusNullInstance.Instance;
        }

        public object Add(string name, object instance)
        {
            Add(name);
            Set(name, instance);
            return instance;
        }

        public object Set(string name, object obj)
        {
            return _fields[name] = obj;
        }
        
        public T Set<T>(string name, T instance) where T : class
        {
            return (T)(_fields[name] = instance);
        }

        public object Get(string name)
        {
            return GetLocal(name) ?? Parent?.GetLocal(name);
        }

        public T Get<T>(string name) where T : class
        {
            return GetLocal<T>(name) ?? Parent?.GetLocal<T>(name);
        }

        public object GetLocal(string name)
        {
            return _fields.ContainsKey(name) ? _fields[name] : null;
        }
        
        public T GetLocal<T>(string name) where T : class
        {
            return _fields.ContainsKey(name) ? (T)_fields[name] : null;
        }
        
        public bool ContainsLocal(string name)
        {
            return _fields.ContainsKey(name);
        }

        public bool Contains(string name)
        {
            if (ContainsLocal(name))
            {
                return true;
            }

            return Parent != null && Parent.Contains(name);
        }
    }
}