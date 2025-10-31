using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyCommand
{
    public class InputModel
    {
        public void UpdateMove(Vector2 value)
        {
            Debug.Log($"InputModel : {value}");
        }

        public void OnAnyAction(InputAction.CallbackContext context)
        {
            string actionName = context.action.name;

            if (Enum.TryParse<InputName>(actionName, out var inputEnum))
            {
                //除外するタイプ
                HashSet<InputName> IgnoredActions = new()
                {
                    InputName.Move,
                };

                //IgnoredActionsのTypeならreturn
                if (IgnoredActions.Contains(inputEnum))
                    return;

                Debug.Log($"InputModel : Action : {actionName}");
            }
            else
            {
                Debug.LogWarning($"未定義のアクション名: {actionName}");
            }
        }
    }
}
