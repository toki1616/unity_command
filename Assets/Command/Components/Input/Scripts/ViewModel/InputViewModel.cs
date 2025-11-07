using UnityEngine;
using Zenject;
using R3;
using ObservableCollections;
using System.Collections.Generic;
using System.Linq;

namespace My.Command
{
    public class InputViewModel
    {
        private InputModel _inputModel;

        [Inject]
        public InputViewModel
            (
                InputModel inputModel
            )
        {
            _inputModel = inputModel;
        }

        public Observable<InputDirection> InputDirectionObservable => 
            _inputModel.InputDirectionRP
            .AsObservable()
            .DistinctUntilChanged();

        public void OnVector2Action(string actionName, Vector2 value)
        {
            _inputModel.HandleVector2(actionName, value);
        }

        public void OnFloatAction(string actionName, float value)
        {
            _inputModel.HandleFloat(actionName, value);
        }

        /// <summary>
        /// 攻撃入力の押した通知
        /// </summary>
        public Observable<InputAttack> PressedAttacksObservable =>
            _inputModel.PressedAttacksObservableList
                .ObserveAdd()
                .Select(e => e.Value);

        /// <summary>
        /// 攻撃入力の離した通知
        /// </summary>
        public Observable<InputAttack> ReleasedAttacksObservable =>
            _inputModel.PressedAttacksObservableList
                .ObserveRemove()
                .Select(e => e.Value);

        public void OnButtonPressed(string actionName)
        {
            _inputModel.HandleButtonPressed(actionName);
        }

        public void OnButtonReleased(string actionName)
        {
            _inputModel.HandleButtonReleased(actionName);
        }

        public Observable<List<InputFrameData>> InputFrameHistoryListAsObservable =>
            _inputModel.InputFrameHistoryObservable
            .Publish();
    }
}
