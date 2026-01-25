using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using R3;

namespace My.Command
{
    public class CommandModel
    {
        private List<CommandPattern> _commandPatterns = new List<CommandPattern>();

        public void UpdateCommand(List<CommandPattern> commandPatterns)
        {
            _commandPatterns = commandPatterns;
        }

        public void ReceiveInput(List<InputFrameData> history)
        {
            CheckCommands(history);
        }

        // --- 外部へ通知するSubject ---
        private readonly Subject<SpecialAttack> _onSpecialAttack = new Subject<SpecialAttack>();
        private readonly Subject<NormalAttack> _onNormalAttack = new Subject<NormalAttack>();

        public Observable<SpecialAttack> SpecialAttackObservable => _onSpecialAttack;
        public Observable<NormalAttack> NormalAttackObservable => _onNormalAttack;

        /// <summary>
        /// コマンド判定
        /// </summary>
        private void CheckCommands(List<InputFrameData> inputHistory)
        {
            var lastFrame = inputHistory.Last();
            if (lastFrame.NewlyPressedAttacks.Count <= 0) return;

            bool commandFound = false;

            // 優先度の高い順に並べ替えて判定
            foreach (var pattern in _commandPatterns.OrderBy(p => p.Priority))
            {
                if (!pattern.IsMatch(inputHistory)) continue;

                //Debug.Log($"必殺技成立！ : {pattern.SpecialAttack}");
                _onSpecialAttack.OnNext(pattern.SpecialAttack);
                commandFound = true;
                break;
            }

            if (!commandFound)
            {
                foreach (var attack in lastFrame.NewlyPressedAttacks)
                {
                    NormalAttack normalAttack = ConvertToNormalAttack(lastFrame.Direction, attack);
                    //Debug.Log($"通常技成立！ : {normalAttack}");
                    _onNormalAttack.OnNext(normalAttack);
                }
            }
        }

        /// <summary>
        /// 攻撃ボタン＋方向から通常技を判定
        /// </summary>
        private NormalAttack ConvertToNormalAttack(InputDirection direction, InputAttack attack)
        {
            switch (attack)
            {
                case InputAttack.Punch_Weak:
                    return direction == InputDirection.Bottom
                        ? NormalAttack.Crouch_Punch_Weak
                        : NormalAttack.Stand_Punch_Weak;

                case InputAttack.Punch_Middle:
                    return direction == InputDirection.Bottom
                        ? NormalAttack.Crouch_Punch_Middle
                        : NormalAttack.Stand_Punch_Middle;

                case InputAttack.Punch_Strong:
                    return direction == InputDirection.Bottom
                        ? NormalAttack.Crouch_Punch_Strong
                        : NormalAttack.Stand_Punch_Strong;

                case InputAttack.Kick_Weak:
                    return direction == InputDirection.Bottom
                        ? NormalAttack.Crouch_Kick_Weak
                        : NormalAttack.Stand_Kick_Weak;

                case InputAttack.Kick_Middle:
                    return direction == InputDirection.Bottom
                        ? NormalAttack.Crouch_Kick_Middle
                        : NormalAttack.Stand_Kick_Middle;

                case InputAttack.Kick_Strong:
                    return direction == InputDirection.Bottom
                        ? NormalAttack.Crouch_Kick_Strong
                        : NormalAttack.Stand_Kick_Strong;

                default:
                    return NormalAttack.None;
            }
        }
    }
}
