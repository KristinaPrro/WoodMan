using Game.Tree;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.WoodMan
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class WoodManager : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private ChoosingTree _choosingTree;
        //[SerializeField] private ChoosingTree _choosingTree; //скрипт navMesh - gththfcxtn

        private NavMeshAgent _agent;
        private TreeInfo _nearTree;
        private UnityEvent<TreeInfo> _updateNearTree;
        private bool _isCharged;
        public Vector3 StartPosition { get => _startPosition; }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _updateNearTree.AddListener((TreeInfo near) =>
            {
                //перерасет навмеша?
                _nearTree = near;
            });
        }

        private void FixedUpdate()
        {
            //if (_agent.velocity== Vector3.zero)
            //{
            //    if (!_isCharged)
            //    {
            //        _agent.destination = _nearTree.TreeGO.transform.position;

            //    }
            //    else
            //    {

            //    }
            //}
        }
    }
    public enum Anim
    {
        idle,
        go,
        back,
        collect,
        unload
    }
       
}
