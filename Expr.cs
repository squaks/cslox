namespace cslox
{
	internal abstract class Expr
	{
		public class Binary : Expr
		{
			readonly Expr left;
			readonly Token op;
			readonly Expr right;

			public Binary(Expr left, Token op, Expr right)
			{
				this.left = left;
				this.op = op;
				this.right = right;
			}
		}
		public class Grouping : Expr
		{
			readonly Expr expression;

			public Grouping(Expr expression)
			{
				this.expression = expression;
			}
		}
		public class Literal : Expr
		{
			readonly object value;

			public Literal(object value)
			{
				this.value = value;
			}
		}
		public class Unary : Expr
		{
			readonly Token op;
			readonly Expr right;

			public Unary(Token op, Expr right)
			{
				this.op = op;
				this.right = right;
			}
		}
	}
}
