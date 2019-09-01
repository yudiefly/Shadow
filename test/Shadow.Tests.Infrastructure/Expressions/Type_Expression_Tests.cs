using System;
using System.Linq.Expressions;
using Xunit;

namespace Shadow.Tests.Infrastructure.Expressions
{
    public class Type_Expression_Tests
    {
        [Fact]
        public void Should_ConvertToPropertyExpression_Test()
        {
            // Func<ExpressionTestModel, object> f = s => s.Name;

            var lambdaParam = Expression.Parameter(typeof(ExpressionTestModel));
            var lambdaBody = Expression.Property(lambdaParam, "Name");
            var exp = Expression.Lambda<Func<ExpressionTestModel, object>>(lambdaBody, lambdaParam);

            var func = exp.Compile();
            var result = func(new ExpressionTestModel { Name = "123abc" });

            Assert.NotNull(result);
            Assert.True(result.Equals("123abc"), result.ToString());
        }

        [Fact]
        public void Should_ConvertExpressionToProperty_Test()
        {
            Expression<Func<ExpressionTestModel, object>> expression = s => s.Name;
            var name = ((MemberExpression)expression.Body).Member.Name;

            Assert.NotNull(name);
            Assert.True(name == nameof(ExpressionTestModel.Name));
        }
    }

    internal class ExpressionTestModel
    {
        public string Name { get; set; }
    }
}
