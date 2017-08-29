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

using System.Linq;
using CPlatypus.Framework;

namespace CPlatypus.Lexer.Matcher
{
    public class OperatorMatcher : IMatcher
    {
        public PlatypusToken Match(SourceLocation location, Source source)
        {
            if (!new[] {'+', '-', '*', '/', '!', '=', '>', '<'}.Contains(source.PeekChar()))
            {
                return null;
            }

            var currentChar = source.PopChar();
            switch (currentChar)
            {
                case '+':
                case '-':
                    if (currentChar == source.PeekChar())
                    {
                        var result = currentChar.ToString() + source.PopChar();
                        return new PlatypusToken(
                            result == "++" ? PlatypusTokenType.PlusPlusOperator : PlatypusTokenType.MinusMinusOperator,
                            result, location);
                    }
                    else if (source.PeekChar() == '=')
                    {
                        var result = currentChar.ToString() + source.PopChar();
                        return new PlatypusToken(
                            result == "+="
                                ? PlatypusTokenType.PlusAssignOperator
                                : PlatypusTokenType.MinusAssignOperator,
                            result, location);
                    }
                    return new PlatypusToken(
                        currentChar == '+' ? PlatypusTokenType.PlusOperator : PlatypusTokenType.MinusOperator,
                        currentChar.ToString(),
                        location);
                case '*':
                case '/':
                    if (source.PeekChar() == '=')
                    {
                        var result = currentChar.ToString() + source.PopChar();
                        return new PlatypusToken(
                            result == "*="
                                ? PlatypusTokenType.MultiplyAssignOperator
                                : PlatypusTokenType.DivideAssignOperator,
                            result,
                            location);
                    }
                    return new PlatypusToken(
                        currentChar == '*' ? PlatypusTokenType.MultiplyOperator : PlatypusTokenType.DivideOperator,
                        currentChar.ToString(),
                        location);
                case '!':
                case '=':
                    if (source.PeekChar() == '=')
                    {
                        var result = currentChar.ToString() + source.PopChar();
                        return new PlatypusToken(
                            result == "==" ? PlatypusTokenType.EqualOperator : PlatypusTokenType.NotEqualOperator,
                            result, location);
                    }
                    if (currentChar == '=')
                    {
                        return new PlatypusToken(PlatypusTokenType.EqualAssignOperator, currentChar.ToString(),
                            location);
                    }
                    return new PlatypusToken(PlatypusTokenType.NotOperator, currentChar.ToString(),
                        location);
                case '>':
                case '<':
                    if (source.PeekChar() == '=')
                    {
                        var result = currentChar.ToString() + source.PopChar();
                        return new PlatypusToken(
                            result == ">="
                                ? PlatypusTokenType.GreaterEqualOperator
                                : PlatypusTokenType.LessEqualOperator,
                            result, location);
                    }
                    return new PlatypusToken(
                        currentChar == '>' ? PlatypusTokenType.GreaterOperator : PlatypusTokenType.LessOperator,
                        currentChar.ToString(), location);
                default:
                    return null;
            }
        }
    }
}