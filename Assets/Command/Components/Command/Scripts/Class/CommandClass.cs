using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace My.Command
{
    public class CommandPattern
    {
        public SpecialAttack SpecialAttack { get; private set; }

        /// <summary>
        /// 猶予フレーム
        /// </summary>
        public float GraceFrame { get; private set; }

        /// <summary>
        /// コマンドの入力方向
        /// </summary>
        public List<InputDirection> Directions { get; private set; }

        /// <summary>
        /// 優先度（数値が大きいほど優先）
        /// </summary>
        public int Priority { get; private set; }

        public CommandPattern(SpecialAttack specialAttack, float graceFrame, List<InputDirection> directions, int priority)
        {
            SpecialAttack = specialAttack;
            GraceFrame = graceFrame;
            Directions = directions;
            Priority = priority;
        }

        public bool IsMatch(List<InputFrameData> inputHistory)
        {
            if (inputHistory.Count < Directions.Count) return false;
            var graceList = GetGraceFrameHistory(inputHistory, GraceFrame);
            return CheckDirectionPattern(graceList);
        }

        private List<InputFrameData> GetGraceFrameHistory(List<InputFrameData> inputHistory, float graceFrame)
        {
            float accumulatedFrame = 0f;
            var graceList = new List<InputFrameData>();

            for (int i = inputHistory.Count - 1; i >= 0; i--)
            {
                accumulatedFrame += inputHistory[i].holdFrame;
                graceList.Insert(0, inputHistory[i]);

                if (accumulatedFrame > graceFrame)
                    break;
            }
            return graceList;
        }

        private bool CheckDirectionPattern(List<InputFrameData> inputHistory)
        {
            var filteredHistory = inputHistory
                .Where(f => f.Direction != InputDirection.Neutral)
                .ToList();

            if (filteredHistory.Count < Directions.Count) return false;

            for (int startIndex = 0; startIndex <= filteredHistory.Count - Directions.Count; startIndex++)
            {
                bool match = true;
                for (int i = 0; i < Directions.Count; i++)
                {
                    if (filteredHistory[startIndex + i].Direction != Directions[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return true;
            }
            return false;
        }
    }
}
