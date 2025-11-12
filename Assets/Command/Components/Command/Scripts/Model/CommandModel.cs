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
                if (EndsWith(inputHistory, pattern.Directions))
                {
                    // 攻撃履歴をまとめる
                    var allAttacks = inputHistory.SelectMany(h => h.Attacks).ToList();

                    if (pattern.AttackCondition(allAttacks))
                    {
                        Debug.Log($"コマンド成立！ : {pattern.Name}");
                    }
                }
            }
        }

        /// <summary>
        /// 入力した方向の履歴の末尾がパターンと一致しているか判定
        /// </summary>
        private bool EndsWith(List<InputFrameData> inputHistory, List<InputDirection> pattern)
        {
            if (inputHistory.Count < pattern.Count) return false;

            for (int i = 0; i < pattern.Count; i++)
            {
                // InputFrameData の Direction を比較
                if (inputHistory[inputHistory.Count - pattern.Count + i].Direction != pattern[i])
                    return false;
            }
            return true;
        }
    }
}
