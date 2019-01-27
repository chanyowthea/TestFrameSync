using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMatch : MonoBehaviour
{
    public void OnClickMatch()
    {
        Facade.Instance._GateService.Send(new MatchReq());
    }
}
