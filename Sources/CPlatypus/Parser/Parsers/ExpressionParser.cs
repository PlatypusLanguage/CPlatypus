using System;
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
                return new BinaryOperationNode(BinaryOperation.Assignment, left, ParseAssignment(parser),
                    parser.Peek().SourceLocation);

            // +=
            if (parser.AcceptType(PlatypusTokenType.PlusAssignOperator))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(
                        BinaryOperation.Addition, left, ParseAssignment(parser),
                        parser.Peek().SourceLocation),
                    parser.Peek().SourceLocation);

            // -=
            if (parser.AcceptType(PlatypusTokenType.MinusAssignOperator))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(
                        BinaryOperation.Subtraction, left, ParseAssignment(parser),
                        parser.Peek().SourceLocation),
                    parser.Peek().SourceLocation);

            // *=
            if (parser.AcceptType(PlatypusTokenType.MultiplyAssignOperator))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(
                    BinaryOperation.Multiplication, left, ParseAssignment(parser),
                    parser.Peek().SourceLocation), parser.Peek().SourceLocation);

            // /=
            if (parser.AcceptType(PlatypusTokenType.DivideAssignOperator))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(
                    BinaryOperation.Division, left, ParseAssignment(parser),
                    parser.Peek().SourceLocation), parser.Peek().SourceLocation);

            return left;
        }

        private PlatypusNode ParseLogicalOr(PlatypusParser parser)
        {
            var left = ParseLogicalAnd(parser);
            while (parser.AcceptType(PlatypusTokenType.OrOperator))
            {
                left = new BinaryOperationNode(BinaryOperation.Or, left, ParseLogicalAnd(parser),
                    parser.Peek().SourceLocation);
            }
            return left;
        }

        private PlatypusNode ParseLogicalAnd(PlatypusParser parser)
        {
            var left = ParseComparison(parser);
            while (parser.AcceptType(PlatypusTokenType.AndOperator))
            {
                left = new BinaryOperationNode(BinaryOperation.And, left, ParseComparison(parser),
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
                        left = new BinaryOperationNode(BinaryOperation.NotEqual, left,
                            ParseBinary(parser), parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.EqualOperator:
                        parser.ConsumeType(PlatypusTokenType.EqualOperator);
                        left = new BinaryOperationNode(BinaryOperation.Equal, left, ParseBinary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.GreaterOperator:
                        parser.ConsumeType(PlatypusTokenType.GreaterOperator);
                        left = new BinaryOperationNode(BinaryOperation.Greater, left,
                            ParseBinary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.LessOperator:
                        parser.ConsumeType(PlatypusTokenType.LessOperator);
                        left = new BinaryOperationNode(BinaryOperation.Less, left,
                            ParseBinary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.GreaterEqualOperator:
                        parser.ConsumeType(PlatypusTokenType.GreaterEqualOperator);
                        left = new BinaryOperationNode(BinaryOperation.GreaterEqual, left,
                            ParseBinary(parser), parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.LessEqualOperator:
                        parser.ConsumeType(PlatypusTokenType.LessEqualOperator);
                        left = new BinaryOperationNode(BinaryOperation.LessEqual, left,
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
                        left = new BinaryOperationNode(BinaryOperation.Addition, left,
                            ParseUnary(parser), parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.MinusOperator:
                        parser.ConsumeType(PlatypusTokenType.MinusOperator);
                        left = new BinaryOperationNode(BinaryOperation.Subtraction, left,
                            ParseUnary(parser), parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.MultiplyOperator:
                        parser.ConsumeType(PlatypusTokenType.MultiplyOperator);
                        left = new BinaryOperationNode(BinaryOperation.Multiplication, left, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.DivideOperator:
                        parser.ConsumeType(PlatypusTokenType.DivideOperator);
                        left = new BinaryOperationNode(BinaryOperation.Division, left, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                        continue;
                    case PlatypusTokenType.IsOperator:
                        parser.ConsumeType(PlatypusTokenType.IsOperator);
                        left = new BinaryOperationNode(BinaryOperation.Is, left, ParseUnary(parser),
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
                        return new UnaryOperationNode(UnaryOperation.Not, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                    case PlatypusTokenType.PlusPlusOperator:
                        parser.ConsumeType(PlatypusTokenType.PlusPlusOperator);
                        return new UnaryOperationNode(UnaryOperation.PreIncrement, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                    case PlatypusTokenType.MinusMinusOperator:
                        parser.ConsumeType(PlatypusTokenType.MinusMinusOperator);
                        return new UnaryOperationNode(UnaryOperation.PreDecrement, ParseUnary(parser),
                            parser.Peek().SourceLocation);
                }
            }
            else if (parser.Peek().TokenType == PlatypusTokenType.MinusOperator)
            {
                parser.ConsumeType(PlatypusTokenType.MinusOperator);
                return new UnaryOperationNode(UnaryOperation.Negate, ParseUnary(parser),
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
                    new FunctionCallNode(left, ArgumentListParser.Instance.Parse(parser) as ArgumentListNode,
                        parser.Peek().SourceLocation));
            }
            if (parser.AcceptType(PlatypusTokenType.OpenSquare))
            {
                var expression = Parse(parser);
                parser.ConsumeType(PlatypusTokenType.CloseSquare);
                return ParseAccess(parser, new ArrayAccessNode(left, expression, parser.Peek().SourceLocation));
            }
            if (parser.AcceptType(PlatypusTokenType.PlusPlusOperator))
            {
                return new UnaryOperationNode(UnaryOperation.PostIncrement, left, parser.Peek().SourceLocation);
            }
            if (parser.AcceptType(PlatypusTokenType.MinusMinusOperator))
            {
                return new UnaryOperationNode(UnaryOperation.PostDecrement, left, parser.Peek().SourceLocation);
            }
            if (parser.AcceptType(PlatypusTokenType.Dot))
            {
                var identifierNode = IdentifierParser.Instance.Parse(parser) as IdentifierNode;
                return ParseAccess(parser, new AttributeAccessNode(left, identifierNode, parser.Peek().SourceLocation));
            }

            return left;
        }

        private PlatypusNode ParseTerm(PlatypusParser parser)
        {
            if (parser.AcceptType(PlatypusTokenType.NewKeyword))
            {
                // TODO Rework this part
                return new NewNode(ParseAccess(parser), parser.Peek().SourceLocation);
            }
            if (parser.AcceptType(PlatypusTokenType.ThisKeyword))
            {
                return new ThisNode(parser.Peek().SourceLocation);
            }
            if (parser.MatchGroup(PlatypusTokenTypeGroup.TrueFalse))
            {
                return new BooleanNode(parser.MatchType(PlatypusTokenType.TrueLiteral), parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.Identifier))
            {
                return new IdentifierNode(parser.ConsumeType(PlatypusTokenType.Identifier).Value,
                    parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.RealLiteral))
            {
                return new FloatNode(float.Parse(parser.ConsumeType(PlatypusTokenType.RealLiteral).Value),
                    parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.IntegerLiteral))
            {
                return new IntegerNode(int.Parse(parser.ConsumeType(PlatypusTokenType.IntegerLiteral).Value),
                    parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.TextLiteral))
            {
                return new StringNode(parser.ConsumeType(PlatypusTokenType.TextLiteral).Value,
                    parser.Peek().SourceLocation);
            }
            if (parser.MatchType(PlatypusTokenType.CharLiteral))
            {
                return new CharNode(parser.ConsumeType(PlatypusTokenType.CharLiteral).Value[0],
                    parser.Peek().SourceLocation);
            }
            if (parser.AcceptType(PlatypusTokenType.OpenParen))
            {
                var expression = Parse(parser);
                parser.ConsumeType(PlatypusTokenType.CloseParen);
                return expression;
            }

            throw new Exception("Not reachable normally ?");
        }
    }
}