namespace cslox
{
    internal class Scanner
    {
        private readonly string source;
        private readonly List<Token> tokens = [];
        private int start = 0;
        private int current = 0;
        private int line = 1;
        private static readonly Dictionary<string, TokenType> keywords = [];

        public Scanner(string source)
        {
            this.source = source;
            keywords["and"] = TokenType.AND;
            keywords["class"] = TokenType.CLASS;
            keywords["else"] = TokenType.ELSE;
            keywords["false"] = TokenType.FALSE;
            keywords["for"] = TokenType.FOR;
            keywords["fun"] = TokenType.FUN;
            keywords["if"] = TokenType.IF;
            keywords["nil"] = TokenType.NIL;
            keywords["or"] = TokenType.OR;
            keywords["print"] = TokenType.PRINT;
            keywords["return"] = TokenType.RETURN;
            keywords["super"] = TokenType.SUPER;
            keywords["this"] = TokenType.THIS;
            keywords["true"] = TokenType.TRUE;
            keywords["var"] = TokenType.VAR;
            keywords["while"] = TokenType.WHILE;
        }

        public List<Token> scanTokens()
        {
            while (!isAtEnd())
            {
                start = current;
                scanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_BRACE); break;
                case ',': addToken(TokenType.COMMA); break;
                case '.': addToken(TokenType.DOT); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case ';': addToken(TokenType.SEMICOLON); break;
                case '*': addToken(TokenType.STAR); break;
                case '!': addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
                case '=': addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
                case '<': addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
                case '>': addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
                case '/':
                    if (match('/'))
                    {
                        // A comment goes until the end of the line.
                        while (peek() != '\n' && !isAtEnd()) advance();
                    }
                    else
                    {
                        addToken(TokenType.SLASH);
                    }
                    break;

                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.
                    break;

                case '\n':
                    line++;
                    break;

                case '"': str(); break;

                default:
                    if (isDigit(c))
                    {
                        number();
                    }
                    else if (isAlpha(c))
                    {
                        identifier();
                    }
                    else
                    {
                        Lox.error(line, "Unexpected character.");
                    }
                    break;
            }
        }

        private void identifier()
        {
            while (isAlphaNumeric(peek())) advance();

            string text = source[start..current];   
            if (keywords.ContainsKey(text))
            {
                TokenType type = keywords[text];
                type = TokenType.IDENTIFIER;
                addToken(type);
            }
            
        }

        private void number()
        {
            while (isDigit(peek())) advance();

            // Look for a fractional part
            if (peek() == '.' && isDigit(peekNext()))
            {
                // Consume the "."
                advance();

                while (isDigit(peek())) advance();
            }

            addToken(TokenType.NUMBER);
        }

        private void str()
        {
            while (peek() != '"' && !isAtEnd())
            {
                if (peek() == '\n') line++;
                advance();
            }

            if (isAtEnd())
            {
                Lox.error(line, "Unterminated string.");
                return;
            }

            // The closing ".
            advance();

            // Trim the surrounding quotes.
            string value = source[(start + 1)..(current - 1)];
            addToken(TokenType.STRING, value);
        }

        private bool match(char expected)
        {
            if (isAtEnd()) return false;
            if (source[current] != expected) return false;

            current++;
            return true;
        }

        private char peek()
        {
            if (isAtEnd()) return '\0';
            return source[current];
        }

        private char peekNext()
        {
            if (current + 1 >= source.Length) return '\0';
            return source[current + 1];
        }

        private bool isAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
        }

        private bool isAlphaNumeric(char c)
        {
            return isAlpha(c) || isDigit(c);
        }

        private bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool isAtEnd()
        {
            return current >= source.Length;
        }

        private char advance()
        {
            return source[current++];
        }

        private void addToken(TokenType type)
        {
            addToken(type, null);
        }

        private void addToken(TokenType type, object literal)
        {
            string text = source[start..current];
            tokens.Add(new Token(type, text, literal, line));
        }
    }
}
