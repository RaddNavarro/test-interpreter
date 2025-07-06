#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Environment
{
    private Environment? parent;
    private Dictionary<string, RuntimeVal> variables;

    public Environment()
    {
        this.variables = new Dictionary<string, RuntimeVal>();
    }

    public Environment(Environment parentENV)
    {
        this.parent = parentENV;
        this.variables = new Dictionary<string, RuntimeVal>();
    }


    public RuntimeVal DeclareVar(string varName, RuntimeVal value)
    {
        if (this.variables.TryGetValue(varName, out var val))
            throw new Exception("Cannot declare variable " + varName + ". As it already defined");

        this.variables.Add(varName, value);
        return value;
    }

    public RuntimeVal AssignVar(string varName, RuntimeVal value)
    {
        Environment env = this.Resolve(varName);

        env.variables.Add(varName, value);

        return value;
    }

    public RuntimeVal FindVar(string varName)
    {
        Environment env = this.Resolve(varName);
        return env.variables[varName];
    }
    public Environment Resolve(string varName)
    {
        if (this.variables.TryGetValue(varName, out var val))
            return this;

        if (this.parent == null)
            throw new Exception("Cannot resolve " + varName + " as it does not exist");

        return this.parent.Resolve(varName);

    }
}
