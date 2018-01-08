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
using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class CodeParser : PlatypusNodeParser
    {
        public static CodeParser Instance { get; } = new CodeParser();

        private CodeParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            throw new Exception("Maybe this shouldn't be used ever ?");
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            return ParseTill(parser);
        }

        public CodeNode ParseTill(PlatypusParser parser, PlatypusTokenType tokenType = PlatypusTokenType.EndKeyword)
        {
            return ParseTill(parser, new List<PlatypusTokenType> {tokenType}, out _);
        }

        public CodeNode ParseTill(PlatypusParser parser, List<PlatypusTokenType> tokenTypes, out PlatypusTokenType tokenType)
        {
            var codeNode = new CodeNode(parser.NextId(), parser.Peek().SourceLocation);
            tokenType = PlatypusTokenType.Unknown;

            while (!tokenTypes.Contains(parser.Lexer.CurrentToken.TokenType))
            {
                var success = false;
                foreach (var p in parser.Parsers)
                {
                    if (p.Match(parser))
                    {
                        var node = p.Parse(parser);
                        codeNode.Children.Add(node);
                        success = true;
                        break;
                    }
                }
                if (!success)
                {
                    throw new Exception("Debug parser error #1 : Failed to parse!");
                }
            }

            if (tokenTypes.Contains(parser.Lexer.CurrentToken.TokenType))
            {
                tokenType = parser.Lexer.CurrentToken.TokenType;
                parser.ConsumeType(tokenType);
            }

            return codeNode;
        }
    }
}