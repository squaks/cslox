namespace cslox
{
    internal abstract class Expr
	{
		public interface IAstVisitor<R>
		{
			R visitBinaryExpr(Binary expr);
			R visitGroupingExpr(Grouping expr);
			R visitLiteralExpr(Literal expr);
			R visitUnaryExpr(Unary expr);
		}
		public class Binary : Expr
		{
			public readonly Expr left;
			public readonly Token op;
			public readonly Expr right;

			public Binary(Expr left, Token op, Expr right)
			{
				this.left = left;
				this.op = op;
				this.right = right;
			}

			public override R accept<R>(IAstVisitor<R> visitor)
			{
				return visitor.visitBinaryExpr(this);
			}
		}
		public class Grouping : Expr
		{
			public readonly Expr expression;

			public Grouping(Expr expression)
			{
				this.expression = expression;
			}

			public override R accept<R>(IAstVisitor<R> visitor)
			{
				return visitor.visitGroupingExpr(this);
			}
		}
		public class Literal : Expr
		{
			public readonly object value;

			public Literal(object value)
			{
				this.value = value;
			}

			public override R accept<R>(IAstVisitor<R> visitor)
			{
				return visitor.visitLiteralExpr(this);
			}
		}
		public class Unary : Expr
		{
			public readonly Token op;
			public readonly Expr right;

			public Unary(Token op, Expr right)
			{
				this.op = op;
				this.right = right;
			}

			public override R accept<R>(IAstVisitor<R> visitor)
			{
				return visitor.visitUnaryExpr(this);
			}
		}

		public abstract R accept<R>(IAstVisitor<R> visitor);
	}
}
