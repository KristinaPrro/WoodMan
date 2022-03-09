using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Tree
{
    public class TreeController : MonoBehaviour, ITree
    {
        [Header("Tree settings")] 
        [SerializeField] private float _yPos;
        [SerializeField] private ChoosingTree _choosingTree;

        [Header("Log settings")]
        [SerializeField] private GameObject _log;
        [SerializeField] private int _logCount;
        [SerializeField] private float _logScatter;

        private void OnEnable()
        {
            //
            Create();
        }

        public void CutIntoLog()
        {
            for (int i = 0; i < _logCount; i++)
            {
                float in1Percent = _logScatter / 100;
                Vector3 deltaPos = new Vector3(Random.Range(-100, 100) * in1Percent,
                    Random.Range(0, 100) * in1Percent, Random.Range(-100, 100) * in1Percent);
                Vector3 rotation = new Vector3(Random.Range(0, 180), Random.Range(0, 90), Random.Range(0, 180));
                Instantiate(_log, transform.position + deltaPos, Quaternion.Euler(rotation));
            }
            Destroy();
        }

        private void Create()
        {
            transform.position = 
                new Vector3(transform.position.x, _yPos, transform.position.z);
            _choosingTree.AddAvailableTree(gameObject, this);
        }
        
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
