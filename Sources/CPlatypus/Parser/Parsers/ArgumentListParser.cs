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
using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class ArgumentListParser : PlatypusNodeParser
    {
        public static ArgumentListParser Instance { get; } = new ArgumentListParser();

        private ArgumentListParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            throw new Exception("This function shouldn't be used ever !");
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var argumentListNode = new ArgumentListNode(parser.NextId(), parser.Peek().SourceLocation);
            parser.ConsumeType(PlatypusTokenType.OpenParen);
            while (!parser.MatchType(PlatypusTokenType.CloseParen))
            {
                argumentListNode.Children.Add(ExpressionParser.Instance.Parse(parser));
                if (!parser.AcceptType(PlatypusTokenType.Comma)) break;
            }
            parser.ConsumeType(PlatypusTokenType.CloseParen);
            return argumentListNode;
        }
    }
}