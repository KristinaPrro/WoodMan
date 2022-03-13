using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIController: MonoBehaviour
    {
        [SerializeField] private Button _generateButton;
        private UnityEvent _generateRandomTree;
        public void SetInfo(UnityEvent generateRandomTree)
        {
            _generateRandomTree = generateRandomTree;
            _generateButton.onClick.AddListener(() =>
            {
                _generateRandomTree?.Invoke();
            });
        }
    }
}
