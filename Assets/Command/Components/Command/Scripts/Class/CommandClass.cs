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
        /// コマンドの成立するAttackButtonType
        /// </summary>
        public AttackType AttackType { get; private set; }

        /// <summary>
        /// コマンドの溜めフレーム
        /// </summary>
        public float ChargeFrame { get; private set; }

        /// <summary>
        /// 優先度（数値が大きいほど優先）
        /// </summary>
        public int Priority { get; private set; }
        
        public bool IsMitigation { get; private set; }

        public CommandPattern(SpecialAttack specialAttack, float graceFrame, List<InputDirection> directions, AttackType attackType, float chargeFrame, int priority, bool isMitigation)
        {
            SpecialAttack = specialAttack;
            GraceFrame = graceFrame;
            Directions = directions;
            AttackType = attackType;
            ChargeFrame = chargeFrame;
            Priority = priority;
            IsMitigation = isMitigation;
        }

        public bool IsMatch(List<InputFrameData> inputHistory)
        {
            if (inputHistory.Count < Directions.Count) return false;

            var graceList = GetGraceFrameHistory(inputHistory, GraceFrame);

            //AttckTypeが一致しているか
            bool isMatchAttackType = IsMatchAttackType(graceList);
            //Debug.Log($"command : attackTypeMatch : {isMatchAttackType}");
            if (!isMatchAttackType) return false;

            //方向パターンが一致しているか
            bool directionMatch = CheckDirectionPattern(graceList);
            //Debug.Log($"command : directionMatch : {directionMatch}");
            if (!directionMatch) return false;

            //チャージが必要なら判定
            bool chargeMatch = isChargeSuccess(graceList);
            //Debug.Log($"command : chargeMatch : {chargeMatch}");
            return chargeMatch;
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

        private bool IsMatchAttackType(List<InputFrameData> inputHistory)
        {
            var lastFrame = inputHistory.Last();
            if (lastFrame.NewlyPressedAttacks.Count <= 0) return false;

            foreach (var inputAttack in lastFrame.NewlyPressedAttacks)
            {
                if (AttackType == inputAttack.ToAttackType())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// コマンドの方向の判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckDirectionPattern(List<InputFrameData> inputHistory)
        {
            // 溜め技
            if (ChargeFrame > 1) 
            {
                if (IsMitigation)
                    return CheckMitigationChargeDirection(inputHistory);
                    
                return CheckChargeDirection(inputHistory);
            }

            if (IsMitigation)
                return CheckMitigationNormalDirectionPattern(inputHistory);
                
            return CheckNormalDirectionPattern(inputHistory);
        }

        /// <summary>
        /// コマンドが成立しているか判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckNormalDirectionPattern(List<InputFrameData> inputHistory)
        {
            //Neutralを省く
            var filteredHistory = inputHistory
                .Where(f => f.Direction != InputDirection.Neutral)
                .ToList();

            if (filteredHistory.Count < Directions.Count) 
                return false;

            //緩和判定を使って順番マッチ
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

                if (match) 
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 緩和コマンドが成立しているか判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckMitigationNormalDirectionPattern(List<InputFrameData> inputHistory)
        {
            //Neutralを省く
            var filteredHistory = inputHistory
                .Where(f => f.Direction != InputDirection.Neutral)
                .ToList();

            if (filteredHistory.Count < Directions.Count) 
                return false;

            //緩和判定を使って順番マッチ
            for (int startIndex = 0; startIndex <= filteredHistory.Count - Directions.Count; startIndex++)
            {
                bool match = true;

                for (int i = 0; i < Directions.Count; i++)
                {
                    var inputDir = filteredHistory[startIndex + i].Direction;
                    var requiredDir = Directions[i];

                    if (!DirectionHelper.IsDirectionMatch(inputDir, requiredDir))
                    {
                        match = false;
                        break;
                    }
                }

                if (match) 
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 溜め技の入力判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckChargeDirection(List<InputFrameData> inputHistory)
        {
            int index = 0;

            foreach (var frame in inputHistory)
            {
                if (frame.Direction == Directions[index])
                {
                    index++;

                    // 全ての方向が一致したら成功
                    if (index >= Directions.Count)
                        return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// 溜め技の簡易入力判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckMitigationChargeDirection(List<InputFrameData> inputHistory)
        {
            int index = 0;

            foreach (var frame in inputHistory)
            {
                if (DirectionHelper.IsDirectionMatch(frame.Direction, Directions[index]))
                {
                    index++;

                    if (index >= Directions.Count)
                        return true;
                }
            }

            return false;

        }

        private bool isChargeSuccess(List<InputFrameData> inputHistory)
        {
            if (IsMitigation)
                return CheckMitigationCharge(inputHistory);
                
            return CheckCharge(inputHistory);
        }

        /// <summary>
        /// 初めのDirectionの方向がChargeFrame分入力されているか判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckCharge(List<InputFrameData> inputHistory)
        {
            if (ChargeFrame <= 1)
                return true;

            float totalCharge = 0f;
            for (int i = inputHistory.Count - 1; i >= 0; i--)
            {
                var frame = inputHistory[i];

                if (frame.Direction != Directions[0])
                    continue;

                totalCharge += frame.holdFrame;
                if (totalCharge >= ChargeFrame)
                    return true;
            }

            return false;
        }
        
        /// <summary>
        /// 溜めの簡易判定
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private bool CheckMitigationCharge(List<InputFrameData> inputHistory)
        {
            if (ChargeFrame <= 1)
                return true;

            float totalCharge = 0f;
            for (int i = inputHistory.Count - 1; i >= 0; i--)
            {
                var frame = inputHistory[i];

                if (!DirectionHelper.IsDirectionMatch(frame.Direction, Directions[0]))
                    continue;

                totalCharge += frame.holdFrame;
                if (totalCharge >= ChargeFrame)
                    return true;
            }

            return false;
        }
    }
}
