using System;
using System.Collections.Generic;
using UnityEngine;

namespace My.Command
{
    public class CommandPattern
    {
        public string Name;
        public List<InputDirection> Directions;
        public Func<List<InputAttack>, bool> AttackCondition;

        public CommandPattern(string name, List<InputDirection> directions, Func<List<InputAttack>, bool> attackCondition)
        {
            Name = name;
            Directions = directions;
            AttackCondition = attackCondition;
        }
    }
}
