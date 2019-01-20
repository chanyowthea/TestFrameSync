using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Singleton
{
    public static NetworkManager _NetworkManager;
    public static PhysicalSystem _PhysicSystem;

    public static void Init()
    {
        _NetworkManager = new NetworkManager();
        _NetworkManager.Init();
        _PhysicSystem = new PhysicalSystem(); 
        _PhysicSystem.Open(); 
    }

    public static void Clear()
    {
        _PhysicSystem.Close();
        _NetworkManager.Clear();
        _NetworkManager = null;
    }
}
