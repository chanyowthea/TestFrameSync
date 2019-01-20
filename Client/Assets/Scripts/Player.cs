using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMove
{
    void Move(Vector3 motion);
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

    public void Move(Vector3 motion)
    {
        ChangeDir(new Vector2(motion.x, motion.z).normalized);
        Move();
    }

    // 移动罗盘只控制方向，不控制速度
    public virtual void Move()
    {
        CustomVector3 temp;

        FixedPointF x = CustomMath.GetCos(_RotationTf.Angle);
        FixedPointF z = CustomMath.GetSin(_RotationTf.Angle);

        temp.x = x * _MoveSpeed * ConstValue._RenderFrameRate;
        temp.y = new FixedPointF(0);
        temp.z = z * _MoveSpeed * ConstValue._RenderFrameRate;

        _PositionTf.LocalPosition += temp;
    }

    void ChangeDir(Vector2 tVec2)
    {
        //发送遥感角度
        if (tVec2.x != 0)
        {
            int angle = (int)(Mathf.Atan2(tVec2.y, tVec2.x) * 180 / 3.14f);
            if (Mathf.Abs(_RotationTf.Angle - angle) > 5)
            {
                _RotationTf.Angle = angle;
            }
        }
        else
        {
            int angle = tVec2.y > 0 ? 90 : -90;
            if (Mathf.Abs(_RotationTf.Angle - angle) > 5)
            {
                _RotationTf.Angle = angle;
            }
        }
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
    public void Move(Vector3 motion)
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
    IPlayerAttack _IPlayerAttack;
    IPlayerMove _IPlayerMove;
    FixedPointF _MoveSpeed = new FixedPointF(1);
    [SerializeField] CustomTransform _PositionTf;
    [SerializeField] CustomTransform _RotationTf;

    public void SetData(IPlayerAttack playerAttack, IPlayerMove playerMove)
    {
        _IPlayerAttack = playerAttack;
        _IPlayerMove = playerMove;
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

    public void Move(Vector3 motion)
    {
        _IPlayerMove.Move(motion);
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
