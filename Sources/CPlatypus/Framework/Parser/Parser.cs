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

using CPlatypus.Framework.Lexer;
using System;

namespace CPlatypus.Framework.Parser
{
    public abstract class Parser
    {
        protected Lexer.Lexer Lexer;

        public Parser(Lexer.Lexer lexer)
        {
            this.Lexer = lexer;
        }

        public abstract Node Parse(Lexer.Lexer lexer);

        public bool Match(int tokenType)
        {
            var token = Lexer.NextToken();

            return token != null && Convert.ToInt32(token.TokenType) == tokenType;
        }

        public bool Match(int tokenType, string value)
        {
            var token = Lexer.NextToken();

            return token != null && Convert.ToInt32(token.TokenType) == tokenType && token.Value.Equals(value);
        }

        public Token Consume(int tokenType)
        {
            if (Match(tokenType))
            {
                return Lexer.CurrentToken;
            }

            // TODO INFORM THE USER
            return null;
        }

        public Token Consume(int tokenType, string value)
        {
            if (Match(tokenType, value))
            {
                return Lexer.CurrentToken;
            }

            //  TODO INFORM THE USER
            return null;
        }
    }
}