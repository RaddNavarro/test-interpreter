using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using lexer;

public enum NodeType
{
    Program,
    NumericLiteral,
    Identifier,
    BinaryExpr,
    Unknown,
}

public class Statement
{
    public NodeType kind;
}


public class Program : Statement
{
    [JsonConverter(typeof(StringEnumConverter))]
    public new NodeType kind = NodeType.Program;
    public List<Statement> body = new();
}

public abstract class Expr : Statement { }

public class BinaryExpr : Expr
{

    [JsonConverter(typeof(StringEnumConverter))]
    public new NodeType kind = NodeType.BinaryExpr;
    public Expr left { get; }
    public Expr right { get; }
    public string Operator { get; }

    public BinaryExpr(Expr left, Expr right, string Operator)
    {
        this.left = left;
        this.right = right;
        this.Operator = Operator;
    }
}

public class Identifier : Expr
{
    [JsonConverter(typeof(StringEnumConverter))]
    public new NodeType kind = NodeType.Identifier;
    public string symbol { get; }

    public Identifier(string symbol)
    {
        this.symbol = symbol;
    }
}

public class NumericLiteral : Expr
{
    [JsonConverter(typeof(StringEnumConverter))]
    public new NodeType kind = NodeType.NumericLiteral;
    public double value { get; }

    public NumericLiteral(double value)
    {
        this.value = value;
    }
}

public class Unknown : Expr
{
    [JsonConverter(typeof(StringEnumConverter))]
    public new NodeType kind = NodeType.Unknown;

    public TokenType type { get; }
    public string value { get; }

    public Unknown(TokenType type, string value)
    {
        this.type = type;
        this.value = value;
    }

}
public class Ast : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
