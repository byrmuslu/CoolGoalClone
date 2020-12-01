namespace CoolGoal.UI.Panel
{
    using CoolGoal.Signal;
    
    public class PanelMain : MyUI
    {
        private void Awake()
        {
            SignalBus<SignalStartGame>.Instance.Register(DeActive);
            SignalBus<SignalLoseGame>.Instance.Register(Active);
            SignalBus<SignalRestartGame>.Instance.Register(Active);
            SignalBus<SignalWinGame>.Instance.Register(Active);
        }
    }
}
