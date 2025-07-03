using UnityEngine;
using Newtonsoft.Json;

public class Main : MonoBehaviour
{
    public string sample;

    // Start is called before the first frame update
    void Start()
    {
        Parser parser = GetComponent<Parser>();

        var program = parser.produceAST(sample);
        string json = JsonConvert.SerializeObject(program, Formatting.Indented);
        Debug.Log(json);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
