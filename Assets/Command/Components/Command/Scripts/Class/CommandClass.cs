using System;
using System.Collections.Generic;
using UnityEngine;

namespace My.Command
{
    public class CommandPattern
    {
        public string Name;
        //猶予フレーム
        public float GraceFrame;
        public List<InputDirection> Directions;
        public Func<List<InputAttack>, bool> AttackCondition;

        public CommandPattern(string name, float graceFrame, List<InputDirection> directions, Func<List<InputAttack>, bool> attackCondition)
        {
            Name = name;
            GraceFrame = graceFrame;
            Directions = directions;
            AttackCondition = attackCondition;
        }
    }
}
