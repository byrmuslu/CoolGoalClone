namespace CoolGoal.Game
{
    using CoolGoal.Signal;
    using UnityEngine;

    public class WayPoint : MonoBehaviour
    {
        [SerializeField] private int _index = 0;
        public int Index { get => _index; }
        private void Start()
        {
            SignalBus<SignalShooting>.Instance.Register(DeActive);
            DeActive();
        }

        public void Active()
        {
            gameObject.SetActive(true);
        }

        public void DeActive()
        {
            gameObject.SetActive(false);
        }
        
    }
}
