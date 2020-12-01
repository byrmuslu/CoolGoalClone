namespace CoolGoal.UI.Button
{
    using CoolGoal.Signal;
    using UnityEngine.EventSystems;

    public class BtnTapToRestart : MyUI, IPointerDownHandler
    {
        private void Awake()
        {
            SignalBus<SignalLoseGame>.Instance.Register(Active);
            DeActive();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            SignalBus<SignalRestartGame>.Instance.Fire();
            DeActive();
        }
    }
}
