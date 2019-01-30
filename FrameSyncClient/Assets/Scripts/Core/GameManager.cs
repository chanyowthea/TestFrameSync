using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Msg;
using System.Linq;

public class GameManager : MonoBehaviour
{
    Dictionary<int, UDPFrameData> _FrameMessages = new Dictionary<int, UDPFrameData>();
    Dictionary<int, UDPFrameData> _PartialFrameMessages = new Dictionary<int, UDPFrameData>();
    readonly float _UpdateDeltaTime = 0.02f;
    float _LapsedTime;
    public int CurGameFrame { private set; get; }
    readonly int _MaxSmoothFrame = 3;
    int _CurSmoothFrame = 0;
    readonly int _MaxForwardTimePerFrame = 5;

    void Awake()
    {
        GameSingleton.Init();
        GameSingleton._GameService.AddCallback<UDPMoveStart>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPMoveEnd>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPChangeDir>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPReleaseSkill>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPFrameData>(OnProcessMsg);
        GameSingleton._GameService.Send(new UDPGameStart{ UserId = Facade.Instance.LocalPlayerUserId});
    }

    void Update()
    {
        if (_LapsedTime < _UpdateDeltaTime)
        {
            _LapsedTime += Time.deltaTime;
            return;
        }
        _LapsedTime = 0; 
        FrameForward();
    }

    void OnDestroy()
    {
        GameSingleton._GameService.RemoveCallback<UDPFrameData>(OnProcessMsg);
        GameSingleton._GameService.RemoveCallback<UDPMoveStart>(OnProcessMsg);
        GameSingleton._GameService.RemoveCallback<UDPMoveEnd>(OnProcessMsg);
        GameSingleton._GameService.RemoveCallback<UDPChangeDir>(OnProcessMsg);
        GameSingleton._GameService.RemoveCallback<UDPReleaseSkill>(OnProcessMsg);
        GameSingleton.Clear();
    }

    void FrameForward()
    {
        int lastestFrameNumber = 0;
        if (_FrameMessages.Count > 0)
        {
            lastestFrameNumber = _FrameMessages.ElementAt(_FrameMessages.Count - 1).Value.FrameNumber;
        }
        int gap = lastestFrameNumber - CurGameFrame;
        if (gap < 0)
        {
            gap = 0;
        }
        for (int k = 0; k < Mathf.Clamp(gap, 1, _MaxForwardTimePerFrame); k++)
        {
            if (_CurSmoothFrame == 0)
            {
                if (_FrameMessages.Count == 0)
                {
                    return;
                }

                var message = _FrameMessages.ElementAt(0).Value;
                _FrameMessages.Remove(message.FrameNumber);
                Debug.Log("FrameNumber=" + message.FrameNumber);
                for (int i = 0, length = message.Msgs.Count; i < length; i++)
                {
                    GameSingleton._GameService.ProcessReceivedMessage(message.Msgs[i].ToByteArray());
                }
                PlayerController.Instance.FrameForward();
                GameSingleton._PhysicSystem.UpdateCollider();
                CurGameFrame += 1;
                _CurSmoothFrame += 1;
            }
            else
            {
                PlayerController.Instance.FrameForward();
                GameSingleton._PhysicSystem.UpdateCollider();
                _CurSmoothFrame += 1;
                if (_CurSmoothFrame == _MaxSmoothFrame)
                {
                    _CurSmoothFrame = 0;
                }
            }
        }
    }

    void OnProcessMsg(UDPFrameData message)
    {
        Debug.LogError("OnProcessMsg(UDPFrameData message) frame number=" + message.FrameNumber);
        if (_FrameMessages.Count > 0)
        {
            int lastFrame = _FrameMessages.ElementAt(_FrameMessages.Count - 1).Value.FrameNumber;
            if (message.FrameNumber > lastFrame + 1)
            {
                // supply frame
                _PartialFrameMessages.Add(message.FrameNumber, message);
                // request frame [lastFrame+1, message.FrameNumber-1]
            }
            else if (message.FrameNumber == lastFrame + 1)
            {
                _FrameMessages.Add(message.FrameNumber, message);
            }
            // message.FrameNumber <= lastFrame
            // use the newest message. 
            else
            {
                int firstFrame = _FrameMessages.ElementAt(0).Value.FrameNumber;
                if (message.FrameNumber >= firstFrame)
                {
                    _FrameMessages[message.FrameNumber] = message;
                }
            }
        }
    }

    void OnProcessMsg(UDPMoveStart msg)
    {
        PlayerController.Instance._Player.StartMove();
    }

    void OnProcessMsg(UDPMoveEnd msg)
    {
        PlayerController.Instance._Player.EndMove();
    }

    void OnProcessMsg(UDPChangeDir msg)
    {
        PlayerController.Instance._Player.ChangeDir(msg.Angle);
    }

    void OnProcessMsg(UDPReleaseSkill msg)
    {
        Debug.Log("OnProcessMsg UDPReleaseSkill.SkillId=" + msg.SkillId);
    }
}
