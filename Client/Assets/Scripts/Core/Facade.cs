using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade : MonoBehaviour
{
    public Facade Instance{ private set; get; } 
    GateService _GateService;
    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        _GateService = new GateService(); 
        _GateService.Init();
        _GateService.AddCallback<LoginRes>(LoginCallback);
        _GateService.Send(new LoginReq { AccountName = "kitty" });
    }

    private void OnDestroy()
    {
        _GateService.RemoveCallback<LoginRes>(LoginCallback);
        _GateService.Clear();
    }

    void LoginCallback(LoginRes message)
    {
        Debug.Log("message.Rs=" + message.Rs);
    }
}
