using UnityEngine;

namespace My.Command
{
    /// <summary>
    /// キャラクター
    /// </summary>
    public enum CharacterType
    {
        Ryu,
        Chunli,
        Guile,
    }
    public static class CharacterTypeExtension 
    {
        public static string GetName(this CharacterType characterType)
        {
            switch (characterType)
            {
                case CharacterType.Ryu:
                    return "Ryu";

                case CharacterType.Chunli:
                    return "Chunli";

                case CharacterType.Guile:
                    return "Guile";

                default:
                    return "";
            }
        }
    }

    /// <summary>
    /// キャラクターの姿勢
    /// </summary>
    public enum PlayerPosture
    {
        Standing,
        Crouching,
        Jumping
    }
}
