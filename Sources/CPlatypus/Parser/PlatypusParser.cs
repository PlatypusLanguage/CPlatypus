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

using System.Collections.Generic;
using CPlatypus.Core;
using CPlatypus.Framework.Parser;
using CPlatypus.Lexer;
using CPlatypus.Parser.Parsers;

namespace CPlatypus.Parser
{
    public class PlatypusParser : Parser<PlatypusToken, PlatypusNode>
    {
        public List<NodeParser> Parsers;

        private int _counter;

        public PlatypusParser(PlatypusLexer lexer) : base(lexer)
        {
            _counter = 0;
            Parsers = new List<NodeParser>
            {
                IfParser.Instance,
                WhileParser.Instance,
                ForParser.Instance,
                VariableDeclarationParser.Instance,
                FunctionParser.Instance,
                ReturnParser.Instance,
                ClassParser.Instance,
                ModuleParser.Instance,
                ConstructorParser.Instance,
                ExpressionParser.Instance
            };
        }

        public int NextId()
        {
            return _counter++;
        }

        public override PlatypusNode Parse()
        {
            var topNode = CodeParser.Instance.ParseTill(this, PlatypusTokenType.Eos);
            return topNode;
        }

        public bool MatchGroup(PlatypusTokenTypeGroup tokenTypeGroup)
        {
            return Peek().IsInTokenGroup(tokenTypeGroup);
        }

        public bool MatchType(PlatypusTokenType tokenType)
        {
            return base.MatchType((int) tokenType);
        }

        public bool MatchTypeValue(PlatypusTokenType tokenType, string value)
        {
            return base.MatchTypeValue((int) tokenType, value);
        }

        public bool MatchType(int offset, PlatypusTokenType tokenType)
        {
            return base.MatchType(offset, (int) tokenType);
        }

        public bool MatchValueType(int offset, PlatypusTokenType tokenType, string value)
        {
            return base.MatchTypeValue(offset, (int) tokenType, value);
        }

        public bool AcceptType(PlatypusTokenType tokenType)
        {
            return base.AcceptType((int) tokenType);
        }

        public bool AcceptTypeValue(PlatypusTokenType tokenType, string value)
        {
            return base.AcceptTypeValue((int) tokenType, value);
        }

        public PlatypusToken PeekType(PlatypusTokenType tokenType)
        {
            return base.PeekType((int) tokenType);
        }

        public PlatypusToken PeekType(int offset, PlatypusTokenType tokenType)
        {
            return base.PeekType(offset, (int) tokenType);
        }

        public new PlatypusToken Consume()
        {
            return base.Consume();
        }
        
        public PlatypusToken ConsumeType(PlatypusTokenType tokenType)
        {
            return base.ConsumeType((int) tokenType);
        }

        public PlatypusToken ConsumeTypeValue(PlatypusTokenType tokenType, string value)
        {
            return base.ConsumeTypeValue((int) tokenType, value);
        }
    }
}