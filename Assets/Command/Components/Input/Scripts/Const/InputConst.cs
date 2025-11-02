using UnityEngine;

namespace MyCommand
{
    /// <summary>
    /// InputActionの名前enum
    /// </summary>
    public enum InputName
    {
        Move,
        Punchi_week,
        Punchi_middle,
        Punchi_strong,
        Kick_week,
        Kick_middle,
        Kick_strong,
    }

    /// <summary>
    /// 方向入力のenum
    /// </summary>
    public enum InputDirection
    {
        Neutral,
        Top,
        Bottom,
        Left,
        Right,
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight
    }

    /// <summary>
    /// 攻撃の種類
    /// </summary>
    public enum InputAttack
    {
        Neutral,
        Punchi_week,
        Punchi_middle,
        Punchi_strong,
        Kick_week,
        Kick_middle,
        Kick_strong,
    }
}
