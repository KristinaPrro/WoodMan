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
        private const string ANIMATION_PARAMETR_NAME = "State";
        private const float DELTA_TIME = 1.0f;

        private Vector3 _startPosition;
        private NavMeshAgent _agent;
        private Animator _animator;
        private IBag _bag;
        private UnityEvent<ITree> _availableTreeRemoved;
        private UnityEvent<ITree> _nearTreeUpdated;
        private ITree _nearTree;
        private ITree _currentTree;
        private int _countLog;

        public IWoodManState State { get; set; }

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
            //State.SelectNewTree();
        }

        private void OnTriggerEnter(Collider other)
        {
            State.OnTrigger(other);
        }

        private void AnimationPlay(int num)
        {
            _animator.SetInteger(ANIMATION_PARAMETR_NAME, num);
        }

        // State методы
        #region IdleState
        bool _statePlayed = false;
        private void IdleSt_Init()
        {
            AnimationPlay((int)AnimState.idle);
            //_statePlayed = true;
            //IdleSt_SelectTree();
        }

        private void IdleSt_SelectTree()
        {
            if ((_nearTree != null) /*&& (_statePlayed)*/)
            {
                ChangeState();
                //_statePlayed = false;
            }
            else Debug.Log("!!!");
        }
        #endregion

        #region MoveToTreeState
        private void MoveToTreeSt_Init()
        {
            _agent.destination = _nearTree.Position;
            Debug.Log("CURR "+ _nearTree.Position);
            AnimationPlay((int)AnimState.go);
        }
        private void MoveToTreeSt_Trigger(Collider other)
        {
            //проверка на совпадение с объектом к которому мы шли? try?
            if (other.TryGetComponent(out ITree tree)) 
            {
                ChangeState();
            }
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
            _countLog = _nearTree.CutIntoLog();
            yield return new WaitForSeconds(_countLog * DELTA_TIME);

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
            //_availableTreeRemoved.Invoke(_nearTree);
            AnimationPlay((int)AnimState.collect);
            yield return new WaitForSeconds(_countLog * DELTA_TIME);

            _bag.LoadLog();// переработать?

            _availableTreeRemoved?.Invoke(_nearTree);            
            ChangeState();
        }
        #endregion

        #region MoveToHomeState
        private void MoveToHomeSt_Init()
        {
            _agent.destination = _startPosition;
            AnimationPlay((int)AnimState.go);
        }

        private void MoveToHomeSt_Trigger(Collider other)
        {
            //проверка на совпадение с объектом к которому мы шли? try?
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
            AnimationPlay((int)AnimState.collect);
            yield return new WaitForSeconds(_countLog * DELTA_TIME);

            ChangeState();
        }
        #endregion
    }
}

