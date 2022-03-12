using UnityEngine;

namespace Game.WoodMan
{
    public partial class WoodManManager
    {
        public class MoveToHomeWoodManState : WoodManState
        {
            public MoveToHomeWoodManState(WoodManManager woodman)
            {
                Init(woodman);
                _woodman.MoveToHomeSt_Init();
            }

            public override void ChangeState()
            {
                _woodman.State = new PutTimberWoodManState(_woodman);
            }
            public override void OnTrigger(Collider other)
            {
                _woodman.MoveToHomeSt_Trigger(other);
            }
        }
    }
}

