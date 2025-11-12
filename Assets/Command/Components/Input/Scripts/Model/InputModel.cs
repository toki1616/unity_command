using System;
using System.Collections.Generic;
using UnityEngine;
using R3;
using ObservableCollections;

namespace My.Command
{
    public class InputModel
    {
        public InputModel()
        {
            _subscription = Observable.EveryUpdate()
                .Subscribe(_ => UpdatePerFrame());
        }

        private IDisposable _subscription;

        private readonly ReactiveProperty<InputDirection> _inputDirectionRP = new ReactiveProperty<InputDirection>(InputDirection.Neutral);
        public ReadOnlyReactiveProperty<InputDirection> InputDirectionRP => _inputDirectionRP;

        /// <summary>
        /// 方向キーの入力
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="value"></param>
        public void HandleVector2(string actionName, Vector2 value)
        {
            //Debug.Log($"InputModel : HandleVector2 : name : {actionName} : value : {value}");

            _inputDirectionRP.Value = GetInputDirectionFromRawVector(value);
        }

        /// <summary>
        /// 入力キーの値をInputDirectionに変換
        /// </summary>
        /// <param name="inputDiretion"></param>
        /// <returns></returns>
        private InputDirection GetInputDirectionFromRawVector(Vector2 value)
        {
            float x = value.x;
            float y = value.y;

            // 閾値
            float threshold = 0.4f;

            bool isLeft = x < -threshold;
            bool isRight = x > threshold;
            bool isUp = y > threshold;
            bool isDown = y < -threshold;

            if (isUp && isRight) return InputDirection.UpperRight;
            if (isUp && isLeft) return InputDirection.UpperLeft;
            if (isDown && isRight) return InputDirection.LowerRight;
            if (isDown && isLeft) return InputDirection.LowerLeft;
            if (isUp) return InputDirection.Top;
            if (isDown) return InputDirection.Bottom;
            if (isLeft) return InputDirection.Left;
            if (isRight) return InputDirection.Right;

            return InputDirection.Neutral;
        }

        public void HandleFloat(string actionName, float value)
        {
            //Debug.Log($"InputModel : HandleFloat : name : {actionName} : value : {value}");
        }

        private readonly ObservableList<InputAttack> _pressedAttacksObservableList = new ObservableList<InputAttack>();
        public IReadOnlyObservableList<InputAttack> PressedAttacksObservableList => _pressedAttacksObservableList;


        /// <summary>
        /// 押されたボタンの判定
        /// </summary>
        /// <param name="actionName"></param>
        public void HandleButtonPressed(string actionName)
        {
            //Debug.Log($"InputModel ButtonPressed : {actionName}");

            if (Enum.TryParse<InputAttack>(actionName, out var attack))
            {
                //Debug.Log($"一致した攻撃入力: {attack}");

                if (!_pressedAttacksObservableList.Contains(attack))
                {
                    _pressedAttacksObservableList.Add(attack);
                }
            }
        }

        /// <summary>
        /// 離れたボタンの判定
        /// </summary>
        /// <param name="actionName"></param>
        public void HandleButtonReleased(string actionName)
        {
            //Debug.Log($"InputModel ButtonReleased : {actionName}");

            if (Enum.TryParse<InputAttack>(actionName, out var attack))
            {
                if (_pressedAttacksObservableList.Contains(attack))
                {
                    _pressedAttacksObservableList.Remove(attack);
                }
            }
        }

        private Subject<List<InputFrameData>> _inputFrameHistorySubject = new Subject<List<InputFrameData>>();
        /// <summary>
        /// 毎フレームの入力通知
        /// </summary>
        public Observable<List<InputFrameData>> InputFrameHistoryObservable => _inputFrameHistorySubject;

        private readonly List<InputFrameData> _inputFrameHistory = new List<InputFrameData>();

        /// <summary>
        /// 毎フレーム入力保持の処理
        /// </summary>
        public void UpdatePerFrame()
        {
            var currentDirection = _inputDirectionRP.Value;
            var currentAttacks = new List<InputAttack>(_pressedAttacksObservableList);

            var lastFrame = _inputFrameHistory.Count > 0
                ? _inputFrameHistory[_inputFrameHistory.Count - 1]
                : null;

            var currentFrame = new InputFrameData(currentDirection, currentAttacks, Time.time);

            if (lastFrame != null && lastFrame.IsSameAs(currentFrame))
            {
                lastFrame.AddHold();
            }
            else
            {
                _inputFrameHistory.Add(currentFrame);
            }

            // フレーム数ベースで履歴を制限
            var limitedHistory = GetLimitInputHistory(_inputFrameHistory);
            _inputFrameHistory.Clear();
            _inputFrameHistory.AddRange(limitedHistory);

            // 通知を飛ばす（履歴が更新された）
            _inputFrameHistorySubject.OnNext(new List<InputFrameData>(_inputFrameHistory));

            //Debug.Log($"UpdatePerFrame : {_inputFrameHistory.LastOrDefault()?.ToString() ?? "履歴なし"}");
        }

        /// <summary>
        /// 最新の入力から30フレームまでの履歴を取得
        /// </summary>
        /// <param name="inputHistory"></param>
        /// <returns></returns>
        private List<InputFrameData> GetLimitInputHistory(List<InputFrameData> inputHistory)
        {
            float limitFrame = 30f;
            float accumulatedFrame = 0f;
            var reversedLimitedHistory = new List<InputFrameData>();

            // 後ろから前に向かって処理（新しい順）
            for (int i = inputHistory.Count - 1; i >= 0; i--)
            {
                var inputFrameData = inputHistory[i];
                float nextTotal = accumulatedFrame + inputFrameData.holdFrame;

                if (nextTotal <= limitFrame)
                {
                    reversedLimitedHistory.Add(inputFrameData);
                    accumulatedFrame = nextTotal;
                }
                else
                {
                    if (reversedLimitedHistory.Count == 0 || accumulatedFrame < limitFrame)
                    {
                        reversedLimitedHistory.Add(inputFrameData);
                    }
                    break;
                }
            }

            // 時系列順に戻す（古い順に）
            reversedLimitedHistory.Reverse();
            return reversedLimitedHistory;
        }
    }
}
