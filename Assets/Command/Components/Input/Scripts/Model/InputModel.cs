using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyCommand
{
    public class InputModel
    {
        public void HandleVector2(string actionName, Vector2 value)
        {
            Debug.Log($"InputModel : HandleVector2 : name : {actionName} : value : {value}");
        }

        public void HandleButton(string actionName, bool isPressed)
        {
            Debug.Log($"InputModel : HandleButton : name : {actionName} : value : {isPressed}");
        }

        public void HandleFloat(string actionName, float value)
        {
            Debug.Log($"InputModel : HandleFloat : name : {actionName} : value : {value}");
        }

        public void HandleButtonPressed(string actionName)
        {
            Debug.Log($"InputModel ButtonPressed : {actionName}");
        }

        public void HandleButtonReleased(string actionName)
        {
            Debug.Log($"InputModel ButtonReleased : {actionName}");
        }
    }
}
