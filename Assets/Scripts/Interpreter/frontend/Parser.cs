using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using lexer;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public Statement statement;
    public Program program;
    public Expr expr;
    public BinaryExpr binaryExpr;
    public NumericLiteral numericLiteral;
    public Identifier identifier;
    public Lexer lexer;

    private List<Token> tokens = new List<Token>();

    private void Start()
    {
        lexer = GetComponent<Lexer>();
    }
    private bool NotEOF()
    {
        return this.tokens[0].Type != TokenType.EOF;
    }

    private Statement ParseStmt()
    {
        return this.ParseExpr();
    }

    private Token GetToken()
    {

        return this.tokens[0] as Token;
    }

    private Token Eat()
    {
        var prev = this.tokens[0];
        this.tokens.RemoveAt(0);
        return prev;
    }

    private Token Expect(TokenType type, string error)
    {
        var prev = this.tokens[0];
        this.tokens.RemoveAt(0);

        if (prev == null || prev.Type != type)
        {
            Debug.LogError("Parse Error: \n" + error + " " + prev.ToString() + " - Expecting: " + type.ToString());
            return null;
        }

        return prev;
    }

    private Expr ParseExpr()
    {
        return this.ParseAdditiveExpr();
    }

    private Expr ParseAdditiveExpr()
    {
        Expr left = this.ParseMultiplicativeExpr();

        while (this.GetToken().Value == "+" || this.GetToken().Value == "-")
        {
            string Operator = this.Eat().Value;
            Expr right = this.ParseMultiplicativeExpr();
            left = new BinaryExpr(left, right, Operator);
        }

        return left;
    }
    private Expr ParseMultiplicativeExpr()
    {
        Expr left = this.ParsePrimaryExpr();

        while (this.GetToken().Value == "/" || this.GetToken().Value == "*" || this.GetToken().Value == "%")
        {
            string Operator = this.Eat().Value;
            Expr right = this.ParsePrimaryExpr();
            left = new BinaryExpr(left, right, Operator);
        }

        return left;
    }

    private Expr ParsePrimaryExpr()
    {
        var token = this.GetToken().Type;

        switch (token)
        {
            case TokenType.IDENTIFIER:
                return new Identifier(this.Eat().Value);
            case TokenType.NUMBER:
                return new NumericLiteral(float.Parse(this.Eat().Value));
            case TokenType.OPEN_PAREN:
                this.Eat();
                Expr value = this.ParseExpr();
                this.Expect(TokenType.CLOSE_PAREN, "Unexpected token. Expected closing parenthesis");
                return value;

            default:
                Debug.Log("Unexpected token: " + "'" + this.GetToken().Value.ToString() + "'" + this.GetToken().Type);
                var unknown = new Unknown(this.GetToken().Type, this.GetToken().Value);
                this.Eat();
                return unknown;

        }
    }
    public Program produceAST(string sourceCode)
    {
        this.tokens = lexer.tokenize(sourceCode);

        Program program = new Program();

        while (NotEOF())
        {
            program.body.Add(this.ParseStmt());
        }


        return program;
    }

}
