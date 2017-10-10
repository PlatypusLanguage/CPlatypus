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

using CPlatypus.Execution.Object;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Execution.Executors
{
    public class VariableDeclarationExecutor : NodeExecutor
    {
        public static VariableDeclarationExecutor Instance { get; } = new VariableDeclarationExecutor();

        private VariableDeclarationExecutor()
        {
        }

        public override PlatypusObject Execute(PlatypusNode node, Context context, Context globalContext)
        {
            if (node is VariableDeclarationNode variableDeclarationNode)
            {
                context.Add(new PlatypusVariable(variableDeclarationNode.VariableNameNode.Value, context));
            }
            return PlatypusNull.Instance; // Should never happen
        }
    }
}