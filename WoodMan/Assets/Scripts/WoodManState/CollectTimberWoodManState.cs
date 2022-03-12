namespace Game.WoodMan
{
    public partial class WoodManManager
    {
        public class CollectTimberWoodManState : WoodManState
        {
            public CollectTimberWoodManState(WoodManManager woodman)
            {
                Init(woodman);
                _woodman.CollectTimberSt_Init();
            }

            public override void ChangeState()
            {
                _woodman.State = new MoveToHomeWoodManState(_woodman);
            }
        }
    }
}

