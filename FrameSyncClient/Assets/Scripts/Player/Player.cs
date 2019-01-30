using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMove
{
    void Move();
}

public interface IPlayerAttack
{
    void Attack(Player player);
}

public class LocalPlayerMove : IPlayerMove
{
    CustomTransform _PositionTf;
    CustomTransform _RotationTf;
    FixedPointF _MoveSpeed;

    public void SetData(CustomTransform position, CustomTransform rotation, FixedPointF speed)
    {
        _PositionTf = position;
        _RotationTf = rotation;
        _MoveSpeed = speed;
    }

    // 移动罗盘只控制方向，不控制速度
    public void Move()
    {
        CustomVector3 temp;

        FixedPointF x = CustomMath.GetCos(_RotationTf.Angle);
        FixedPointF z = CustomMath.GetSin(_RotationTf.Angle);

        temp.x = x * _MoveSpeed * ConstValue._RenderFrameRate;
        temp.y = new FixedPointF(0);
        temp.z = z * _MoveSpeed * ConstValue._RenderFrameRate;

        _PositionTf.LocalPosition += temp;
    }
}

public class LocalPlayerAttack : IPlayerAttack
{
    public void Attack(Player player)
    {

    }
}

public class NetPlayerMove : IPlayerMove
{
    public void Move()
    {

    }
}

public class NetPlayerAttack : IPlayerAttack
{
    public void Attack(Player player)
    {

    }
}

public class Player : MonoBehaviour, IPlayerAttack, IPlayerMove
{
    int _UserId; 
    public bool IsMove{ private set; get; }
    IPlayerAttack _IPlayerAttack;
    IPlayerMove _IPlayerMove;
    FixedPointF _MoveSpeed = new FixedPointF(3);
    [SerializeField] CustomTransform _PositionTf;
    [SerializeField] CustomTransform _RotationTf;

    public void SetData(IPlayerAttack playerAttack, IPlayerMove playerMove, int userId)
    {
        _IPlayerAttack = playerAttack;
        _IPlayerMove = playerMove;
        _UserId = userId; 
    }

    public void ClearData()
    {
        _IPlayerAttack = null;
        _IPlayerMove = null;
    }

    public void Attack(Player player)
    {
        _IPlayerAttack.Attack(player);
    }

    public void ChangeDir(int angle)
    {
        _RotationTf.Angle = angle;
    }

    public void Move()
    {
        _IPlayerMove.Move();
    }

    public void StartMove()
    {
        IsMove = true;
    }

    public void EndMove()
    {
        IsMove = false;
    }

    public CustomVector3 Position
    {
        get
        {
            return _PositionTf.Position;
        }
        set
        {
            _PositionTf.Position = value;
        }
    }

    public int Angle
    {
        get
        {
            return _RotationTf.Angle;
        }
        set
        {
            _RotationTf.Angle = value;
        }
    }

    public FixedPointF MoveSpeed
    {
        get
        {
            return _MoveSpeed;
        }
    }

    public CustomTransform PositionTf
    {
        get
        {
            return _PositionTf;
        }
    }

    public CustomTransform RotationTf
    {
        get
        {
            return _RotationTf;
        }
    }
}
