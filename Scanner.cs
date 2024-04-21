namespace cslox
{
    internal class Scanner
    {
        private readonly string source;
        private readonly List<Token> tokens = new();
        private int start = 0;
        private int current = 0;
        private int line = 1;

        public Scanner(string source)
        {
            this.source = source;
        }

        List<Token> scanTokens()
        {
            while (!isAtEnd())
            {
                start = current;
                //scanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        private bool isAtEnd()
        {
            return current >= source.Length;
        }
    }
}
