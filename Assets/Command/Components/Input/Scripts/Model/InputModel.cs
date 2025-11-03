using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using R3;
using ObservableCollections;

namespace MyCommand
{
    public class InputModel
    {
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

        private readonly ObservableList<InputAttack> _pressedAttacksRC = new ObservableList<InputAttack>();
        public IReadOnlyObservableList<InputAttack> PressedAttacksRC => _pressedAttacksRC;


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

                if (!_pressedAttacksRC.Contains(attack))
                {
                    _pressedAttacksRC.Add(attack);
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
                if (_pressedAttacksRC.Contains(attack))
                {
                    _pressedAttacksRC.Remove(attack);
                }
            }
        }
    }
}
