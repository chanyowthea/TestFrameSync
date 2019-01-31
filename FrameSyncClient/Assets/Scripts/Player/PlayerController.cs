using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Msg;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { private set; get; }
    public Player _Player { private set; get; }
    bool _IsMove;
    bool _IsOffLine;

    private void Awake()
    {
        Instance = this;
        _Player = GetComponent<Player>();
        var move = new LocalPlayerMove();
        move.SetData(_Player.PositionTf, _Player.RotationTf, _Player.MoveSpeed);

        _IsOffLine = Facade.Instance == null; 
        if (_IsOffLine)
        {
            _Player.SetData(new LocalPlayerAttack(), move, 0);
        }
        else
        {
            _Player.SetData(new LocalPlayerAttack(), move, Facade.Instance.LocalPlayerUserId);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0)
        {
            if (!_IsMove)
            {
                StartMove();
            }
            _IsMove = true;
            ChangeDir(new Vector2(h, v));
        }
        else
        {
            if (_IsMove)
            {
                EndMove();
            }
            _IsMove = false;
        }

        if (_IsOffLine)
        {
            FrameForward();
        }
    }

    public void FrameForward()
    {
        //if (_Player == null)
        //{
        //    return;
        //}

        if (_IsOffLine)
        {
            if (_IsMove)
            {
                _Player.Move();
            }
        }
        else
        {
            if (_Player.IsMove)
            {
                _Player.Move();
            }
        }
    }

    void StartMove()
    {
        if (!_IsOffLine)
        {
            GameSingleton._GameService.Send(new UDPMoveStart());
        }
    }

    void EndMove()
    {
        if (!_IsOffLine)
        {
            GameSingleton._GameService.Send(new UDPMoveEnd());
        }
    }

    void ChangeDir(Vector2 tVec2)
    {
        if (tVec2.x != 0)
        {
            int angle = (int)(Mathf.Atan2(tVec2.y, tVec2.x) * 180 / 3.14f);
            if (Mathf.Abs(_Player.RotationTf.Angle - angle) > 5)
            {
                Debug.LogError("ChangeDir0 angle=" + angle);
                if (!_IsOffLine)
                {
                    GameSingleton._GameService.Send(new UDPChangeDir { Angle = angle });
                }
                else
                {
                    _Player.RotationTf.Angle = angle;
                }
            }
        }
        else
        {
            int angle = tVec2.y > 0 ? 90 : -90;
            if (Mathf.Abs(_Player.RotationTf.Angle - angle) > 5)
            {
                Debug.LogError("ChangeDir angle=" + angle);
                if (!_IsOffLine)
                {
                    GameSingleton._GameService.Send(new UDPChangeDir { Angle = angle });
                }
                else
                {
                    _Player.RotationTf.Angle = angle;
                }
            }
        }
    }
}
