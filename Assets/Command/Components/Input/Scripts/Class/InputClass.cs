using System.Collections.Generic;
using UnityEngine;

namespace My.Command
{
    public class InputFrameData
    {
        /// <summary>
        /// 入力した方向
        /// </summary>
        public InputDirection Direction { get; private set; }
        
        /// <summary>
        /// 入力した攻撃の履歴表示用
        /// </summary>
        public List<InputAttack> Attacks { get; private set; }

        /// <summary>
        /// 入力した攻撃の押した瞬間のもの
        /// </summary>
        public List<InputAttack> NewlyPressedAttacks { get; private set; }

        /// <summary>
        /// 押しているフレーム数
        /// </summary>
        public float holdFrame { get; private set; }

        public InputFrameData(InputDirection direction, List<InputAttack> attacks, List<InputAttack> newlyPressed)
        {
            Direction = direction;
            Attacks = attacks;
            NewlyPressedAttacks = newlyPressed;
            holdFrame++;
        }

        public void AddHold()
        {
            holdFrame++;
            NewlyPressedAttacks.Clear();
        }

        public override string ToString()
        {
            string attackStr = string.Join("+", Attacks);
            string newlyAttackStr = string.Join("+", NewlyPressedAttacks);
            return $"Direction : {Direction} : attackList : {attackStr} : newlyAttackList : {newlyAttackStr} : holdFrame : {holdFrame}";
        }

        public bool IsSameAs(InputFrameData other)
        {
            if (other == null) return false;
            if (Direction != other.Direction) return false;
            if (Attacks.Count != other.Attacks.Count) return false;

            foreach (var attack in Attacks)
            {
                if (!other.Attacks.Contains(attack))
                    return false;
            }

            return true;
        }

        public string GetFrameString()
        {
            return holdFrame.ToString();
        }
    }
}
