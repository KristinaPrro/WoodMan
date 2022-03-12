using System.Collections.Generic;
using UnityEngine;

namespace Game.Bag
{
    public class BagController : MonoBehaviour, IBag
    {
        [SerializeField] private List<GameObject> _logList = new List<GameObject>(3);
        
        private bool _isEmpty;

        public bool IsEmpty { get => _isEmpty;}

        public void LoadLog()
        {
            foreach (GameObject log in _logList)
            {
                log.SetActive(true);
                _isEmpty = true;
            }
        }

        public void UnLoadLog()
        {
            foreach (GameObject log in _logList)
            {
                log.SetActive(false);
                _isEmpty = false;
            }
        }
    }
}
