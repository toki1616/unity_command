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
                // 通知を飛ばす（内容が変わったことを知らせる）
                _inputFrameHistorySubject.OnNext(new List<InputFrameData>(_inputFrameHistory));
            }
            else
            {
                _inputFrameHistory.Add(currentFrame);

                if (_inputFrameHistory.Count > 30)
                {
                    _inputFrameHistory.RemoveAt(0);
                }

                // 通知を飛ばす（新しい履歴が追加された）
                _inputFrameHistorySubject.OnNext(new List<InputFrameData>(_inputFrameHistory));
            }

            if (_inputFrameHistory.Count > 0)
            {
                Debug.Log($"UpdatePerFrame : {_inputFrameHistory[_inputFrameHistory.Count - 1].ToString()}");
            }
            else
            {
                Debug.Log("UpdatePerFrame : 入力履歴がまだありません");
            }
        }
    }
}
