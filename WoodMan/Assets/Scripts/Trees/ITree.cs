using UnityEngine;

namespace Game.Tree
{
    public interface ITree
    {
        public GameObject TreeGO { get;}        
        public Vector3 Position{ get; }  
        public float Distanse { get; set; }
        public int CutIntoLog();
    }
}