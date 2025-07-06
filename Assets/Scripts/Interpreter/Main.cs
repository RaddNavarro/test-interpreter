using UnityEngine;
using Newtonsoft.Json;

public class Main : MonoBehaviour
{
    public string sample;
    Values values;

    void Awake()
    {
        values = GetComponent<Values>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Parser parser = GetComponent<Parser>();
        Environment env = new Environment();
        env.DeclareVar("x", values.MkNum(100));
        Interpreter interpreter = GetComponent<Interpreter>();


        var program = parser.ProduceAST(sample);

        string json = JsonConvert.SerializeObject(program, Formatting.Indented);
        Debug.Log(json);

        var result = interpreter.Evaluate(program, env);
        string resJson = JsonConvert.SerializeObject(result, Formatting.Indented);
        Debug.Log(resJson);

    }

}
