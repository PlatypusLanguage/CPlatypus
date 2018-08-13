/*
 * Copyright (c) 2018 Platypus Language http://platypus.vfrz.fr/
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

using System;
using System.Collections.Generic;

namespace CPlatypus.Framework.Execution
{
    public class Context
    {
        public Guid Identifier { get; }
        
        public string Name { get; }

        public Context Parent { get; }

        private readonly Dictionary<string, object> _fields;

        public Context TopContext
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

        private readonly object _defaultNullValue;

        public Context(string name, Context parent, object defaultNullValue)
        {
            Identifier = Guid.NewGuid();
            Name = name;
            Parent = parent;
            _defaultNullValue = defaultNullValue;
            _fields = new Dictionary<string, object>();
        }

        public object Add(string name)
        {
            _fields.Add(name, _defaultNullValue);
            return _defaultNullValue;
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

        public T Set<T>(string name, T instance)
        {
            return (T) (_fields[name] = instance);
        }

        public object Get(string name)
        {
            return GetLocal(name) ?? Parent?.GetLocal(name);
        }

        public T Get<T>(string name)
        {
            return GetLocal(name) is null ? Parent.GetLocal<T>(name) : default(T);
        }

        public object GetLocal(string name)
        {
            return _fields.ContainsKey(name) ? _fields[name] : null;
        }

        public T GetLocal<T>(string name)
        {
            return _fields.ContainsKey(name) ? (T) _fields[name] : default(T);
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