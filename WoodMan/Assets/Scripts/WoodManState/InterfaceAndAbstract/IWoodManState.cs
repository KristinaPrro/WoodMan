using UnityEngine;

namespace Game.WoodMan
{
    public interface IWoodManState
        {
            public void ChangeState();
            public void SelectNewTree();
            public void OnTrigger(Collider other);
        }
}

