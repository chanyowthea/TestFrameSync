using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Msg;

public class Launcher : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Singleton._PhysicSystem.UpdateCollider();
        Debug.Log(Time.deltaTime);
    }
}
