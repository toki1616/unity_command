using System.Collections.Generic;
using UnityEngine;

namespace MyCommand
{
    public class InputFrameData
    {
        public InputDirection Direction { get; private set; }
        public List<InputAttack> Attacks { get; private set; }
        public float holdFrame { get; private set; }

        public InputFrameData(InputDirection direction, List<InputAttack> attacks, float timestamp)
        {
            Direction = direction;
            Attacks = attacks;
            AddHold();
        }

        public void AddHold()
        {
            holdFrame++;
        }

        public override string ToString()
        {
            string attackStr = string.Join("+", Attacks);
            return $"{Direction} : {attackStr} : {holdFrame}";
        }

        public bool IsSameAs(InputFrameData other)
        {
            if (other == null) return false;
            if (Direction != other.Direction) return false;
            if (Attacks.Count != other.Attacks.Count) return false;

            foreach (var attack in Attacks)
            {
                if (!other.Attacks.Contains(attack))
                    return false;
            }

            return true;
        }
    }
}
