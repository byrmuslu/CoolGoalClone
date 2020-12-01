namespace CoolGoal.Manager
{
    using CoolGoal.Signal;
    using UnityEngine;

    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick = null;

        private bool _gameStarted;

        private void Awake()
        {
            SignalBus<SignalStartGame>.Instance.Register(()=> { Invoke("GameStarted", .5f); });
            SignalBus<SignalLoseGame>.Instance.Register(GameStopped);
            SignalBus<SignalWinGame>.Instance.Register(GameStopped);
        }

        private void GameStarted()
        {
            _gameStarted = true;
        }

        private void GameStopped()
        {
            _gameStarted = false;
        }

        private void Update()
        {
            if (!_gameStarted)
                return;
            if (Input.GetMouseButton(0))
            {
                SignalBus<SignalRotateValue, float>.Instance.Fire(_joystick.Horizontal);
                SignalBus<SignalTouch, bool>.Instance.Fire(true);
            }else
            {
                SignalBus<SignalTouch, bool>.Instance.Fire(false);
            }
        }
    }
}
