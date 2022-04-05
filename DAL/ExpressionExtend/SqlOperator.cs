﻿using System;
using System.Linq.Expressions;

namespace DAL.ExpressionExtend
{
    internal static class SqlOperator
    {
        internal static string ToSqlOperator(this ExpressionType type)
        {
            switch (type)
            {
                case (ExpressionType.AndAlso):
                case (ExpressionType.And):
                    return "AND";
                case (ExpressionType.OrElse):
                case (ExpressionType.Or):
                    return "OR";
                case (ExpressionType.Not):
                    return "NOT";
                case (ExpressionType.NotEqual):
                    return "<>";
                case (ExpressionType.GreaterThan):
                    return ">";
                case (ExpressionType.GreaterThanOrEqual):
                    return ">=";
                case (ExpressionType.LessThan):
                    return "<";
                case (ExpressionType.LessThanOrEqual):
                    return "<=";
                case (ExpressionType.Equal):
                    return "=";
                default:
                    throw new Exception("不支持该方法");
            }
        }
    }
}

