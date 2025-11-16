using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.Command
{
    public class InputHistoryItemListView : MonoBehaviour
    {
        [SerializeField]
        private Text _frameText;

        [SerializeField]
        private InputHistoryDirectionView _inputHistoryDirectionView;

        [SerializeField]
        private List<InputHistoryAttackUIView> _attackUIViewList;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        public void SetInputFrameData(InputFrameData inputFrameData)
        {
            //Debug.Log($"{inputFrameData.ToString()}");
            _frameText.text = inputFrameData.GetFrameString();

            foreach (var attackUIView in _attackUIViewList)
            {
                attackUIView.SetInputFrameData(inputFrameData);
            }

            _inputHistoryDirectionView.SetInputFrameData(inputFrameData);
        }

        public void UpdateInputFrameData(InputFrameData inputFrameData)
        {
            _frameText.text = inputFrameData.GetFrameString();
        }
    }
}
