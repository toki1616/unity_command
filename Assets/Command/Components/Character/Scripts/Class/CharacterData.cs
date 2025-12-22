using System.Collections.Generic;
using UnityEngine;

namespace My.Command
{
    public class CharacterData
    {
        public CharacterType Character { get; private set; }
        public PlayerPosture Posture { get; private set; }
        public List<CommandPattern> CommandPatterns { get; private set; }

        public CharacterData(CharacterType character, PlayerPosture playerPosture, List<CommandPattern> commandPatterns)
        {
            Character = character;
            Posture = playerPosture;
            CommandPatterns = commandPatterns;
        }
    }
}
