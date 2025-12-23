using UnityEngine;
using Zenject;
using R3;
using System;
using System.Collections.Generic;

namespace My.Command
{
    public class CommandViewModel : IInitializable, IDisposable
    {
        private CommandModel _commandModel;
        private InputViewModel _inputViewModel;
        private CharacterViewModel _characterViewModel;

        [Inject]
        public CommandViewModel
            (
                CommandModel commandModel,
                InputViewModel inputViewModel,
                CharacterViewModel characterViewModel
            )
        {
            //Debug.Log("CommandViewModel : Inject");
            _commandModel = commandModel;
            _inputViewModel = inputViewModel;
            _characterViewModel = characterViewModel;
        }

        private IDisposable _disposable;

        public void Initialize()
        {
            _disposable = _inputViewModel.InputFrameHistoryListAsObservable
                .Subscribe(inputHistory =>
                {
                    ReceiveInput(inputHistory);
                });

            _disposable = _characterViewModel.CharacterObservable
                .Subscribe(character =>
                {
                    UpdateCommand(character.CommandPatterns);
                });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void ReceiveInput(List<InputFrameData> inputHistory)
        {
            _commandModel.ReceiveInput(inputHistory);
        }

        public void UpdateCommand(List<CommandPattern> commandPatterns)
        {
            _commandModel.UpdateCommand(commandPatterns);
        }
    }
}
