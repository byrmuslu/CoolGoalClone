namespace CoolGoal.UI.Button
{
    using CoolGoal.Signal;
    using UnityEngine.EventSystems;

    public class BtnTapToNextLevel : MyUI, IPointerDownHandler
    {
        private void Awake()
        {
            SignalBus<SignalWinGame>.Instance.Register(Active);
            DeActive();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SignalBus<SignalNextLevel>.Instance.Fire();
            DeActive();
        }
    }
}
