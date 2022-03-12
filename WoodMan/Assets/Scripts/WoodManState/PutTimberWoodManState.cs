using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.WoodMan
{
    public partial class WoodManManager : MonoBehaviour
    {
        public class PutTimberWoodManState : WoodManState
        {
            public PutTimberWoodManState(WoodManManager woodman)
            {
                Init(woodman);
                _woodman.PutTimberSt_Init();
            }

            public override void ChangeState()
            {
                _woodman.State = new IdleWoodManState(_woodman);
            }

        }
    }
}

