using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player _Player;

    void Start()
    {
        _Player = GetComponent<Player>();
        var move = new LocalPlayerMove(); 
        move.SetData(_Player.PositionTf, _Player.RotationTf, _Player.MoveSpeed);
        _Player.SetData(new LocalPlayerAttack(), move);
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0)
        {
            _Player.Move(new Vector3(h, 0, v));
        }
    }
}
