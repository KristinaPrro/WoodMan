using Game.Tree;
using Game.UI;
using Game.WoodMan;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Wood
{
    public class GameManager : MonoBehaviour
    {
        
        [SerializeField] private WoodManManager _woodman;

        [SerializeField] private GameObject _treeGO;
        [SerializeField] private GameObject _homeGO;
        [SerializeField] private int _treeCount;
        [SerializeField] private Transform[] _zonePoint=new Transform[2];

        private UnityEvent<ITree> _availableTreeAdded;
        private UnityEvent<ITree> _calculateCurrentTree;
        private UnityEvent<ITree> _availableTreeRemoved;
        private UnityEvent<ITree> _nearTreeUpdated;
        private UnityEvent _generateRandomTree;

        private ChoosingTree _choosingTree;
        private UIController _uIController;


        public UnityEvent<ITree> AvailableTreeAdded { get { return _availableTreeAdded; } set { _availableTreeAdded = value; } }
        public UnityEvent<ITree> CalculateCurrentTree { get { return _calculateCurrentTree; } set { _calculateCurrentTree = value; } }
        public UnityEvent<ITree> AvailableTreeRemoved { get { return _availableTreeRemoved; } set { _availableTreeRemoved = value; } }

        private void Awake()
        {
            SetInfo();
        }

        private void StartGame()
        {
            _availableTreeAdded = new UnityEvent<ITree>();
            _calculateCurrentTree = new UnityEvent<ITree>();
            _availableTreeRemoved = new UnityEvent<ITree>();
            _nearTreeUpdated = new UnityEvent<ITree>();
            _generateRandomTree = new UnityEvent();

            _choosingTree = gameObject.GetComponent<ChoosingTree>();
            _uIController = gameObject.GetComponent<UIController>();
            SetInfo();
        }

        private void SetInfo()
        {
            Vector3 startPosition = _homeGO.transform.position;
            startPosition.y = _woodman.gameObject.transform.position.y;

            _uIController.SetInfo(_generateRandomTree);
            _choosingTree.SetInfo(startPosition, _nearTreeUpdated, _availableTreeAdded, _availableTreeRemoved);
            _woodman.SetInfo(_nearTreeUpdated, _availableTreeRemoved, startPosition);

            _generateRandomTree.AddListener(GenerateRandomeTree);

            for (int i = 0; i < _treeCount; i++)
            {
                GenerateRandomeTree();
            }
        }

        private void GenerateRandomeTree()
        {
            Vector3 deltaPos = new Vector3(Random.Range(_zonePoint[0].position.x, _zonePoint[1].position.x), 0, Random.Range(_zonePoint[0].position.y, _zonePoint[1].position.x));
            Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
            GameObject tree = Instantiate(_treeGO, transform.position + deltaPos, Quaternion.Euler(rotation));
            tree.GetComponent<TreeController>().AvailableTreeAdded = _availableTreeAdded;
            tree.GetComponent<TreeController>().CalculateCurrentTree = _calculateCurrentTree;
        }
    }
}
