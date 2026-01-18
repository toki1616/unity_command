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
                        specialAttack: SpecialAttack.SA1,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerRight, InputDirection.Right, InputDirection.Bottom, InputDirection.LowerRight, InputDirection.Right },
                        attackType: AttackType.Punch,
                        chargeFrame: 1f,
                        priority: 1,
                        isMitigation: false
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Syoryu,
                        graceFrame: CommandConst.syoryuGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Right, InputDirection.Bottom, InputDirection.LowerRight },
                        attackType: AttackType.Punch,
                        chargeFrame: 1f,
                        priority: 2,
                        isMitigation: true
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Hadou,
                        graceFrame: CommandConst.hadouGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerRight, InputDirection.Right },
                        attackType: AttackType.Punch,
                        chargeFrame: 1f,
                        priority: 3,
                        isMitigation: false
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Tatsumaki,
                        graceFrame: CommandConst.tatsumakiGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerLeft, InputDirection.Left },
                        attackType: AttackType.Kick,
                        chargeFrame: 1f,
                        priority: 4,
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
                        specialAttack: SpecialAttack.SA1,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerRight, InputDirection.Right, InputDirection.Bottom, InputDirection.LowerRight, InputDirection.Right },
                        attackType: AttackType.Punch,
                        chargeFrame: 1f,
                        priority: 1,
                        isMitigation: false
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Syoryu,
                        graceFrame: CommandConst.syoryuGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.Bottom },
                        attackType: AttackType.Kick,
                        chargeFrame: 1f,
                        priority: 2,
                        isMitigation: false
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Hadou,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Left, InputDirection.Right },
                        attackType: AttackType.Punch,
                        chargeFrame: CommandConst.chargeFrame,
                        priority: 3,
                        isMitigation: true
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Tatsumaki,
                        graceFrame: CommandConst.tatsumakiGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.LowerLeft, InputDirection.Left },
                        attackType: AttackType.Kick,
                        chargeFrame: 1f,
                        priority: 4,
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
                        specialAttack: SpecialAttack.SA1,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Left, InputDirection.Right, InputDirection.Left, InputDirection.Right },
                        attackType: AttackType.Punch,
                        chargeFrame: CommandConst.chargeFrame,
                        priority: 1,
                        isMitigation: true
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.SA2,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.Top, InputDirection.Bottom, InputDirection.Top },
                        attackType: AttackType.Punch,
                        chargeFrame: CommandConst.chargeFrame,
                        priority: 2,
                        isMitigation: true
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Syoryu,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Bottom, InputDirection.Top },
                        attackType: AttackType.Kick,
                        chargeFrame: CommandConst.chargeFrame,
                        priority: 3,
                        isMitigation: false
                    ),
                    new CommandPattern(
                        specialAttack: SpecialAttack.Hadou,
                        graceFrame: CommandConst.chargeGraceFrame,
                        directions: new List<InputDirection> { InputDirection.Left, InputDirection.Right },
                        attackType: AttackType.Punch,
                        chargeFrame: CommandConst.chargeFrame,
                        priority: 4,
                        isMitigation: true
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
