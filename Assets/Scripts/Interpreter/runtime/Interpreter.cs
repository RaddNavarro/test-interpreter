using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    Values values;
    private void Awake()
    {
        values = GetComponent<Values>();
    }
    private RuntimeVal EvalProgram(Program program, Environment env)
    {

        RuntimeVal lastEvaluated = values.MkNull();

        foreach (Statement statement in program.body)
        {
            lastEvaluated = Evaluate(statement, env);
        }


        return lastEvaluated;
    }

    private NumberVal EvalNumericBinaryExpr(NumberVal lhs, NumberVal rhs, string Operator)
    {
        double result;

        if (Operator == "+")
        {
            result = lhs.value + rhs.value;
        }
        else if (Operator == "-")
        {
            result = lhs.value - rhs.value;
        }
        else if (Operator == "*")
        {
            result = lhs.value * rhs.value;
        }
        else if (Operator == "/")
        {
            // TODO for self: Divisiion by zero check
            result = lhs.value / rhs.value;
        }
        else
        {
            result = lhs.value % rhs.value;
        }

        return new NumberVal(result);

    }
    private RuntimeVal EvalBinaryExpr(BinaryExpr binOp, Environment env)
    {
        // lhs - left hand side
        RuntimeVal lhs = Evaluate(binOp.left, env);
        RuntimeVal rhs = Evaluate(binOp.right, env);

        if (lhs.type == ValueType.Number && rhs.type == ValueType.Number)
        {
            return EvalNumericBinaryExpr(lhs as NumberVal, rhs as NumberVal, binOp.Operator);
        }

        return values.MkNull();

    }

    private RuntimeVal EvalIdentifier(Identifier ident, Environment env)
    {
        RuntimeVal val = env.FindVar(ident.symbol);
        return val;
    }
    public RuntimeVal Evaluate(Statement astNode, Environment env)
    {
        switch (astNode.kind)
        {
            case NodeType.NumericLiteral:
                return new NumberVal((astNode as NumericLiteral).value);
            case NodeType.Identifier:
                return EvalIdentifier(astNode as Identifier, env);
            case NodeType.BinaryExpr:
                return EvalBinaryExpr(astNode as BinaryExpr, env);
            case NodeType.Program:
                return EvalProgram(astNode as Program, env);


            default:
                Debug.LogError("This AST node has not been setup yet");
                return null;

        }

    }
}
