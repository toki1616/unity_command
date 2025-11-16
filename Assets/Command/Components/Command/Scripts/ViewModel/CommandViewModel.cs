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

        [Inject]
        public CommandViewModel
            (
                CommandModel commandModel,
                InputViewModel inputViewModel
            )
        {
            //Debug.Log("CommandViewModel : Inject");
            _commandModel = commandModel;
            _inputViewModel = inputViewModel;
        }

        private IDisposable _disposable;

        public void Initialize()
        {
            _disposable = _inputViewModel.InputFrameHistoryListAsObservable
                .Subscribe(inputHistory =>
                {
                    ReceiveInput(inputHistory);
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
    }
}
