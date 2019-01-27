using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogin : MonoBehaviour
{
    public void OnClickLogin()
    {
        Facade.Instance._GateService.Send(new LoginReq { AccountName = "kitty" });
    }
}
