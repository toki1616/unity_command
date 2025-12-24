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
        /// コマンドの溜めフレーム
        /// </summary>
        public float ChargeFrame { get; private set; }

        /// <summary>
        /// 優先度（数値が大きいほど優先）
        /// </summary>
        public int Priority { get; private set; }

        public CommandPattern(SpecialAttack specialAttack, float graceFrame, List<InputDirection> directions, float chargeFrame, int priority)
        {
            SpecialAttack = specialAttack;
            GraceFrame = graceFrame;
            Directions = directions;
            ChargeFrame = chargeFrame;
            Priority = priority;
        }

        public bool IsMatch(List<InputFrameData> inputHistory)
        {
            if (inputHistory.Count < Directions.Count) return false;

            var graceList = GetGraceFrameHistory(inputHistory, GraceFrame);

            // 方向パターンが一致しているか
            bool directionMatch = CheckDirectionPattern(graceList);
            if (!directionMatch) return false;

            // チャージが必要なら判定
            bool chargeMatch = CheckCharge(graceList);
            return CheckCharge(graceList);
        }

        /// <summary>
        /// 最新から猶予フレーム分の入力の取得
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <param name="graceFrame"></param>
        /// <returns></returns>
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

        /// <summary>
        /// コマンドが成立しているか判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckDirectionPattern(List<InputFrameData> inputHistory)
        {
            //Neutralを省く
            var filteredHistory = inputHistory
                .Where(f => f.Direction != InputDirection.Neutral)
                .ToList();

            if (filteredHistory.Count < Directions.Count) return false;

            //
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

        /// <summary>
        /// 初めのDirectionの方向がChargeFrame分入力されているか判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckCharge(List<InputFrameData> inputHistory)
        {
            if (ChargeFrame <= 1) return true;

            for (int i = inputHistory.Count - 1; i >= 0; i--)
            {
                var frame = inputHistory[i];

                if (frame.Direction != Directions[0]) continue;
                if (frame.holdFrame >= ChargeFrame) return true;
            }

            return false;
        }
    }
}
