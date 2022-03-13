using Game.Wood;
using Game.WoodMan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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

        private UnityEvent<ITree> _availableTreeAdded;
        private UnityEvent<ITree> _calculateCurrentTree;
        private List<GameObject> _logGO;
        private Vector3 _pos;
        private MeshRenderer _mesh;

        public Vector3 Position { get =>_pos; }
        public GameObject TreeGO { get => gameObject; }
        public float Distanse { get; set ; }
        public UnityEvent<ITree> AvailableTreeAdded { get => _availableTreeAdded; set => _availableTreeAdded = value; }
        public UnityEvent<ITree> CalculateCurrentTree { get => _calculateCurrentTree; set => _calculateCurrentTree = value; }

        private void OnEnable()
        {
            GameManager gameManager = GameObject.FindWithTag("Game").GetComponent<GameManager>();
            AvailableTreeAdded = gameManager.AvailableTreeAdded;
            CalculateCurrentTree = gameManager.CalculateCurrentTree;
            _mesh = gameObject.GetComponent<MeshRenderer>(); ;
            _logGO = new List<GameObject>();

            UpdatePos();
            AvailableTreeAdded.Invoke(this);
        }

        private void FixedUpdate() //todo
        {
            UpdatePos();
            CalculateCurrentTree.Invoke(this);
        }

        //public void OnDrop(PointerEventData eventData)
        //{
        //    UpdatePos();
        //    CalculateCurrentTree.Invoke(this);
        //    Debug.Log("Drop");
        //}

        private void UpdatePos()
        {
            _pos = transform.position;
        }

        public float CutIntoLog()
        {
            StartCoroutine(CutTree());
            return _logCount*_timeBetweenCut;
        }

        private IEnumerator CutTree()
        {
            for (int i = 0; i < _logCount; i++)
            {
                yield return new WaitForSeconds(_timeBetweenCut);                
                Vector3 deltaPos = new Vector3(Random.Range(-_logScatter, _logScatter) ,0, Random.Range(-_logScatter, _logScatter));
                Vector3 rotation = new Vector3(Random.Range(0, 180), Random.Range(0, 90), Random.Range(0, 180));
                _logGO.Add(Instantiate(_log, transform.position + deltaPos, Quaternion.Euler(rotation)));

            }
            _mesh.enabled = false;
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
