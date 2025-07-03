using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;

public class Interpreter : MonoBehaviour
{
    List<string> response = new List<string>();
    Parser parser;

    private void Start()
    {
        parser = GetComponent<Parser>();
    }
    public List<string> Interpret(string userInput)
    {
        response.Clear();

        var program = parser.produceAST(userInput);
        string json = JsonConvert.SerializeObject(program, Formatting.Indented);
        string[] MyData = json.Split("\n");

        foreach (string line in MyData)
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
