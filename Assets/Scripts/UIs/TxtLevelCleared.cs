namespace CoolGoal.UI.Text
{
    using CoolGoal.Signal;
    using UnityEngine;

    public class TxtLevelCleared : MyUI
    {
        private void Awake()
        {
            SignalBus<SignalWinGame>.Instance.Register(Active);
            SignalBus<SignalRestartGame>.Instance.Register(DeActive);
            SignalBus<SignalNextLevel>.Instance.Register(DeActive);
            DeActive();
        }

    }
}
