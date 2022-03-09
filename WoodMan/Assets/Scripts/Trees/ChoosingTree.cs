using Game.Tree;
using Game.WoodMan;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Tree
{
    public class ChoosingTree
    {

        private List<TreeInfo> _availableTreeList=new List<TreeInfo>();
        private Vector3 _startPosition;
        private UnityEvent<TreeInfo> _updateNearTree;

        public ChoosingTree (Vector3 startPosition, UnityEvent<TreeInfo> updateNearTree)
        {
            _startPosition = startPosition;
            _updateNearTree=updateNearTree;
        }

        public void AddAvailableTree(GameObject tree, ITree des)
        {
            _availableTreeList.Add(new TreeInfo
            {
                Distanse = Vector3.Distance(_startPosition, tree.transform.position),
                TreeC = des,
                TreeGO = tree
            }); ;
            ChooseTree();
        }

        public void ChooseTree()
        {
            TreeInfo near = _availableTreeList[0];
            foreach (var tree in _availableTreeList)
            {
                if (tree.Distanse < near.Distanse)
                {
                    near = tree;
                }
            }
            _updateNearTree.Invoke(near);
        }
    }

}
