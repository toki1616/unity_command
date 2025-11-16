using UnityEngine;
using Zenject;
using R3;
using System.Collections.Generic;
using System.Linq;

namespace My.Command
{
    public class InputHistoryView : MonoBehaviour
    {
        private InputViewModel _inputViewModel;

        [Inject]
        public void Construct
            (
                InputViewModel inputViewModel
            )
        {
            _inputViewModel = inputViewModel;

            _inputViewModel.InputFrameHistoryAsObservable
                .Subscribe(_ => CreateHistory(_))
                .AddTo(this);
        }

        [SerializeField]
        private GameObject parentUI;

        [SerializeField]
        private GameObject prefab;

        private List<InputHistoryItemListView> historyItemList = new List<InputHistoryItemListView>();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        private void CreateHistory(InputFrameData inputFrameData)
        {
            if (prefab == null || parentUI == null)
            {
                Debug.LogWarning("CreateHistory: prefab または parentUI が設定されていません");
                return;
            }

            //Debug.Log($"{inputFrameData.ToString()}");

            if (inputFrameData.holdFrame > 1)
            {
                historyItemList.Last().UpdateInputFrameData(inputFrameData);
                return;
            }

            // プレハブを生成して親に設定
            GameObject historyItem = Instantiate(prefab, parentUI.transform);
            var inputHistoryItemListView = historyItem.GetComponent<InputHistoryItemListView>();
            inputHistoryItemListView.SetInputFrameData(inputFrameData);

            historyItemList.Add(inputHistoryItemListView);

            int maxItems = 30; // 表示上限
            if (historyItemList.Count > maxItems)
            {
                Destroy(historyItemList[0].gameObject);
                historyItemList.RemoveAt(0);
            }
        }
    }
}
