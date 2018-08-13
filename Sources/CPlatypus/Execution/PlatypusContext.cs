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

using CPlatypus.Core;
using CPlatypus.Core.Exceptions;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Execution;

namespace CPlatypus.Execution
{
    public class PlatypusContext : Context
    {
        public PlatypusContextType Type { get; }
        
        public PlatypusContext(PlatypusContextType type, Context parent) : base(type.ToContextName(), parent, PlatypusNullInstance.Instance)
        {
            Type = type;
        }

        public PlatypusContext GetFirstParentContextOfType(PlatypusContextType contextType)
        {
            if (Parent is PlatypusContext parentContext)
            {
                return parentContext.Type == contextType ? parentContext : parentContext.GetFirstParentContextOfType(contextType);
            }

            throw new ParentContextNotFoundException(contextType);
        }
    }
}