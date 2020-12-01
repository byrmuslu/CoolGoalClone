namespace CoolGoal.UI.Text
{
    using CoolGoal.Signal;
    
    public class TxtLevelFailed : MyUI
    {
        private void Awake()
        {
            SignalBus<SignalLoseGame>.Instance.Register(Active);
            SignalBus<SignalRestartGame>.Instance.Register(DeActive);
            DeActive();
        }
    }
}
