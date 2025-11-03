using UnityEngine;

namespace MyCommand
{
    public static class CommandColors
    {
        public static Color directionActiveColor = new Color(1f, 1f, 1f);
        public static Color directionEnactiveColor = new Color(0.8f, 0.8f, 0.8f);

        public static Color weekAttackActiveColor = new Color(1f, 1f, 1f);
        public static Color weekAttackEnactiveColor = new Color(1f, 1f, 1f);

        public static Color middleAttackActiveColor = new Color(1f, 1f, 1f);
        public static Color middleAttackEnactiveColor = new Color(1f, 1f, 1f);

        public static Color strongAttackActiveColor = new Color(1f, 1f, 1f);
        public static Color strongAttackEnactiveColor = new Color(1f, 1f, 1f);
    }

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
