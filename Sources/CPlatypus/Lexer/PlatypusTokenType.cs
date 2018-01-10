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
        [PlatypusTokenGroup(UserDefined)] VarKeyword,
        [PlatypusTokenGroup(UserDefined)] StaticKeyword,
        [PlatypusTokenGroup(UserDefined)] EndKeyword,
        [PlatypusTokenGroup(UserDefined)] FunctionKeyword,
        [PlatypusTokenGroup(UserDefined)] ClassKeyword,
        [PlatypusTokenGroup(UserDefined)] ModuleKeyword,
        [PlatypusTokenGroup(UserDefined)] ConstructorKeyword,
        [PlatypusTokenGroup(UserDefined)] ThisKeyword,
        [PlatypusTokenGroup(UserDefined)] NewKeyword,
        [PlatypusTokenGroup(UserDefined)] ImportKeyword,

        [PlatypusTokenGroup(UserDefined)] ReturnKeyword,
        [PlatypusTokenGroup(UserDefined)] IfKeyword,
        [PlatypusTokenGroup(UserDefined)] ElseKeyword,
        [PlatypusTokenGroup(UserDefined)] WhileKeyword,
        [PlatypusTokenGroup(UserDefined)] ForKeyword,
        [PlatypusTokenGroup(UserDefined)] ForEachKeyword,
        [PlatypusTokenGroup(UserDefined)] InKeyword,
        [PlatypusTokenGroup(UserDefined)] SwitchKeyword,
        [PlatypusTokenGroup(UserDefined)] CaseKeyword,
        [PlatypusTokenGroup(UserDefined)] DefaultKeyword,
        [PlatypusTokenGroup(UserDefined)] TryKeyword,
        [PlatypusTokenGroup(UserDefined)] CatchKeyword,
        [PlatypusTokenGroup(UserDefined)] BreakKeyword,
        [PlatypusTokenGroup(UserDefined)] ContinueKeyword,

        [PlatypusTokenGroup(Operator, BinaryOperator)] PlusOperator,
        [PlatypusTokenGroup(Operator, BinaryOperator)] MinusOperator,
        [PlatypusTokenGroup(Operator, BinaryOperator)] MultiplyOperator,
        [PlatypusTokenGroup(Operator, BinaryOperator)] DivideOperator,

        [PlatypusTokenGroup(Operator, UnaryOperator)] PlusPlusOperator,
        [PlatypusTokenGroup(Operator, UnaryOperator)] MinusMinusOperator,
        [PlatypusTokenGroup(Operator, UnaryOperator)] NotOperator,

        [PlatypusTokenGroup(Operator, AssignOperator)] EqualAssignOperator,
        [PlatypusTokenGroup(Operator, AssignOperator)] PlusAssignOperator,
        [PlatypusTokenGroup(Operator, AssignOperator)] MinusAssignOperator,
        [PlatypusTokenGroup(Operator, AssignOperator)] MultiplyAssignOperator,
        [PlatypusTokenGroup(Operator, AssignOperator)] DivideAssignOperator,

        [PlatypusTokenGroup(Operator, ComparisonOperator)] IsOperator,
        [PlatypusTokenGroup(Operator, ComparisonOperator)] OrOperator,
        [PlatypusTokenGroup(Operator, ComparisonOperator)] AndOperator,
        [PlatypusTokenGroup(Operator, ComparisonOperator)] EqualOperator,
        [PlatypusTokenGroup(Operator, ComparisonOperator)] NotEqualOperator,
        [PlatypusTokenGroup(Operator, ComparisonOperator)] GreaterOperator,
        [PlatypusTokenGroup(Operator, ComparisonOperator)] LessOperator,
        [PlatypusTokenGroup(Operator, ComparisonOperator)] GreaterEqualOperator,
        [PlatypusTokenGroup(Operator, ComparisonOperator)] LessEqualOperator,

        Comma,
        Dot,
        Colon,

        [PlatypusTokenGroup(Literal)] IntegerLiteral,
        [PlatypusTokenGroup(Literal)] RealLiteral,
        [PlatypusTokenGroup(Literal)] CharLiteral,
        [PlatypusTokenGroup(Literal)] TextLiteral,

        [PlatypusTokenGroup(UserDefined, Literal, TrueFalse)] TrueLiteral,
        [PlatypusTokenGroup(UserDefined, Literal, TrueFalse)] FalseLiteral,

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