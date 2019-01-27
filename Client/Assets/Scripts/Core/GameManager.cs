using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Msg;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        GameSingleton.Init();
        GameSingleton._GameService.AddCallback<UDPMoveStart>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPMoveEnd>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPChangeDir>(OnProcessMsg);
        GameSingleton._GameService.AddCallback<UDPReleaseSkill>(OnProcessMsg);
    }

    void Update()
    {
        GameSingleton._PhysicSystem.UpdateCollider();
        //Debug.Log(Time.deltaTime);
    }

    void OnDestroy()
    {
        GameSingleton._GameService.RemoveCallback<UDPMoveStart>(OnProcessMsg);
        GameSingleton._GameService.RemoveCallback<UDPMoveEnd>(OnProcessMsg);
        GameSingleton._GameService.RemoveCallback<UDPChangeDir>(OnProcessMsg);
        GameSingleton._GameService.RemoveCallback<UDPReleaseSkill>(OnProcessMsg);
        GameSingleton.Clear(); 
    }

    void OnProcessMsg(UDPMoveStart msg)
    {

    }

    void OnProcessMsg(UDPMoveEnd msg)
    {

    }

    void OnProcessMsg(UDPChangeDir msg)
    {

    }

    void OnProcessMsg(UDPReleaseSkill msg)
    {

    }
}
