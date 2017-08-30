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

using CPlatypus.Core;
using static CPlatypus.Lexer.PlatypusTokenTypeGroup;

namespace CPlatypus.Lexer
{
    public enum PlatypusTokenType
    {
        Identifier,
        [TokenGroup(UserDefined)] VarKeyword,
        [TokenGroup(UserDefined)] StaticKeyword,
        [TokenGroup(UserDefined)] EndKeyword,
        [TokenGroup(UserDefined)] FunctionKeyword,
        [TokenGroup(UserDefined)] ClassKeyword,
        [TokenGroup(UserDefined)] ConstructorKeyword,
        [TokenGroup(UserDefined)] ThisKeyword,
        [TokenGroup(UserDefined)] NewKeyword,
        [TokenGroup(UserDefined)] ImportKeyword,

        [TokenGroup(UserDefined)] IfKeyword,
        [TokenGroup(UserDefined)] ElseKeyword,
        [TokenGroup(UserDefined)] WhileKeyword,
        [TokenGroup(UserDefined)] ForKeyword,
        [TokenGroup(UserDefined)] ForEachKeyword,
        [TokenGroup(UserDefined)] InKeyword,
        [TokenGroup(UserDefined)] SwitchKeyword,
        [TokenGroup(UserDefined)] CaseKeyword,
        [TokenGroup(UserDefined)] DefaultKeyword,
        [TokenGroup(UserDefined)] TryKeyword,
        [TokenGroup(UserDefined)] CatchKeyword,
        [TokenGroup(UserDefined)] BreakKeyword,
        [TokenGroup(UserDefined)] ContinueKeyword,

        [TokenGroup(Operator, BinaryOperator)] PlusOperator,
        [TokenGroup(Operator, BinaryOperator)] MinusOperator,
        [TokenGroup(Operator, BinaryOperator)] MultiplyOperator,
        [TokenGroup(Operator, BinaryOperator)] DivideOperator,

        [TokenGroup(Operator, UnaryOperator)] PlusPlusOperator,
        [TokenGroup(Operator, UnaryOperator)] MinusMinusOperator,
        [TokenGroup(Operator, UnaryOperator)] NotOperator,

        [TokenGroup(Operator, AssignOperator)] EqualAssignOperator,
        [TokenGroup(Operator, AssignOperator)] PlusAssignOperator,
        [TokenGroup(Operator, AssignOperator)] MinusAssignOperator,
        [TokenGroup(Operator, AssignOperator)] MultiplyAssignOperator,
        [TokenGroup(Operator, AssignOperator)] DivideAssignOperator,

        [TokenGroup(Operator, ComparisonOperator)] IsOperator,
        [TokenGroup(Operator, ComparisonOperator)] OrOperator,
        [TokenGroup(Operator, ComparisonOperator)] AndOperator,
        [TokenGroup(Operator, ComparisonOperator)] EqualOperator,
        [TokenGroup(Operator, ComparisonOperator)] NotEqualOperator,
        [TokenGroup(Operator, ComparisonOperator)] GreaterOperator,
        [TokenGroup(Operator, ComparisonOperator)] LessOperator,
        [TokenGroup(Operator, ComparisonOperator)] GreaterEqualOperator,
        [TokenGroup(Operator, ComparisonOperator)] LessEqualOperator,

        Comma,
        Dot,
        Colon,

        [TokenGroup(Literal)] IntegerLiteral,
        [TokenGroup(Literal)] RealLiteral,
        [TokenGroup(Literal)] CharLiteral,
        [TokenGroup(Literal)] TextLiteral,

        [TokenGroup(UserDefined, Literal)] TrueLiteral,
        [TokenGroup(UserDefined, Literal)] FalseLiteral,

        OpenParen,
        CloseParen,
        OpenSquare,
        CloseSquare,
        OpenBracket,
        CloseBracket,

        Comment,
        WhiteSpace,
        Eos,
        Unknown
    }
}