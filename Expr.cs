namespace cslox
{
	internal abstract class Expr<R>
	{
		public interface IAstVisitor<R>
		{
			R visitBinaryExpr(Binary expr);
			R visitGroupingExpr(Grouping expr);
			R visitLiteralExpr(Literal expr);
			R visitUnaryExpr(Unary expr);
		}
		public class Binary : Expr<R>
		{
			readonly Expr<R> left;
			readonly Token op;
			readonly Expr<R> right;

			public Binary(Expr<R> left, Token op, Expr<R> right)
			{
				this.left = left;
				this.op = op;
				this.right = right;
			}

			public override R accept(IAstVisitor<R> visitor)
			{
				return visitor.visitBinaryExpr(this);
			}
		}
		public class Grouping : Expr<R>
		{
			readonly Expr<R> expression;

			public Grouping(Expr<R> expression)
			{
				this.expression = expression;
			}

			public override R accept(IAstVisitor<R> visitor)
			{
				return visitor.visitGroupingExpr(this);
			}
		}
		public class Literal : Expr<R>
		{
			readonly object value;

			public Literal(object value)
			{
				this.value = value;
			}

			public override R accept(IAstVisitor<R> visitor)
			{
				return visitor.visitLiteralExpr(this);
			}
		}
		public class Unary : Expr<R>
		{
			readonly Token op;
			readonly Expr<R> right;

			public Unary(Token op, Expr<R> right)
			{
				this.op = op;
				this.right = right;
			}

			public override R accept(IAstVisitor<R> visitor)
			{
				return visitor.visitUnaryExpr(this);
			}
		}

		public abstract R accept(IAstVisitor<R> visitor);
	}
}
