using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Msg;

public class Launcher : MonoBehaviour
{
    void Awake()
    {
        Singleton.Init();
        Singleton._NetworkManager.AddCallback<LoginRes>(LoginCallback);
        Singleton._NetworkManager.Send(new LoginReq{ AccountName = "kitty"});
    }

    private void OnDestroy()
    {
        Singleton.Clear();
    }

    void LoginCallback(LoginRes message)
    {
        Debug.Log("message.Rs=" + message.Rs);
    }

    void Update()
    {
        Singleton._PhysicSystem.UpdateCollider();
    }
}
