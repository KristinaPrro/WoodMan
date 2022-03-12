using Game.Wood;
using Game.WoodMan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Tree
{
    public class TreeController : MonoBehaviour, ITree
    {
        [Header("Tree settings")] 
        [SerializeField] private float _yPos;
        //[SerializeField] private ChoosingTree _choosingTree;

        [Header("Log settings")]
        [SerializeField] private GameObject _log;
        [SerializeField] private int _logCount;
        [SerializeField] private float _logScatter;
        [SerializeField] private float _timeBetweenCut;

        private UnityEvent<TreeInfo> _availableTreeAdded;
        private UnityEvent<TreeInfo> _availableTreeRemoved;
        private List<GameObject> _logGO;
        private Vector3 _pos;

        public Vector3 Trans{ get =>_pos; }
        public UnityEvent<TreeInfo> AvailableTreeAdded { get => _availableTreeAdded; set => _availableTreeAdded = value; }
        public GameObject TreeGO { get => gameObject; }
        public float Distanse { get; set ; }

        private void OnEnable()
        {
            AvailableTreeAdded = GameObject.FindWithTag("Game").GetComponent<GameManager>().AvailableTreeAdded;      
            _pos=new Vector3(transform.position.x, _yPos, transform.position.z);
            transform.position = _pos;

            _logGO = new List<GameObject>();

            AvailableTreeAdded.Invoke(new TreeInfo {
                TreeC = this,
                TreeGO = gameObject
            }) ;
        }

        public int CutIntoLog()
        {
            StartCoroutine(CutTree());
            return _logCount;
        }

        private IEnumerator CutTree()
        {
            for (int i = 0; i < _logCount; i++)
            {
                yield return new WaitForSeconds(_timeBetweenCut);                
                float in1Percent = _logScatter / 100;
                Vector3 deltaPos = new Vector3(Random.Range(-100, 100) * in1Percent,0, Random.Range(-100, 100) * in1Percent);
                Vector3 rotation = new Vector3(Random.Range(0, 180), Random.Range(0, 90), Random.Range(0, 180));
                _logGO.Add(Instantiate(_log, transform.position + deltaPos, Quaternion.Euler(rotation)));
            }
            yield return new WaitForSeconds(_logCount*1f);
            Destroy();

        }
        
        private void Destroy()
        {
            Destroy(gameObject);
            foreach (GameObject log in _logGO)
            {
                Destroy(log, 0.1f);
            }
        }

    }
}
