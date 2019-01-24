using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
        Singleton.Init();
        Singleton._NetworkManager.AddCallback<LoginRes>(LoginCallback);
        Singleton._NetworkManager.Send(new LoginReq { AccountName = "kitty" });
    }

    private void OnDestroy()
    {
        Singleton.Clear();
    }

    void LoginCallback(LoginRes message)
    {
        Debug.Log("message.Rs=" + message.Rs);
    }
}
