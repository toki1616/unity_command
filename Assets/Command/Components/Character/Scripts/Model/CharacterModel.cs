using System.Collections.Generic;
using UnityEngine;
using Zenject;
using R3;

namespace My.Command
{
    public class CharacterModel
    {
        public IReadOnlyList<CharacterData> Characters => _character;
        private static List<CharacterData> _character = new List<CharacterData>
        {
            new CharacterData(
                CharacterType.Ryu,
                PlayerPosture.Standing,
                new List<CommandPattern>
                {
                    new CommandPattern(
                        SpecialAttack.Syoryu,
                        CommandConst.syoryuGraceFrame,
                        new List<InputDirection> { InputDirection.Right, InputDirection.Bottom, InputDirection.LowerRight },
                        1f,
                        priority: 1 // 昇竜拳を最優先
                    ),
                    new CommandPattern(
                        SpecialAttack.Hadou,
                        CommandConst.hadouGraceFrame,
                        new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerRight, InputDirection.Right },
                        1f,
                        priority: 2
                    ),
                    new CommandPattern(
                        SpecialAttack.Tatsumaki,
                        CommandConst.tatsumakiGraceFrame,
                        new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerLeft, InputDirection.Left },
                        1f,
                        priority: 3
                    ),
                }
            ),
            new CharacterData(
                CharacterType.Chunli,
                PlayerPosture.Standing,
                new List<CommandPattern>
                {
                    new CommandPattern(
                        SpecialAttack.Syoryu,
                        CommandConst.syoryuGraceFrame,
                        new List<InputDirection> { InputDirection.Bottom, InputDirection.Bottom },
                        1f,
                        priority: 1 // 昇竜拳を最優先
                    ),
                    new CommandPattern(
                        SpecialAttack.Hadou,
                        CommandConst.hadouGraceFrame,
                        new List<InputDirection> { InputDirection.Left, InputDirection.Right },
                        45f,
                        priority: 2
                    ),
                    new CommandPattern(
                        SpecialAttack.Tatsumaki,
                        CommandConst.tatsumakiGraceFrame,
                        new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerLeft, InputDirection.Left },
                        1f,
                        priority: 3
                    ),
                }
            ),
            new CharacterData(
                CharacterType.Guile,
                PlayerPosture.Standing,
                new List<CommandPattern>
                {
                    new CommandPattern(
                        SpecialAttack.Syoryu,
                        CommandConst.syoryuGraceFrame,
                        new List<InputDirection> { InputDirection.Bottom, InputDirection.Top },
                        45f,
                        priority: 1 // 昇竜拳を最優先
                    ),
                    new CommandPattern(
                        SpecialAttack.Hadou,
                        CommandConst.hadouGraceFrame,
                        new List<InputDirection> { InputDirection.Left, InputDirection.Right },
                        45f,
                        priority: 2
                    ),
                    new CommandPattern(
                        SpecialAttack.Tatsumaki,
                        CommandConst.tatsumakiGraceFrame,
                        new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerLeft, InputDirection.Left },
                        1f,
                        priority: 3
                    ),
                }
            ),
        };

        private static readonly Dictionary<CharacterType, CharacterData> _characterDict = new Dictionary<CharacterType, CharacterData>
        {
            { CharacterType.Ryu, _character[0] },
            { CharacterType.Chunli, _character[1] },
            { CharacterType.Guile, _character[2] },
        };


        private readonly ReactiveProperty<CharacterData> _characterRP = new ReactiveProperty<CharacterData>(_characterDict[CharacterType.Ryu]);
        public ReadOnlyReactiveProperty<CharacterData> CharacterRP => _characterRP;

        public void UpdateCharacter(CharacterType character)
        {
            _characterRP.Value = _characterDict[character];
        }
    }
}
