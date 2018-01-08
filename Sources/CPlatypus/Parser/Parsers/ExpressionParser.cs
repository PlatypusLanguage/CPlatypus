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
using System.Globalization;
using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class ExpressionParser : NodeParser
    {
        public static ExpressionParser Instance { get; } = new ExpressionParser();

        private ExpressionParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return true;
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            return ParseAssignment(parser);
        }

        private PlatypusNode ParseAssignment(PlatypusParser parser)
        {
            var left = ParseLogicalOr(parser);

            // =
            if (parser.AcceptType(PlatypusTokenType.EqualAssignOperator))
                return new BinaryOperationNode(parser.NextId(), BinaryOperation.Assignment, left,
                    ParseAssignment(parser),
                    parser.Peek().SourceLocation);

            // +=
            if (parser.AcceptType(PlatypusTokenType.PlusAssignOperator))
                return new BinaryOperationNode(parser.NextId(), BinaryOperation.Assignment, left,
                    new BinaryOperationNode(
                        parser.NextId(), BinaryOperation.Addition, left, ParseAssignment(parser),
                        parser.Peek().SourceLocation),
                    parser.Peek().SourceLocation);

            // -=
            if (parser.AcceptType(PlatypusTokenType.MinusAssignOperator))
                return new BinaryOperationNode(parser.NextId(), BinaryOperation.Assignment, left,
                    new BinaryOperationNode(
                        parser.NextId(), BinaryOperation.Subtraction, left, ParseAssignment(parser),
                        parser.Peek().SourceLocation),
                    parser.Peek().SourceLocation);

            // *=
            if (parser.AcceptType(PlatypusTokenType.MultiplyAssignOperator))
                return new BinaryOperationNode(parser.NextId(), BinaryOperation.Assignment, left,
                    new BinaryOperationNode(
                        parser.NextId(), BinaryOperation.Multiplication, left, ParseAssignment(parser),
                        parser.Peek().SourceLocation), parser.Peek().SourceLocation);

            // /=
            if (parser.AcceptType(PlatypusTokenType.DivideAssignOperator))
                return new BinaryOperationNode(parser.NextId(), BinaryOperation.Assignment, left,
                    new BinaryOperationNode(
                        parser.NextId(), BinaryOperation.Division, left, ParseAssignment(parser),
                        parser.Peek().SourceLocation), parser.Peek().SourceLocation);

            return left;
        }

        private PlatypusNode ParseLogicalOr(PlatypusParser parser)
        {
            var left = ParseLogicalAnd(parser);
            while (parser.AcceptType(PlatypusTokenType.OrOperator))
            {
                left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Or, left, ParseLogicalAnd(parser),
                    parser.Peek().SourceLocation);
            }
            return left;
        }

        private PlatypusNode ParseLogicalAnd(PlatypusParser parser)
        {
            var left = ParseComparison(parser);
            while (parser.AcceptType(PlatypusTokenType.AndOperator))
            {
                left = new BinaryOperationNode(parser.NextId(), BinaryOperation.And, left, ParseComparison(parser),
                    parser.Peek().SourceLocation);
            }
            return left;
        }

        private PlatypusNode ParseComparison(PlatypusParser parser)
        {
            var left = ParseBinary(parser);
            while (parser.MatchGroup(PlatypusTokenTypeGroup.ComparisonOperator))
            {
                switch (parser.Peek().TokenType)
                {
                    case PlatypusTokenType.NotEqualOperator:
                        parser.ConsumeType(PlatypusTokenType.NotEqualOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.NotEqual, left,
                            ParseBinary(parser), parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.EqualOperator:
                        parser.ConsumeType(PlatypusTokenType.EqualOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Equal, left,
                            ParseBinary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.GreaterOperator:
                        parser.ConsumeType(PlatypusTokenType.GreaterOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Greater, left,
                            ParseBinary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.LessOperator:
                        parser.ConsumeType(PlatypusTokenType.LessOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Less, left,
                            ParseBinary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.GreaterEqualOperator:
                        parser.ConsumeType(PlatypusTokenType.GreaterEqualOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.GreaterEqual, left,
                            ParseBinary(parser), parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.LessEqualOperator:
                        parser.ConsumeType(PlatypusTokenType.LessEqualOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.LessEqual, left,
                            ParseBinary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                }

                break;
            }
            return left;
        }


        private PlatypusNode ParseBinary(PlatypusParser parser)
        {
            var left = ParseUnary(parser);
            while (parser.MatchGroup(PlatypusTokenTypeGroup.BinaryOperator))
            {
                switch (parser.Peek().TokenType)
                {
                    case PlatypusTokenType.PlusOperator:
                        parser.ConsumeType(PlatypusTokenType.PlusOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Addition, left,
                            ParseUnary(parser), parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.MinusOperator:
                        parser.ConsumeType(PlatypusTokenType.MinusOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Subtraction, left,
                            ParseUnary(parser), parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.MultiplyOperator:
                        parser.ConsumeType(PlatypusTokenType.MultiplyOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Multiplication, left,
                            ParseUnary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.DivideOperator:
                        parser.ConsumeType(PlatypusTokenType.DivideOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Division, left,
                            ParseUnary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.IsOperator:
                        parser.ConsumeType(PlatypusTokenType.IsOperator);
                        left = new BinaryOperationNode(parser.NextId(), BinaryOperation.Is, left, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                }
                break;
            }
            return left;
        }

        private PlatypusNode ParseUnary(PlatypusParser parser)
        {
            if (parser.MatchGroup(PlatypusTokenTypeGroup.UnaryOperator))
            {
                switch (parser.Peek().TokenType)
                {
                    case PlatypusTokenType.NotOperator:
                        parser.ConsumeType(PlatypusTokenType.NotOperator);
                        return new UnaryOperationNode(parser.NextId(), UnaryOperation.Not, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                    case PlatypusTokenType.PlusPlusOperator:
                        parser.ConsumeType(PlatypusTokenType.PlusPlusOperator);
                        return new UnaryOperationNode(parser.NextId(), UnaryOperation.PreIncrement, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                    case PlatypusTokenType.MinusMinusOperator:
                        parser.ConsumeType(PlatypusTokenType.MinusMinusOperator);
                        return new UnaryOperationNode(parser.NextId(), UnaryOperation.PreDecrement, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                }
            }
            else if (parser.Peek().TokenType == PlatypusTokenType.MinusOperator)
            {
                parser.ConsumeType(PlatypusTokenType.MinusOperator);
                return new UnaryOperationNode(parser.NextId(), UnaryOperation.Negate, ParseUnary(parser),
                    parser.Peek().SourceLocation);
            }
            return ParseAccess(parser);
        }

        private PlatypusNode ParseAccess(PlatypusParser parser)
        {
            return ParseAccess(parser, ParseTerm(parser));
        }

        private PlatypusNode ParseAccess(PlatypusParser parser, PlatypusNode left)
        {
            if (parser.MatchType(PlatypusTokenType.OpenParen))
            {
                return ParseAccess(parser,
                    new FunctionCallNode(parser.NextId(), left,
                        ArgumentListParser.Instance.Parse(parser) as ArgumentListNode,
                        parser.Peek().SourceLocation));
            }
            if (parser.AcceptType(PlatypusTokenType.OpenSquare))
            {
                var expression = Parse(parser);
                parser.ConsumeType(PlatypusTokenType.CloseSquare);
                return ParseAccess(parser,
                    new ArrayAccessNode(parser.NextId(), left, expression, parser.Peek().SourceLocation));
            }
            if (parser.AcceptType(PlatypusTokenType.PlusPlusOperator))
            {
                return new UnaryOperationNode(parser.NextId(), UnaryOperation.PostIncrement, left,
                    parser.Peek().SourceLocation);
            }
            if (parser.AcceptType(PlatypusTokenType.MinusMinusOperator))
            {
                return new UnaryOperationNode(parser.NextId(), UnaryOperation.PostDecrement, left,
                    parser.Peek().SourceLocation);
            }
            if (parser.AcceptType(PlatypusTokenType.Dot))
            {
                var identifierNode = IdentifierParser.Instance.Parse(parser) as IdentifierNode;
                return ParseAccess(parser,
                    new AttributeAccessNode(parser.NextId(), left, identifierNode, parser.Peek().SourceLocation));
            }

            return left;
        }

        private PlatypusNode ParseTerm(PlatypusParser parser)
        {
            if (parser.AcceptType(PlatypusTokenType.NewKeyword))
            {
                // TODO Rework this part
                return new NewNode(parser.NextId(), ParseAccess(parser), parser.Peek().SourceLocation);
            }
            if (parser.AcceptType(PlatypusTokenType.ThisKeyword))
            {
                return new ThisNode(parser.NextId(), parser.Peek().SourceLocation);
            }
            if (parser.MatchGroup(PlatypusTokenTypeGroup.TrueFalse))
            {
                return new BooleanNode(parser.NextId(), true, parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.Identifier))
            {
                return new IdentifierNode(parser.NextId(), parser.ConsumeType(PlatypusTokenType.Identifier).Value,
                    parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.RealLiteral))
            {
                return new FloatNode(parser.NextId(),
                    float.Parse(parser.ConsumeType(PlatypusTokenType.RealLiteral).Value,
                        CultureInfo.InvariantCulture.NumberFormat), parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.IntegerLiteral))
            {
                return new IntegerNode(parser.NextId(),
                    int.Parse(parser.ConsumeType(PlatypusTokenType.IntegerLiteral).Value,
                        CultureInfo.InvariantCulture.NumberFormat), parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.TextLiteral))
            {
                return new StringNode(parser.NextId(), parser.ConsumeType(PlatypusTokenType.TextLiteral).Value,
                    parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.CharLiteral))
            {
                return new CharNode(parser.NextId(), parser.ConsumeType(PlatypusTokenType.CharLiteral).Value[0],
                    parser.Peek().SourceLocation);
            }
            if (parser.AcceptType(PlatypusTokenType.OpenParen))
            {
                var expression = Parse(parser);
                parser.ConsumeType(PlatypusTokenType.CloseParen);
                return expression;
            }
            if (parser.MatchType(PlatypusTokenType.Unknown))
            {
                throw new Exception("Unknown token found : " + parser.ConsumeType(PlatypusTokenType.Unknown));
            }
            
            throw new Exception("Not reachable normally ?");
        }
    }
}