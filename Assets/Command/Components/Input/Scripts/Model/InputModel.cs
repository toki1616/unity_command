using UnityEngine;

namespace MyCommand
{
    public class InputModel
    {
        public void UpdateMove(Vector2 value)
        {
            Debug.Log($"InputModel : {value}");
        }
    }
}
