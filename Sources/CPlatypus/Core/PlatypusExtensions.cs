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

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using CPlatypus.Core.Exceptions;
using CPlatypus.Execution;
using CPlatypus.Framework.Lexer;
using CPlatypus.Lexer;
using CPlatypus.Parser;

namespace CPlatypus.Core
{
    public static class PlatypusExtensions
    {
        public static string JoinToString(this object[] objects)
        {
            if (objects.Length == 0)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            sb.AppendJoin(" ", objects);
            return sb.ToString();
        }
        
        public static Delegate CreateDelegate(this MethodInfo methodInfo, object target = null)
        {
            Func<Type[], Type> getType;
            var types = methodInfo.GetParameters().Select(p => p.ParameterType);

            if (methodInfo.ReturnType == typeof(void))
            {
                getType = Expression.GetActionType;
            }
            else
            {
                getType = Expression.GetFuncType;
                types = types.Concat(new[] {methodInfo.ReturnType});
            }

            if (methodInfo.IsStatic)
            {
                return Delegate.CreateDelegate(getType(types.ToArray()), methodInfo);
            }

            return Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }

        public static string ToContextName(this PlatypusContextType contextType)
        {
            var field = contextType.GetType().GetField(contextType.ToString());
            var attributes = field.GetCustomAttributes<PlatypusContextNameAttribute>(false).ToList();

            if (attributes.Any())
            {
                return attributes.First().ContextName;
            }
            
            throw new MissingAttributeException(contextType.ToString(), nameof(PlatypusContextNameAttribute));
        }
        
        public static string ToOperatorCode(this BinaryOperator binaryOperator)
        {
            var field = binaryOperator.GetType().GetField(binaryOperator.ToString());
            var attributes = field.GetCustomAttributes<OperatorCodeAttribute>(false).ToList();

            if (attributes.Any())
            {
                return attributes.First().Code;
            }
            
            throw new MissingAttributeException(binaryOperator.ToString(), nameof(OperatorCodeAttribute));
        }
        
        public static string ToKeywordIndex(this PlatypusKeywords keyword)
        {
            var field = keyword.GetType().GetField(keyword.ToString());
            var attributes = field.GetCustomAttributes<PlatypusKeywordIndexAttribute>(false).ToList();
            
            if (attributes.Any())
            {
                return attributes.First().Index;
            }
            
            throw new MissingAttributeException(keyword.ToString(), nameof(PlatypusKeywordIndexAttribute));
        }

        public static PlatypusTokenType GetTokenType(this PlatypusKeywords keyword)
        {
            var field = keyword.GetType().GetField(keyword.ToString());
            var attributes = field.GetCustomAttributes<PlatypusKeywordIndexAttribute>(false).ToList();
            
            if (attributes.Any())
            {
                return attributes.First().TokenType;
            }

            throw new MissingAttributeException(keyword.ToString(), nameof(PlatypusKeywordIndexAttribute));
        }

        public static bool IsInTokenGroup(this PlatypusTokenType tokenType, PlatypusTokenTypeGroup group)
        {
            var field = tokenType.GetType().GetField(tokenType.ToString());
            var attributes = field.GetCustomAttributes<PlatypusTokenGroupAttribute>(false).ToList();

            if (attributes.Any())
            {
                return attributes.First().Groups.Contains(group);
            }
            
            throw new MissingAttributeException(tokenType.ToString(), nameof(PlatypusTokenGroupAttribute));
        }

        public static bool IsInTokenGroups(this PlatypusTokenType tokenType, PlatypusTokenTypeGroup[] groups)
        {
            var field = tokenType.GetType().GetField(tokenType.ToString());
            var attributes = field.GetCustomAttributes<PlatypusTokenGroupAttribute>(false).ToList();

            if (attributes.Any())
            {
                return attributes.First().Groups.Except(groups).Any();
            }
            
            throw new MissingAttributeException(tokenType.ToString(), nameof(PlatypusTokenGroupAttribute));
        }

        public static bool IsInTokenGroup(this PlatypusToken token, PlatypusTokenTypeGroup group)
        {
            return IsInTokenGroup(token.TokenType, group);
        }

        public static bool IsInTokenGroups(this PlatypusToken token, PlatypusTokenTypeGroup[] groups)
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
                {
                    return key;
                }
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