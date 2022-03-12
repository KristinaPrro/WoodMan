using UnityEngine;

namespace Game.WoodMan
{
    public partial class WoodManManager
    {
        public class MoveToTreeWoodManState : WoodManState
        {
            public MoveToTreeWoodManState (WoodManManager woodman)
            {
                Init(woodman);
                _woodman.IdleSt_Init();
            }
            
            public override void ChangeState()
            {
                _woodman.State = new HackTreeWoodManState(_woodman);
            }

            public override void OnTrigger(Collider other)
            {
                _woodman.MoveToTreeSt_Trigger(other);
            }
        }
    }
}

