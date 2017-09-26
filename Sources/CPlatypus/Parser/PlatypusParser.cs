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
using System.Collections.Generic;
using CPlatypus.Framework;
using CPlatypus.Framework.Lexer;
using CPlatypus.Framework.Parser;
using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;
using CPlatypus.Parser.Parsers;

namespace CPlatypus.Parser
{
    public class PlatypusParser : Parser<PlatypusToken, PlatypusNode>
    {
        private List<IPlatypusParser> _parsers;

        public PlatypusParser(PlatypusLexer lexer) : base(lexer)
        {
            _parsers = new List<IPlatypusParser>
            {
                new VariableDeclarationParser()
            };
        }

        public override PlatypusNode Parse()
        {
            var currentNode = new CodeNode(new SourceLocation(0, 0));

            while (Lexer.CurrentToken.TokenType != PlatypusTokenType.Eos)
            {
                foreach (var parser in _parsers)
                {
                    if (parser.Match(this))
                    {
                        currentNode.Children.Add(parser.Parse(this));
                        break;
                    }
                }
            }
        
            return currentNode;
        }

        public bool Match(PlatypusTokenType tokenType)
        {
            return base.Match((int)tokenType);
        }

        public bool Match(PlatypusTokenType tokenType, string value)
        {
            return base.Match((int)tokenType, value);
        }

        public bool Match(int offset, PlatypusTokenType tokenType)
        {
            return base.Match(offset, (int)tokenType);
        }

        public bool Match(int offset, PlatypusTokenType tokenType, string value)
        {
            return base.Match(offset, (int)tokenType, value);
        }

        public PlatypusToken Consume(PlatypusTokenType tokenType)
        {
            return base.Consume((int)tokenType);
        }

        public PlatypusToken Consume(PlatypusTokenType tokenType, string value)
        {
            return base.Consume((int)tokenType, value);
        }
    }
}