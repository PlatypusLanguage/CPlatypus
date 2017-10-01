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

namespace CPlatypus.Execution
{
    public class ExecutionContext
    {
        public Context GlobalContext { get; }

        public Context CurrentContext { get; private set; }

        public ExecutionContext()
        {
            GlobalContext = new Context("Global Context", null);
            CurrentContext = GlobalContext;
        }

        public void Add(string name, PlatypusVariable variable)
        {
            CurrentContext.Add(name, variable);
        }

        public PlatypusVariable Get(string name)
        {
            return CurrentContext.Get(name);
        }

        public void EnterScope(string name = "Context")
        {
            CurrentContext = new Context(name, CurrentContext);
        }

        public void ExitScope()
        {
            if (CurrentContext.Parent != null)
            {
                CurrentContext = CurrentContext.Parent;
            }
            else
            {
                // TODO THROW ERROR (should never happen btw)
            }
        }
    }
}