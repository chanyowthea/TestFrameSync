using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Msg;
using System.Linq;

public class GameManager : MonoBehaviour
{
    Dictionary<int, UDPFrameData> _FrameMessages = new Dictionary<int, UDPFrameData>();
    Dictionary<int, UDPFrameData> _PartialFrameMessages = new Dictionary<int, UDPFrameData>();
    float _UpdateDeltaTime = 0.02f;
    float _LapsedTime;
    public int CurGameFrame{ private set; get; } 

    void Awake()
    {
        GameSingleton.Init();
        GameSingleton._GameService.AddCallback<UDPMoveStart>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPMoveEnd>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPChangeDir>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPReleaseSkill>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPFrameData>(OnProcessMsg);
    }

    void Update()
    {
        if (_LapsedTime < _UpdateDeltaTime)
        {
            _UpdateDeltaTime += Time.deltaTime; 
            return; 
        }

        //_FrameMessages.ElementAt();
        //Debug.Log("FrameNumber=" + message.FrameNumber);
        //for (int i = 0, length = message.Msgs.Count; i < length; i++)
        //{
        //    GameSingleton._GameService.ProcessReceivedMessage(message.Msgs[i].ToByteArray());
        //}
        //GameSingleton._PhysicSystem.UpdateCollider();
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

    }

    void OnProcessMsg(UDPFrameData message)
    {
        if (_FrameMessages.Count > 0)
        {
            int lastFrame = _FrameMessages.ElementAt(_FrameMessages.Count - 1).Value.FrameNumber;
            if (message.FrameNumber > lastFrame + 1)
            {
                // request frame [lastFrame+1, message.FrameNumber-1]
            }
            else if (message.FrameNumber == lastFrame + 1)
            {
                _FrameMessages.Add(message.FrameNumber, message);
            }
            // message.FrameNumber <= lastFrame
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
