using UnityEngine;

namespace Game.WoodMan
{
    public partial class WoodManManager : MonoBehaviour
    {
        public class IdleWoodManState : WoodManState
        {
            public IdleWoodManState(WoodManManager woodman)
            {
                Init(woodman);
                _woodman.IdleSt_Init();
            }

            public override void ChangeState()
            {
                _woodman.State = new MoveToTreeWoodManState(_woodman);
            }

            public override void SelectNewTree()
            {
                _woodman.IdleSt_SelectTree();
            }
        }
    }
}

