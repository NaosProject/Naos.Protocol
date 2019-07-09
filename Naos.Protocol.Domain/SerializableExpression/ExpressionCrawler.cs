// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionCrawler.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class ExpressionCrawler
    {
        public static IEnumerable<ExpressionDescriptionBase> LinkNodes(this ExpressionDescriptionBase source)
        {
            //returns all the "paths" or "links" from a node in an expression tree
            //  each expression type that has "links" from it has different kinds of links
            if (source is LambdaExpressionDescription)
            {
                yield return (source as LambdaExpressionDescription).Body;
                foreach (ParameterExpressionDescription parm in (source as LambdaExpressionDescription).Parameters)
                    yield return parm;
            }
            else if (source is BinaryExpressionDescription)
            {
                yield return (source as BinaryExpressionDescription).Left;
                yield return (source as BinaryExpressionDescription).Right;
            }
            else if (source is ConditionalExpressionDescription)
            {
                yield return (source as ConditionalExpressionDescription).IfTrue;
                yield return (source as ConditionalExpressionDescription).IfFalse;
                yield return (source as ConditionalExpressionDescription).Test;
            }
            else if (source is InvocationExpressionDescription)
                foreach (ExpressionDescriptionBase x in (source as InvocationExpressionDescription).Arguments)
                    yield return x;
            else if (source is ListInitExpressionDescription)
            {
                yield return (source as ListInitExpressionDescription).NewExpressionDescription;
                foreach (ElementInitDescription x in (source as ListInitExpressionDescription).Initializers)
                    foreach (ExpressionDescriptionBase ex in x.Arguments)
                        yield return ex;
            }
            else if (source is MemberExpressionDescription)
                yield return (source as MemberExpressionDescription).ExpressionDescription;
            else if (source is MemberInitExpressionDescription)
                yield return (source as MemberInitExpressionDescription).NewExpressionDescription;
            else if (source is MethodCallExpressionDescription)
            {
                foreach (ExpressionDescriptionBase x in (source as MethodCallExpressionDescription).Arguments)
                    yield return x;
                yield return (source as MethodCallExpressionDescription).ParentObject;
            }
            else if (source is NewArrayExpressionDescription)
                foreach (ExpressionDescriptionBase x in (source as NewArrayExpressionDescription).Expressions)
                    yield return x;
            else if (source is NewExpressionDescription)
                foreach (ExpressionDescriptionBase x in (source as NewExpressionDescription).Arguments)
                    yield return x;
            else if (source is TypeBinaryExpressionDescription)
                yield return (source as TypeBinaryExpressionDescription).ExpressionDescription;
            else if (source is UnaryExpressionDescription)
                yield return (source as UnaryExpressionDescription).Operand;
        }

        //return all the nodes in a given expression tree
        public static IEnumerable<ExpressionDescriptionBase> VisitAllNodes(this ExpressionDescriptionBase source)
        {
            //i.e. left, right, body, etc.
            foreach (ExpressionDescriptionBase linkNode in source.LinkNodes())
                //recursive call to get the nodes from the tree, until you hit terminals
                foreach (ExpressionDescriptionBase subNode in linkNode.VisitAllNodes())
                    yield return subNode;
            yield return source; //return the root of this most recent call
        }

        public static IEnumerable<Expression> LinkNodes(this Expression source)
        {
            //returns all the "paths" or "links" from a node in an expression tree
            //  each expression type that has "links" from it has different kinds of links
            if (source is LambdaExpression)
            {
                yield return (source as LambdaExpression).Body;
                foreach (ParameterExpression parm in (source as LambdaExpression).Parameters)
                    yield return parm;
            }
            else if (source is BinaryExpression)
            {
                yield return (source as BinaryExpression).Left;
                yield return (source as BinaryExpression).Right;
            }
            else if (source is ConditionalExpression)
            {
                yield return (source as ConditionalExpression).IfTrue;
                yield return (source as ConditionalExpression).IfFalse;
                yield return (source as ConditionalExpression).Test;
            }
            else if (source is InvocationExpression)
                foreach (Expression x in (source as InvocationExpression).Arguments)
                    yield return x;
            else if (source is ListInitExpression)
            {
                yield return (source as ListInitExpression).NewExpression;

                yield return (source as ListInitExpression).NewExpression;
                foreach (ElementInit x in (source as ListInitExpression).Initializers)
                foreach (Expression ex in x.Arguments)
                    yield return ex;
            }
            else if (source is MemberExpression)
                yield return (source as MemberExpression).Expression;
            else if (source is MemberInitExpression)
                yield return (source as MemberInitExpression).NewExpression;
            else if (source is MethodCallExpression)
            {
                foreach (Expression x in (source as MethodCallExpression).Arguments)
                    yield return x;
                yield return (source as MethodCallExpression).Object;
            }
            else if (source is NewArrayExpression)
                foreach (Expression x in (source as NewArrayExpression).Expressions)
                    yield return x;
            else if (source is NewExpression)
                foreach (Expression x in (source as NewExpression).Arguments)
                    yield return x;
            else if (source is TypeBinaryExpression)
                yield return (source as TypeBinaryExpression).Expression;
            else if (source is UnaryExpression)
                yield return (source as UnaryExpression).Operand;
        }

        //return all the nodes in a given expression tree
        public static IEnumerable<Expression> VisitAllNodes(this Expression source)
        {
            //i.e. left, right, body, etc.
            foreach (Expression linkNode in source.LinkNodes())
                //recursive call to get the nodes from the tree, until you hit terminals
                foreach (Expression subNode in linkNode.VisitAllNodes())
                    yield return subNode;
            yield return source; //return the root of this most recent call
        }
    }
}
