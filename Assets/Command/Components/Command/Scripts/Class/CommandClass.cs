using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace My.Command
{
    public class CommandPattern
    {
        public string Name;
        //猶予フレーム
        public float GraceFrame;
        public List<InputDirection> Directions;

        public CommandPattern(string name, float graceFrame, List<InputDirection> directions)
        {
            Name = name;
            GraceFrame = graceFrame;
            Directions = directions;
        }

        /// <summary>
        /// 入力履歴に一致しているか判定
        /// </summary>
        public bool IsMatch(List<InputFrameData> inputHistory)
        {
            if (inputHistory.Count < Directions.Count) return false;

            // GraceFrame分だけ遡った履歴を抽出
            var graceList = GetGraceFrameHistory(inputHistory, GraceFrame);

            // その範囲の中でコマンドが成立していたらtrue
            return CheckDirectionPattern(graceList);
        }

        /// <summary>
        /// GraceFrame分だけ過去に遡った履歴を返す
        /// </summary>
        private List<InputFrameData> GetGraceFrameHistory(List<InputFrameData> inputHistory, float graceFrame)
        {
            float accumulatedFrame = 0f;
            var graceList = new List<InputFrameData>();

            // 最新から過去へ遡る
            for (int i = inputHistory.Count - 1; i >= 0; i--)
            {
                accumulatedFrame += inputHistory[i].holdFrame;
                graceList.Insert(0, inputHistory[i]); // 古い順に並べる

                //Debug.Log($"GetGraceFrameHistory : accumulatedFrame : {accumulatedFrame} : {inputHistory[i]}");

                if (accumulatedFrame > graceFrame)
                    break;
            }

            return graceList;
        }


        /// <summary>
        /// 入力履歴の末尾がパターンと一致しているか判定
        /// </summary>
        private bool CheckDirectionPattern(List<InputFrameData> inputHistory)
        {
            if (inputHistory.Count < Directions.Count) return false;

            // graceList の中にパターンが含まれていればOK
            for (int startIndex = 0; startIndex <= inputHistory.Count - Directions.Count; startIndex++)
            {
                bool match = true;
                for (int i = 0; i < Directions.Count; i++)
                {
                    if (inputHistory[startIndex + i].Direction != Directions[i])
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
