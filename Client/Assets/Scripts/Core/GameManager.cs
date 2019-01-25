using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Msg;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        Singleton.Init();
        Singleton._GameService.AddCallback<UDPMoveStart>(OnProcessMsg);
        Singleton._GameService.AddCallback<UDPMoveEnd>(OnProcessMsg);
        Singleton._GameService.AddCallback<UDPChangeDir>(OnProcessMsg);
        Singleton._GameService.AddCallback<UDPReleaseSkill>(OnProcessMsg);
    }

    void Update()
    {
        Singleton._PhysicSystem.UpdateCollider();
        Debug.Log(Time.deltaTime);
    }

    void OnDestroy()
    {
        Singleton._GameService.RemoveCallback<UDPMoveStart>(OnProcessMsg);
        Singleton._GameService.RemoveCallback<UDPMoveEnd>(OnProcessMsg);
        Singleton._GameService.RemoveCallback<UDPChangeDir>(OnProcessMsg);
        Singleton._GameService.RemoveCallback<UDPReleaseSkill>(OnProcessMsg);
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
