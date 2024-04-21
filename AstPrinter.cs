using System.Text;

namespace cslox
{
    internal class AstPrinter : Expr.IAstVisitor<string>
    {
        public string print(Expr expr)
        {
            return expr.accept(this);
        }

         public string visitBinaryExpr(Expr.Binary expr)
        {
            return parenthesize(expr.op.lexeme,
                                expr.left, expr.right);
        }

        public string visitGroupingExpr(Expr.Grouping expr)
        {
            return parenthesize("group", expr.expression);
        }

        public string visitLiteralExpr(Expr.Literal expr)
        {
            if (expr.value is null) return "nil";
            return expr.value.ToString();
        }

        public string visitUnaryExpr(Expr.Unary expr)
        {
            return parenthesize(expr.op.lexeme, expr.right);
        }

        private string parenthesize(string name, params Expr[] exprs)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append('(').Append(name);
            for (int i = 0; i < exprs.Length; i++)
            {
                builder.Append(' ');
                builder.Append(exprs[i].accept(this));
            }
            builder.Append(')');

            return builder.ToString();
        }

        public static void main(string[] args)
        {
            Expr e = new Expr.Binary(new Expr.Unary(new Token(TokenType.MINUS, "-", null, 1),
            new Expr.Literal(123)),
            new Token(TokenType.STAR, "*", null, 1),
            new Expr.Grouping(
            new Expr.Literal(45.67)));

            Console.WriteLine(new AstPrinter().print(e));
        }
    }
}
