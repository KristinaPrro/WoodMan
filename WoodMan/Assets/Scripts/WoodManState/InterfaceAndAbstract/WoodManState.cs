using UnityEngine;

namespace Game.WoodMan
{
    public abstract class WoodManState : IWoodManState
    {
        protected WoodManManager _woodman;
        protected void Init(WoodManManager woodman)
        {
            _woodman = woodman;
            Debug.Log(this);
        }

        public abstract void ChangeState();

        public virtual void SelectNewTree() { }

        public virtual void OnTrigger(Collider other) { }
    }
}

