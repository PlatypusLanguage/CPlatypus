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

using CPlatypus.Framework.Lexer;
using System;

namespace CPlatypus.Framework.Parser
{
    public abstract class Parser<TToken, TNode> where TToken : Token where TNode : Node<TNode>
    {
        public readonly Lexer<TToken> Lexer;

        protected Parser(Lexer<TToken> lexer)
        {
            Lexer = lexer;
        }

        public abstract TNode Parse();

        public bool MatchType(int tokenType)
        {
            return MatchType(0, tokenType);
        }

        public bool MatchTypeValue(int tokenType, string value)
        {
            return MatchTypeValue(0, tokenType, value);
        }

        public bool MatchType(int offset, int tokenType)
        {
            var token = Lexer[offset];
            return token != null && Convert.ToInt32(token.TokenType) == tokenType;
        }

        public bool MatchTypeValue(int offset, int tokenType, string value)
        {
            var token = Lexer[offset];
            return token != null && Convert.ToInt32(token.TokenType) == tokenType && token.Value.Equals(value);
        }

        public bool AcceptType(int tokenType)
        {
            if (!MatchType(tokenType)) return false;
            ConsumeType(tokenType);
            return true;
        }

        public bool AcceptTypeValue(int tokenType, string value)
        {
            if (!MatchTypeValue(tokenType, value)) return false;
            ConsumeTypeValue(tokenType, value);
            return true;
        }

        public TToken Peek(int offset = 0)
        {
            return Lexer[offset];
        }

        public TToken PeekType(int tokenType)
        {
            return PeekType(0, tokenType);
        }

        public TToken PeekType(int offset, int tokenType)
        {
            if (MatchType(offset, tokenType))
            {
                return Lexer[offset];
            }

            // TODO INFORM THE USER
            return null;
        }

        public TToken Consume()
        {
            return Lexer.ConsumeToken();
        }

        public TToken ConsumeType(int tokenType)
        {
            if (MatchType(tokenType))
            {
                return Lexer.ConsumeToken();
            }

            // TODO INFORM THE USER
            return null;
        }

        public TToken ConsumeTypeValue(int tokenType, string value)
        {
            if (MatchTypeValue(tokenType, value))
            {
                return Lexer.ConsumeToken();
            }

            //  TODO INFORM THE USER
            return null;
        }
    }
}