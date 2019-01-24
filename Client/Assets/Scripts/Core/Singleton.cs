using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Singleton
{
    public static GameService _GameService;
    public static PhysicalSystem _PhysicSystem;

    public static void Init()
    {
        _GameService = new GameService();
        _GameService.Init();
        _PhysicSystem = new PhysicalSystem(); 
        _PhysicSystem.Open(); 
    }

    public static void Clear()
    {
        _PhysicSystem.Close();
        _GameService.Clear();
        _GameService = null;
    }
}
