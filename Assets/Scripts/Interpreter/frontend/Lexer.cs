using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using lexer;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements.Experimental;


namespace lexer
{

    public enum TokenType
    {
        NUMBER,
        IDENTIFIER,
        EQUALS,

        OPEN_PAREN, CLOSE_PAREN,
        BINARY_OPERATOR,

        LET, IF, ELSE, ELIF,
        UNKNOWN,
        EOF,
    }
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }


}

public class Lexer : MonoBehaviour
{
    //   string sampleString = "let x = foo * bar\nif 45 + 3";
    private Dictionary<string, TokenType> KEYWORDS = new Dictionary<string, TokenType>
    {
        {"let", TokenType.LET},
        {"if", TokenType.IF}
     };
    //    private void Start()
    //    {
    //        List<Token> tokens = tokenize(sampleString);
    //
    //        foreach (Token token in tokens)
    //        {
    //            Debug.Log(token.Type + ": '" + token.Value + "'");
    //        }
    //        Debug.Log(sampleString);
    //    }

    private bool IsWhitespace(char c)
    {
        return ('\n' == c || '\r' == c || char.IsWhiteSpace(c));
    }
    public List<Token> tokenize(string sourceCode)
    {
        List<Token> tokens = new List<Token>();
        int i = 0;

        while (i < sourceCode.Length)
        {
            char c = sourceCode[i];

            if (IsWhitespace(c))
            {
                i++;
                continue;
            }

            if (char.IsDigit(c))
            {
                string number = "";
                while (i < sourceCode.Length && char.IsDigit(sourceCode[i]))
                {
                    number += sourceCode[i++];

                }
                tokens.Add(new Token(TokenType.NUMBER, number));

                continue;
            }

            if (char.IsLetter(c))
            {
                string word = "";
                while (i < sourceCode.Length && char.IsLetterOrDigit(sourceCode[i]))
                {
                    word += sourceCode[i++];
                }
                TokenType reserved;
                if (!KEYWORDS.TryGetValue(word, out reserved))
                {
                    tokens.Add(new Token(TokenType.IDENTIFIER, word));
                    continue;
                }
                else
                {
                    tokens.Add(new Token(reserved, word));
                    continue;
                }
            }


            if ("+-*/%".Contains(c))
            {
                tokens.Add(new Token(TokenType.BINARY_OPERATOR, c.ToString()));
                i++;
                continue;
            }

            if (c == '(')
            {
                tokens.Add(new Token(TokenType.OPEN_PAREN, c.ToString()));
                i++;
                continue;
            }

            if (c == ')')
            {
                tokens.Add(new Token(TokenType.CLOSE_PAREN, c.ToString()));
                i++;
                continue;
            }

            if (c == '=')
            {
                tokens.Add(new Token(TokenType.EQUALS, c.ToString()));
                i++;
                continue;
            }
            tokens.Add(new Token(TokenType.UNKNOWN, c.ToString()));
            i++;
            continue;
        }
        tokens.Add(new Token(TokenType.EOF, ""));

        return tokens;
    }
}