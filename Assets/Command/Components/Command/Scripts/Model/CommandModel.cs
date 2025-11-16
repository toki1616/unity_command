using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace My.Command
{
    public class CommandModel
    {
        private List<CommandPattern> _commandPatterns = new List<CommandPattern>
        {
            new CommandPattern(
                "hadou_p",
                11f,
                new List<InputDirection>
                {
                    InputDirection.Bottom,
                    InputDirection.LowerRight,
                    InputDirection.Right
                },
                attacks => attacks.Count(atk => atk.ToString().Contains("Punchi")) == 1
            ),

            // パンチが2つ以上でOK
            new CommandPattern(
                "hadou_p_OD_strong",
                11f,
                new List<InputDirection>
                {
                    InputDirection.Bottom,
                    InputDirection.LowerRight,
                    InputDirection.Right
                },
                attacks => attacks.Count(atk => atk.ToString().Contains("Punchi")) >= 2
            )
        };


        public void ReceiveInput(List<InputFrameData> history)
        {
            CheckCommands(history);
        }

        /// <summary>
        /// コマンド判定
        /// </summary>
        private void CheckCommands(List<InputFrameData> inputHistory)
        {
            foreach (var pattern in _commandPatterns)
            {
                int matchStartIndex = GetPatternStartIndex(inputHistory, pattern.Directions);

                if (matchStartIndex >= 0)
                {
                    // パターンに一致する方向入力 + それ以降の攻撃入力を含めた範囲を取得
                    var remainingHistory = inputHistory.Skip(matchStartIndex).ToList();

                    float totalHoldFrame = 0f;
                    var attackRange = new List<InputAttack>();

                    foreach (var frame in remainingHistory)
                    {
                        totalHoldFrame += frame.holdFrame;
                        attackRange.AddRange(frame.Attacks);

                        // 攻撃条件を満たした時点で判定
                        if (pattern.AttackCondition(attackRange))
                        {
                            if (totalHoldFrame <= pattern.GraceFrame)
                            {
                                Debug.Log($"コマンド成立！ : {pattern.Name}");
                            }
                            break; // 攻撃条件を満たしたらそれ以上は不要
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 入力履歴の末尾がパターンと一致しているか判定し、開始インデックスを返す（なければ -1）
        /// </summary>
        private int GetPatternStartIndex(List<InputFrameData> inputHistory, List<InputDirection> pattern)
        {
            if (inputHistory.Count < pattern.Count) return -1;

            int startIndex = inputHistory.Count - pattern.Count;

            for (int i = 0; i < pattern.Count; i++)
            {
                if (inputHistory[startIndex + i].Direction != pattern[i])
                    return -1;
            }

            return startIndex;
        }
    }
}
