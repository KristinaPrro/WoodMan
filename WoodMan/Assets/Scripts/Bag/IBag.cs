namespace Game.Bag
{
    internal interface IBag
    {
        public bool IsEmpty { get; }
        public void LoadLog();
        public void UnLoadLog();
    }
}