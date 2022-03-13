using Game.Bag;
using Game.Home;
using Game.Tree;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.WoodMan
{
    [RequireComponent(typeof(NavMeshAgent))]
    public partial class WoodManManager : MonoBehaviour
    {
        #region WoodMan
        private const string ANIMATION_PARAMETR_NAME = "State";
        private const float DELTA_TIME = 1.0f;

        private Vector3 _startPosition;
        private NavMeshAgent _agent;
        private Animator _animator;
        private IBag _bag;
        private UnityEvent<ITree> _availableTreeRemoved;
        private UnityEvent<ITree> _nearTreeUpdated;

        [SerializeField] private ITree _nearTree;
        [SerializeField] private ITree _currentTree;
        [SerializeField] private float _timeCutLog;

        [SerializeField] private IWoodManState _state;
        public IWoodManState State { get => _state; set => _state = value; }

        public WoodManManager(IWoodManState wms)
        {
            State = wms;
        }
        public void FixedUpdate()
        {
            State.SelectNewTree();
        }

        public void StartGame()
        {
            
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _bag = gameObject.GetComponent<IBag>();
            State = new IdleWoodManState(this);
        } 
        
        public void SetInfo(UnityEvent<ITree> nearTreeUpdated, UnityEvent<ITree> availableTreeRemoved, Vector3 startPosition)
        {
            StartGame();

            _nearTreeUpdated = nearTreeUpdated;
            _nearTreeUpdated.AddListener(SelectNewTree);

            _availableTreeRemoved = availableTreeRemoved;

            _startPosition = startPosition;
            _agent.destination = _startPosition;
        }

        public void ChangeState()
        {
            State.ChangeState();
        }

        public void SelectNewTree(ITree near)
        {
            _nearTree = near;
        }

        private void OnTriggerEnter(Collider other)
        {
            State.OnTrigger(other);
        }

        private void AnimationPlay(int num)
        {
            _animator.SetInteger(ANIMATION_PARAMETR_NAME, num);
        }
        #endregion

        // State методы:
        #region IdleState
        private void IdleSt_Init()
        {
            AnimationPlay((int)AnimState.idle);
        }

        private void IdleSt_SelectTree()
        {
            if (_nearTree != null) 
                ChangeState();
        }
        #endregion

        #region MoveToTreeState
        private void MoveToTreeSt_Init()
        {
            _currentTree = _nearTree;            
            _agent.destination = _currentTree.Position;
            AnimationPlay((int)AnimState.goTree);
        }
        private void MoveToTreeSt_Trigger(Collider other)
        {
            if ((other.TryGetComponent(out ITree tree)) && (tree== _currentTree))
                ChangeState();
        }
        #endregion

        #region HackTreeState
        private void HackTreeSt_Init()
        {
            StartCoroutine(CutLog());
        }

        private IEnumerator CutLog()
        {
            AnimationPlay((int)AnimState.cut);
            _timeCutLog = _currentTree.CutIntoLog();
            yield return new WaitForSeconds(_timeCutLog);
            _availableTreeRemoved?.Invoke(_currentTree);
            ChangeState();
        }
        #endregion

        #region CollectTimberState
        private void CollectTimberSt_Init()
        {
            StartCoroutine(CollectLog());
        }
        
        private IEnumerator CollectLog()
        {
            AnimationPlay((int)AnimState.collect);
            yield return new WaitForSeconds(_timeCutLog );

            _bag.LoadLog();// todo            
            ChangeState();
        }
        #endregion

        #region MoveToHomeState
        private void MoveToHomeSt_Init()
        {
            _agent.destination = _startPosition;
            AnimationPlay((int)AnimState.goHome);
        }

        private void MoveToHomeSt_Trigger(Collider other)
        {
            if (other.TryGetComponent(out IHome home))
            {
                ChangeState();
            }
        }
        #endregion

        #region PutTimberState
        private void PutTimberSt_Init()
        {
            StartCoroutine(UnloadLog());
        }
        private IEnumerator UnloadLog()
        {
            AnimationPlay((int)AnimState.put);
            yield return new WaitForSeconds(_timeCutLog * DELTA_TIME);
            _bag.UnLoadLog();
            ChangeState();
        }
        #endregion
    }
}

