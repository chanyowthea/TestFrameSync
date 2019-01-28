using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Msg;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { private set; get; }
    public Player _Player{ private set; get; }
    bool _IsMove;

    private void Awake()
    {
        Instance = this; 
    }

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
            _IsMove = true;
            if (_IsMove)
            {
                StartMove();
            }
            ChangeDir(new Vector3(h, 0, v));
        }
        else
        {
            _IsMove = false;
            if (!_IsMove)
            {
                EndMove();
            }
        }
    }

    void StartMove()
    {
        GameSingleton._GameService.Send(new UDPMoveStart());
    }

    void EndMove()
    {
        GameSingleton._GameService.Send(new UDPMoveEnd());
    }

    void ChangeDir(Vector3 tVec2)
    {
        if (tVec2.x != 0)
        {
            int angle = (int)(Mathf.Atan2(tVec2.y, tVec2.x) * 180 / 3.14f);
            if (Mathf.Abs(_Player.RotationTf.Angle - angle) > 5)
            {
                GameSingleton._GameService.Send(new UDPChangeDir{ Angle = angle});
            }
        }
        else
        {
            int angle = tVec2.y > 0 ? 90 : -90;
            if (Mathf.Abs(_Player.RotationTf.Angle - angle) > 5)
            {
                GameSingleton._GameService.Send(new UDPChangeDir { Angle = angle });
            }
        }
    }
}
