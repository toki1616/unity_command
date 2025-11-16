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
                CommandConst.hadouGraceFrame,
                new List<InputDirection>
                {
                    InputDirection.Bottom,
                    InputDirection.LowerRight,
                    InputDirection.Right
                }
            ),
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
            if (inputHistory.Last().NewlyPressedAttacks.Count <= 0) return;

            foreach (var pattern in _commandPatterns)
            {
                if (!pattern.IsMatch(inputHistory)) continue;

                Debug.Log($"コマンド成立！ : {pattern.Name}");
            }
        }

        /// <summary>
        /// 入力履歴の末尾がパターンと一致しているか判定
        /// </summary>
        private bool GetPatternStartIndex(List<InputFrameData> inputHistory, List<InputDirection> pattern)
        {
            if (inputHistory.Count < pattern.Count) return false;

            int startIndex = inputHistory.Count - pattern.Count;

            for (int i = 0; i < pattern.Count; i++)
            {
                if (inputHistory[startIndex + i].Direction != pattern[i])
                    return false;
            }

            return true;
        }
    }
}
