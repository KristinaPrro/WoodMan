namespace Game.WoodMan
{
    public partial class WoodManManager
    {
        public class HackTreeWoodManState : WoodManState
        {
            public HackTreeWoodManState(WoodManManager woodman)
            {
                Init(woodman);
                _woodman.HackTreeSt_Init();
            }

            public override void ChangeState()
            {
                _woodman.State = new CollectTimberWoodManState(_woodman);
            }
        }
    }
}

