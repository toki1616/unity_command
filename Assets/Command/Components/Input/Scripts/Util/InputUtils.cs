using UnityEngine;

namespace My.Command
{
    /// <summary>
    /// 簡易入力用のHelper
    /// </summary>
    public static class DirectionHelper
    {
        public static bool IsDirectionMatch(InputDirection input, InputDirection required)
        {
            if (required == InputDirection.Top)
                return IsUp(input);

            if (required == InputDirection.Bottom)
                return IsDown(input);

            if (required == InputDirection.Right)
                return IsForward(input);

            if (required == InputDirection.LowerRight)
                return IsDownForward(input);

            if (required == InputDirection.Left)
                return IsBack(input);

            if (required == InputDirection.LowerLeft)
                return IsDownBack(input);

            return input == required;
        }

        private static bool IsUp(InputDirection dir)
        {
            return dir == InputDirection.Top || dir == InputDirection.UpperLeft || dir == InputDirection.UpperRight;
        }

        private static bool IsDown(InputDirection dir)
        {
            return dir == InputDirection.Bottom || dir == InputDirection.LowerLeft || dir == InputDirection.LowerRight;
        }

        private static bool IsForward(InputDirection dir)
        {
            return dir == InputDirection.Right || dir == InputDirection.LowerRight || dir == InputDirection.UpperRight;
        }

        private static bool IsDownForward(InputDirection dir)
        {
            return dir == InputDirection.Right || dir == InputDirection.LowerRight || dir == InputDirection.Bottom;
        }

        private static bool IsBack(InputDirection dir)
        {
            return dir == InputDirection.Left || dir == InputDirection.LowerLeft || dir == InputDirection.UpperLeft;
        }

        private static bool IsDownBack(InputDirection dir)
        {
            return dir == InputDirection.Left || dir == InputDirection.LowerLeft || dir == InputDirection.Bottom;
        }
    }
}
