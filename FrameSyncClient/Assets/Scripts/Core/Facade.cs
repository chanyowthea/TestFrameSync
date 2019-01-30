using Google.Protobuf;
using Msg;
using Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Facade : MonoBehaviour
{
    public static Facade Instance { private set; get; }
    public GateService _GateService { private set; get; }
    public int LocalPlayerUserId{private set; get; }

    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        _GateService = new GateService();
        _GateService.Init();
        _GateService.AddCallback<LoginRes>(LoginCallback);
        _GateService.AddCallback<MatchRes>(MatchCallback);
    }

    private void Start()
    {
        UIManager.Instance.OpenLogin();
    }

    private void OnDestroy()
    {
        _GateService.RemoveCallback<MatchRes>(MatchCallback);
        _GateService.RemoveCallback<LoginRes>(LoginCallback);
        _GateService.Clear();
    }

    void LoginCallback(LoginRes message)
    {
        Debug.Log("message.Rs=" + message.Rs);
        LocalPlayerUserId = message.UserId; 
        UIManager.Instance.OpenMatch();
    }

    void MatchCallback(MatchRes message)
    {
        Debug.Log("message.Count=" + message.PlayerInfos.Count);
        ChangeScene(ConstValue._PlayeScene);
    }

    public void ChangeScene(string name, bool needLoading = true)
    {
        if (needLoading)
        {
            UILoading.LoadSceneAsync(name);
        }
    }
}
