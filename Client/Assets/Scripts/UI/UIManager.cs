using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { private set; get; }

    [SerializeField] UILogin _UILogin;
    [SerializeField] UIMatch _UIMatch;
    [SerializeField] UILoading _UILoading;

    List<GameObject> _OpenedUIs = new List<GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void OpenLogin()
    {
        CloseAllUIs();
        var ui = GameObject.Instantiate(_UILogin);
        _OpenedUIs.Add(ui.gameObject);
    }

    public void OpenMatch()
    {
        CloseAllUIs();
        var ui = GameObject.Instantiate(_UIMatch);
        _OpenedUIs.Add(ui.gameObject);
    }

    public void OpenLoading()
    {
        CloseAllUIs();
        var ui = GameObject.Instantiate(_UILoading);
        _OpenedUIs.Add(ui.gameObject);
    }

    void CloseAllUIs()
    {
        for (int i = 0, length = _OpenedUIs.Count; i < length; i++)
        {
            GameObject.Destroy(_OpenedUIs[i].gameObject);
        }
    }
}
