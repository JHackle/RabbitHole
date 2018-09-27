namespace Hackle.Objects
{
    public class Knight : MovableUnit
    {
        public Knight() : base(UnitType.Knight)
        {
            Steps = 2;
            Speed = 4f;
        }

        private new void Update()
        {
            base.Update();
        }
    }
}