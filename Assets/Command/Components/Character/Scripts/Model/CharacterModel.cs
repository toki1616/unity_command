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
                        specialAttack: SpecialAttack.Syoryu,
                        graceFrame: CommandConst.syoryuGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Right, InputDirection.Bottom, InputDirection.LowerRight },
                        chargeFrame: 1f,
                        priority: 1,
                        isMitigation: true
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Hadou,
                        graceFrame: CommandConst.hadouGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerRight, InputDirection.Right },
                        chargeFrame: 1f,
                        priority: 2,
                        isMitigation: false
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Tatsumaki,
                        graceFrame: CommandConst.tatsumakiGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerLeft, InputDirection.Left },
                        chargeFrame: 1f,
                        priority: 3,
                        isMitigation: false
                    ),
                }
            ),
            new CharacterData(
                CharacterType.Chunli,
                PlayerPosture.Standing,
                new List<CommandPattern>
                {
                    new CommandPattern(
                        specialAttack: SpecialAttack.Syoryu,
                        graceFrame: CommandConst.syoryuGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.Bottom },
                        chargeFrame: 1f,
                        priority: 1,
                        isMitigation: false
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Hadou,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Left, InputDirection.Right },
                        chargeFrame: CommandConst.chargeFrame,
                        priority: 2,
                        isMitigation: true
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Tatsumaki,
                        graceFrame: CommandConst.tatsumakiGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerLeft, InputDirection.Left },
                        chargeFrame: 1f,
                        priority: 3,
                        isMitigation: false
                    ),
                }
            ),
            new CharacterData(
                CharacterType.Guile,
                PlayerPosture.Standing,
                new List<CommandPattern>
                {
                    new CommandPattern(
                        specialAttack: SpecialAttack.Syoryu,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.Top },
                        chargeFrame: CommandConst.chargeFrame,
                        priority: 1,
                        isMitigation: false
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Hadou,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Left, InputDirection.Right },
                        chargeFrame: CommandConst.chargeFrame,
                        priority: 2,
                        isMitigation: true
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Tatsumaki,
                        graceFrame: CommandConst.tatsumakiGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerLeft, InputDirection.Left },
                        chargeFrame: 1f,
                        priority: 3,
                        isMitigation: false
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
