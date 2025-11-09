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
                InputAttack.Punchi_week or InputAttack.Kick_week =>
                    isActive ? CommandColors.weekAttackActiveColor : CommandColors.weekAttackEnactiveColor,

                InputAttack.Punchi_middle or InputAttack.Kick_middle =>
                    isActive ? CommandColors.middleAttackActiveColor : CommandColors.middleAttackEnactiveColor,

                InputAttack.Punchi_strong or InputAttack.Kick_strong =>
                    isActive ? CommandColors.strongAttackActiveColor : CommandColors.strongAttackEnactiveColor,

                _ => isActive ? CommandColors.directionActiveColor : CommandColors.directionEnactiveColor
            };
        }
    }
}
