using Game.Tree;
using Game.WoodMan;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.WoodMan
{
    public class ChoosingTree : MonoBehaviour
    {

        private List<ITree> _availableTreeList;
        private Vector3 _startPosition;
        private UnityEvent<ITree> _nearTreeUpdated;
        private UnityEvent<ITree> _availableTreeAdded;
        private UnityEvent<ITree> _availableTreeRemoved;

        public void SetInfo(Vector3 startPosition, UnityEvent<ITree> nearTreeUpdated,
               UnityEvent<ITree> availableTreeAdded, UnityEvent<ITree> availableTreeRemoved)
        {
            _startPosition = startPosition;
            _availableTreeList = new List<ITree>();

            _nearTreeUpdated=nearTreeUpdated;
            _availableTreeAdded =availableTreeAdded;
            _availableTreeRemoved = availableTreeRemoved;

            _availableTreeAdded.AddListener(AddAvailableTree);
            _availableTreeRemoved.AddListener(RemoveAvailableTree);
        }

        public void AddAvailableTree(ITree tree)
        {
            _availableTreeList.Add(tree);
            tree.Distanse = Vector3.Distance(_startPosition, tree.Position);
            ChooseTree();
        }
        public void RemoveAvailableTree(ITree tree)
        {
            foreach (ITree ti in _availableTreeList)
            {
                if (ti.Position == tree.Position)
                {
                    _availableTreeList.Remove(tree);
                    ChooseTree();
                    return;
                }
            }
        }

        public void ChooseTree()
        {
            ITree near;
            if (_availableTreeList.Count==0) 
            {
                near = null;
            }
            else
            {
                near = _availableTreeList[0];
                for (int i = 0; i < _availableTreeList.Count; i++)
                {
                    if (_availableTreeList[i].Distanse < near.Distanse)
                    {
                        near = _availableTreeList[i];
                    }
                }
            }
            _nearTreeUpdated?.Invoke(near);
        }
    }

}
