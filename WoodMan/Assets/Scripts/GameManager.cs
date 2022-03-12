using Game.Tree;
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

        private UnityEvent<TreeInfo> _availableTreeAdded;
        private UnityEvent<TreeInfo> _availableTreeRemoved;
        private UnityEvent<TreeInfo> _nearTreeUpdated;

        //private NavMeshSurface _surface;
        private ChoosingTree _choosingTree;

        public UnityEvent<TreeInfo> AvailableTreeAdded { get { return _availableTreeAdded; } set { _availableTreeAdded = value; } }
        public UnityEvent<TreeInfo> AvailableTreeRemoved { get { return _availableTreeRemoved; } set { _availableTreeRemoved = value; } }

        private void Awake()
        {
            _availableTreeAdded = new UnityEvent<TreeInfo>();
            _availableTreeRemoved = new UnityEvent<TreeInfo>();
            _nearTreeUpdated = new UnityEvent<TreeInfo>();

            _choosingTree = GetComponent<ChoosingTree>();

            Vector3 startPosition = _homeGO.transform.position;
            startPosition.y = _woodman.gameObject.transform.position.y;

             _choosingTree.SetInfo(startPosition, _nearTreeUpdated,_availableTreeAdded,_availableTreeRemoved);
            _woodman.SetInfo(_nearTreeUpdated, _availableTreeRemoved, startPosition);
           
            for (int i=0; i< _treeCount; i++)
            {
                Vector3 deltaPos = new Vector3(Random.Range(_zonePoint[0].position.x, _zonePoint[1].position.x), 0, Random.Range(_zonePoint[0].position.y, _zonePoint[1].position.x));
                Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
                GameObject tree=Instantiate(_treeGO, transform.position + deltaPos, Quaternion.Euler(rotation));
                tree.GetComponent<TreeController>().AvailableTreeAdded = _availableTreeAdded;
            }
        }

    }

}
