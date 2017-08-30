﻿/*
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
using System.Linq;
using System.Reflection;
using CPlatypus.Framework.Lexer;
using CPlatypus.Lexer;

namespace CPlatypus.Core
{
    public static class PlatypusExtensions
    {
        public static string ToKeywordIndex(this PlatypusKeywords keyword)
        {
            var attributes = (KeywordIndex[])keyword.GetType().GetField(keyword.ToString()).GetCustomAttributes(typeof(KeywordIndex), false);
            return attributes.Length > 0 ? attributes[0].Index : string.Empty;
        }

        public static PlatypusTokenType GetTokenType(this PlatypusKeywords keyword)
        {
            var attributes = (KeywordIndex[])keyword.GetType().GetField(keyword.ToString()).GetCustomAttributes(typeof(KeywordIndex), false);
            return attributes.Length > 0 ? attributes[0].TokenType : PlatypusTokenType.Unknown;
        }

        public static bool IsInTokenGroup(this PlatypusTokenType tokenType, PlatypusTokenTypeGroup group)
        {
            var attributes = (TokenGroup[])tokenType.GetType().GetField(tokenType.ToString()).GetCustomAttributes(typeof(TokenGroup), false);
            return attributes.Length > 0 && attributes[0].Groups.Contains(group);
        }

        public static bool IsInTokenGroups(this PlatypusTokenType tokenType, params PlatypusTokenTypeGroup[] groups)
        {
            var attributes = (TokenGroup[])tokenType.GetType().GetField(tokenType.ToString()).GetCustomAttributes(typeof(TokenGroup), false);
            return attributes.Length > 0 && !attributes[0].Groups.Except(groups).Any();
        }

        public static bool IsInTokenGroup(this PlatypusToken token, PlatypusTokenTypeGroup group)
        {
            return IsInTokenGroup(token.TokenType, group);
        }

        public static bool IsInTokenGroups(this PlatypusToken token, params PlatypusTokenTypeGroup[] groups)
        {
            return IsInTokenGroups(token.TokenType, groups);
        }

        public static PlatypusTokenType ToPlatypusTokenType(this string str)
        {
            return ToPlatypusKeyword(str).GetTokenType();
        }

        public static PlatypusKeywords ToPlatypusKeyword(this string str)
        {
            foreach (var key in Enum.GetValues(typeof(PlatypusKeywords)).Cast<PlatypusKeywords>())
            {
                if (key.ToKeywordIndex() == str)
                    return key;
            }
            throw new Exception("Keyword not found with string : " + str);
        }

        public static PlatypusToken ToPlatypusToken(this Token token)
        {
            var platypusToken = token as PlatypusToken;
            return platypusToken ?? new PlatypusToken(PlatypusTokenType.Unknown, token.Value, token.SourceLocation);
        }

        public static char ProcessEscapeCode(this char c)
        {
            switch (c)
            {
                case '\\':
                    return '\\';
                case '"':
                    return '\"';
                case '\'':
                    return '\'';
                case 'b':
                    return '\b';
                case 'f':
                    return '\f';
                case 'n':
                    return '\n';
                case 'r':
                    return '\r';
                case 't':
                    return '\t';
                case '#':
                    return '#';
                default:
                    return '\0';
            }
        }
    }
}