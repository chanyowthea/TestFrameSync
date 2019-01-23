using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        List<int> list = new List<int>();
        list.Add(2);
        list.Add(1);
        list.Add(3);
        list.Sort((x, y) => x - y);

        string s = "";
        for (int i = 0, length = list.Count; i < length; i++)
        {
            s += list[i] + ", ";
        }
        Debug.LogError(s);
    }

    void Update()
    {

    }
}
