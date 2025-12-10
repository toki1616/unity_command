using UnityEngine;

namespace My.Command
{
    public static class CommandConst
    {
        public static float syoryuGraceFrame = 11f;
        public static float hadouGraceFrame = 11f;
        public static float tatsumakiGraceFrame = 11f;
    }

    public enum CommandType
    {
        None,

        // 必殺技
        Syoryu,
        Hadou,
        Tatsumaki,

        // 通常技（方向＋攻撃で区別）
        Stand_Punch_Weak,
        Stand_Punch_Middle,
        Stand_Punch_Strong,
        Crouch_Punch_Weak,
        Crouch_Punch_Middle,
        Crouch_Punch_Strong,
        Stand_Kick_Weak,
        Stand_Kick_Middle,
        Stand_Kick_Strong,
        Crouch_Kick_Weak,
        Crouch_Kick_Middle,
        Crouch_Kick_Strong
    }
}
