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

using System;
using CPlatypus.Framework.Parser;
using CPlatypus.Lexer;

namespace CPlatypus.Parser
{
    public class PlatypusParser : Framework.Parser.Parser
    {
        
        public PlatypusParser(Framework.Lexer.Lexer lexer) : base(lexer)
        {
        }

        public override Node Parse(Framework.Lexer.Lexer lexer)
        {
            throw new System.NotImplementedException();
        }

        public bool Match(PlatypusTokenType tokenType)
        {
            return base.Match(Convert.ToInt32(tokenType));
        }

        public bool Match(PlatypusTokenType tokenType, string value)
        {
            return base.Match(Convert.ToInt32(tokenType), value);
        }

        public PlatypusToken Consume(PlatypusTokenType tokenType)
        {
            return (PlatypusToken) base.Consume(Convert.ToInt32(tokenType));
        }

        public PlatypusToken Consume(PlatypusTokenType tokenType, string value)
        {
            return (PlatypusToken)base.Consume(Convert.ToInt32(tokenType), value);
        }
    }
}