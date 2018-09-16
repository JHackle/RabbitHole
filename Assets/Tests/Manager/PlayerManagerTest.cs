namespace Hackle.Manager
{
    using Hackle.Managers;
    using NUnit.Framework;

    class PlayerManagerTest
    {
        [Test]
        public void Test()
        {
            PlayerManager pm = new PlayerManager();
            pm.CanAnyUnitMove();
        }
    }
}
