﻿/*
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

using CPlatypus.Framework;

namespace CPlatypus.Lexer.Matcher
{
    public class EosMatcher : IMatcher
    {
        public PlatypusToken Match(SourceLocation location, Source source)
        {
            return source.PeekChar() != '\0' ? null : new PlatypusToken(PlatypusTokenType.Eos, source.PopChar().ToString(), location);
        }
    }
}