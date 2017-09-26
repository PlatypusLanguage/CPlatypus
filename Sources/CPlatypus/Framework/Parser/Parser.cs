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
    public abstract class Parser<TToken, TNode> where TToken : Token where TNode : Node<TNode>
    {
        protected Lexer<TToken> Lexer;

        public Parser(Lexer<TToken> lexer)
        {
            Lexer = lexer;
        }

        public abstract TNode Parse();

        public bool Match(int tokenType)
        {
            return Match(0, tokenType);
        }

        public bool Match(int tokenType, string value)
        {
            return Match(0, tokenType, value);
        }

        public bool Match(int offset, int tokenType)
        {
            var token = Lexer[offset];
            return token != null && Convert.ToInt32(token.TokenType) == tokenType;
        }

        public bool Match(int offset, int tokenType, string value)
        {
            var token = Lexer[offset];
            return token != null && Convert.ToInt32(token.TokenType) == tokenType && token.Value.Equals(value);
        }

        public TToken Consume(int tokenType)
        {
            if (Match(tokenType))
            {
                return Lexer.ConsumeToken();
            }

            // TODO INFORM THE USER
            return null;
        }

        public TToken Consume(int tokenType, string value)
        {
            if (Match(tokenType, value))
            {
                return Lexer.ConsumeToken();
            }

            //  TODO INFORM THE USER
            return null;
        }
    }
}