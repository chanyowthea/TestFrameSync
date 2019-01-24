using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        PlayerInfo info = new PlayerInfo(); 
        info.PlayerName = "test name"; 
        Debug.Log(info.PlayerName);
    }

    void Update()
    {

    }
}
