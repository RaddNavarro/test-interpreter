using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;

public class Interpret : MonoBehaviour
{
    List<string> response = new List<string>();
    Parser parser;
    Interpreter interpreter;
    Values values;

    private void Awake()
    {
        values = GetComponent<Values>();
        parser = GetComponent<Parser>();
        interpreter = GetComponent<Interpreter>();
    }
    public List<string> ProcessInterpret(string userInput)
    {
        response.Clear();
        Environment env = new Environment();

        env.DeclareVar("x", values.MkNum(100));
        env.DeclareVar("true", values.MkBool(true));
        env.DeclareVar("false", values.MkBool(false));
        env.DeclareVar("null", values.MkNull());
        var program = parser.ProduceAST(userInput);
        //        string json = JsonConvert.SerializeObject(program, Formatting.Indented);
        //        string[] MyData = json.Split("\n");
        //
        //        foreach (string line in MyData)
        //        {
        //            response.Add(line);
        //        }

        var result = interpreter.Evaluate(program, env);
        string resJson = JsonConvert.SerializeObject(result, Formatting.Indented);
        string[] Result = resJson.Split("\n");

        foreach (string line in Result)
        {
            response.Add(line);
        }

        return response;

        //        string[] args = userInput.Split();
        //
        //        if (args[0] == "help")
        //        {
        //            response.Add("Welcome to the test terminal");
        //            response.Add("type -commands to see list of commandss");
        //
        //            return response;
        //        }
        //        else
        //        {
        //            response.Add("Command not recogniezd");
        //            return response;
        //        }
    }
}
