namespace CoolGoal.Game
{
    using CoolGoal.Signal;
    using UnityEngine;

    public class GoalTriggerObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                SignalBus<SignalGoal>.Instance.Fire();
            }
        }
    }
}
