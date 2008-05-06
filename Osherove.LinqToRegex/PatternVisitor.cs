using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlimFlan.ReadableRex;

namespace Osherove.LinqToRegex
{
    internal class PatternVisitor
    {
        private Pattern m_pattern = new Pattern("");
        private object returnValueFromLastExpressionVisit;

        public Pattern VisitExpression(Expression expression)
        {
            if (returnValueFromLastExpressionVisit == null)
            {
                returnValueFromLastExpressionVisit = m_pattern;
            }
            if (expression is MethodCallExpression)
            {
                VisitMethodCall((MethodCallExpression) expression);
            }
            else if (expression is LambdaExpression)
            {
                VisitExpression(((LambdaExpression) expression).Body);
            }
            else if (expression is MemberExpression)
            {
                VisitMemberExpression(((MemberExpression) expression));
            }
            return m_pattern;
        }

        private void VisitMemberExpression(MemberExpression expression)
        {
            VisitExpression(expression.Expression);

            string memberToInvoke = expression.Member.Name;
            PropertyInfo property;
            if (expression.Expression == null) //method is static
            {
                property = typeof (Pattern).GetProperty(memberToInvoke, BindingFlags.Public | BindingFlags.Static);
            }
            else
            {
                property = returnValueFromLastExpressionVisit.GetType().GetProperty(memberToInvoke,
                                                                                    BindingFlags.Public |
                                                                                    BindingFlags.Instance);
            }
            returnValueFromLastExpressionVisit = property.GetValue(returnValueFromLastExpressionVisit, null);
        }

        private void VisitMethodCall(MethodCallExpression expression)
        {
            VisitExpression(expression.Object);
            string methodName = expression.Method.Name;
            object objToInvokeUpon = returnValueFromLastExpressionVisit;

            MethodInfo methodInfo = objToInvokeUpon.GetType().GetMethod(methodName,
                                                                        BindingFlags.Public | BindingFlags.Instance);
            int paramCount = methodInfo.GetParameters().Count();
            Array paramArray = Array.CreateInstance(typeof (Object), paramCount);

            for (int arrLocation = 0; arrLocation < expression.Arguments.Count; arrLocation++)
            {
                Expression argument = expression.Arguments[arrLocation];
                VisitExpression(argument);
                if (argument is ConstantExpression)
                {
                    object argValue = ((ConstantExpression) argument).Value;
                    paramArray.SetValue(argValue, arrLocation);
                }
                else
                {
                    object argValue = returnValueFromLastExpressionVisit;
                    paramArray.SetValue(argValue, arrLocation);
                    Console.WriteLine();
                }
            }
            var array = (object[]) paramArray;
            returnValueFromLastExpressionVisit = methodInfo.Invoke(objToInvokeUpon, array);
            if (returnValueFromLastExpressionVisit is Pattern)
            {
                m_pattern = (Pattern) returnValueFromLastExpressionVisit;
            }
        }


        private void VisitAndAlso(BinaryExpression andAlso)
        {
            VisitExpression(andAlso.Left);
            VisitExpression(andAlso.Right);
        }
    }
}