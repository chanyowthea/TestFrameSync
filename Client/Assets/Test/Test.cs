using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    void Start()
    {
        Action<object> a = ConvertToObjAction<string>(Run); 
        a("22"); 
    }

    void Run(string s)
    {
        Debug.Log("run! " + s);
    }

    public Action<object> ConvertToObjAction<T>(Action<T> tAction)
    {
        if (tAction == null)
        {
            return null;
        }
        else
        {
            return new Action<object>((obj) => tAction((T)obj));
        }
    }
}
