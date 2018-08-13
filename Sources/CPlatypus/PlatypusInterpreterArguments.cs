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

using PowerArgs;

namespace CPlatypus
{
    public class PlatypusInterpreterArguments
    {
        [ArgRequired, ArgShortcut("-f"), ArgDescription("Code file to be executed"), ArgPosition(1)]
        public string File { get; set; } = "";
        
        [ArgShortcut("-g"), ArgDescription("Dot graph file of AST"), ArgPosition(2)]
        public string DotGraphFile { get; set; } = "";
        
        [ArgShortcut("-iut"), ArgDescription("Ignore unknown tokens instead of throwing errors"), ArgPosition(3)]
        public bool IgnoreUnknownTokens { get; set; }
    }
}