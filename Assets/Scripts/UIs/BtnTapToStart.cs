namespace CoolGoal.UI.Button
{
    using CoolGoal.Signal;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class BtnTapToStart : MyUI, IPointerDownHandler
    {
        private void Awake()
        {
            SignalBus<SignalRestartGame>.Instance.Register(Active);
            SignalBus<SignalNextLevel>.Instance.Register(Active);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SignalBus<SignalStartGame>.Instance.Fire();
            DeActive();
        }
        
    }
}
