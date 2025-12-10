using UnityEngine;

namespace My.Command
{
    public static class InputExtension
    {
        /// <summary>
        /// 攻撃ボタンの色の取得
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static Color GetColor(this InputAttack attack, bool isActive)
        {
            return attack switch
            {
                InputAttack.Punch_Weak or InputAttack.Kick_Weak =>
                    isActive ? CommandColors.weekAttackActiveColor : CommandColors.weekAttackEnactiveColor,

                InputAttack.Punch_Middle or InputAttack.Kick_Middle =>
                    isActive ? CommandColors.middleAttackActiveColor : CommandColors.middleAttackEnactiveColor,

                InputAttack.Punch_Strong or InputAttack.Kick_Strong =>
                    isActive ? CommandColors.strongAttackActiveColor : CommandColors.strongAttackEnactiveColor,

                _ => isActive ? CommandColors.directionActiveColor : CommandColors.directionEnactiveColor
            };
        }
    }
}
