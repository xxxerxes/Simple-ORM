using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace DAL.ExpressionExtend
{
    public class CustomExpressionVisitor : ExpressionVisitor
    {
        private Stack<string> ConditionStack = new Stack<string>();
        private Dictionary<string, string> ValueDic = new Dictionary<string, string>();
        private Dictionary<string, int> counter = new Dictionary<string, int>();
        private Stack<string> ValueStack = new Stack<string>(1);

        public string GetWhere()
        {
            string where = string.Concat(ConditionStack.ToArray());
            ConditionStack.Clear();
            return where;
        }

        public Dictionary<string, string> GetValuesDic()
        {
            Dictionary<string, string> dic = ValueDic;
            return dic;
        }

        [return: NotNullIfNotNull("node")]
        public override Expression? Visit(Expression? node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            //ConditionStack.Push(" ) ");
            ConditionStack.Push(" ");
            base.Visit(node.Left);
            ConditionStack.Push(node.NodeType.ToSqlOperator());
            base.Visit(node.Right);
            //ConditionStack.Push(" ( ");
            ConditionStack.Push(" ");
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            string key = ValueStack.Pop();

            ConditionStack.Push(key);
            ValueDic.Add(counter[key] > 1 ? key + counter[key] : key, node.Value.ToString());

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            string key = node.Member.Name;
            if (counter.ContainsKey(key))
            {
                counter[key]++;
                key += counter[key];
                ConditionStack.Push($"@{key}");
            }
            else
            {
                counter.Add(key, 1);
                ConditionStack.Push($"@{node.Member.Name}");
            }
            ValueStack.Push(node.Member.Name);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression == null) throw new ArgumentNullException("MethodCallExpression");

            if (expression.Method.Name == "StartsWith" || expression.Method.Name == "Contains" || expression.Method.Name == "EndsWith")
            {
                Visit(expression.Object);
                Visit(expression.Arguments[0]);
                string right = ConditionStack.Pop();
                string left = ConditionStack.Pop();
                ConditionStack.Push($" {right} Like {left} ");

                string current = counter[right] > 1 ? right + counter[right] : right;

                switch (expression.Method.Name)
                {
                    case "StartsWith":
                        ValueDic[current] = ValueDic[current] + "%";
                        break;
                    case "Contains":
                        ValueDic[current] = "%" + ValueDic[current] + "%";
                        break;
                    case "EndsWith":
                        ValueDic[current] = "%" + ValueDic[current];
                        break;
                }

                return expression;
            }
            else
            {
                throw new NotSupportedException(expression.NodeType + "is not supported");
            }
        }
    }
}

