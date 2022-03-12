﻿using Game.Tree;
using Game.WoodMan;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.WoodMan
{
    public class ChoosingTree : MonoBehaviour
    {

        private List<TreeInfo> _availableTreeList;
        private Vector3 _startPosition;
        private UnityEvent<TreeInfo> _nearTreeUpdated;
        private UnityEvent<TreeInfo> _availableTreeAdded;
        private UnityEvent<TreeInfo> _availableTreeRemoved;

        public void SetInfo(Vector3 startPosition, UnityEvent<TreeInfo> nearTreeUpdated,
               UnityEvent<TreeInfo> availableTreeAdded, UnityEvent<TreeInfo> availableTreeRemoved)
        {
            _startPosition = startPosition;
            _availableTreeList = new List<TreeInfo>();

            _nearTreeUpdated=nearTreeUpdated;
            _availableTreeAdded =availableTreeAdded;
            _availableTreeRemoved = availableTreeRemoved;

            _availableTreeAdded.AddListener(AddAvailableTree);
            _availableTreeRemoved.AddListener(RemoveAvailableTree);
        }

        public void AddAvailableTree(TreeInfo tree)
        {
            _availableTreeList.Add(new TreeInfo
            {
                Distanse = Vector3.Distance(_startPosition, tree.TreeGO.transform.position),
                TreeC = tree.TreeC,
                TreeGO = tree.TreeGO
            });
            ChooseTree();
        }
        public void RemoveAvailableTree(TreeInfo tree)
        {
            foreach (TreeInfo ti in _availableTreeList)
            {
                if (ti.TreeC.Trans== tree.TreeC.Trans)
                {
                    _availableTreeList.Remove(tree);
                    ChooseTree();
                    Debug.Log("tree.TreeC.Trans "+tree.TreeC.Trans);
                    return;
                }
            }
        }

        public void ChooseTree()
        {
            TreeInfo near;
            if (_availableTreeList.Count==0) 
            {
                near =new TreeInfo
                {
                    Distanse = 0,
                    TreeC = null,
                    TreeGO = null,
                }; 
            }
            else
            {
                near = _availableTreeList[0];
                foreach (var tree in _availableTreeList)
                {
                    if (tree.Distanse < near.Distanse)
                    {
                        near = tree;
                    }
                }
            }
            _nearTreeUpdated?.Invoke(near);
        }
    }

}
