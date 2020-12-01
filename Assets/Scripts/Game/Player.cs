namespace CoolGoal.Game
{
    using System;
    using CoolGoal.Signal;
    using UnityEngine;

    public class Player
    {
        private bool _isTouched;

        private bool _shooting;

        private bool _firstTouch;

        public Player()
        {
            SignalBus<SignalTouch, bool>.Instance.Register(Shooting);
            SignalBus<SignalRestartGame>.Instance.Register(Restart);
            SignalBus<SignalNextLevel>.Instance.Register(Restart);
        }

        private void Restart()
        {
            _isTouched = false;
            _shooting = false;
            _firstTouch = false;
        }

        private void Shooting(bool isTouched)
        {
            _isTouched = isTouched;
            if (_firstTouch && !_shooting && !_isTouched)
            {
                SignalBus<SignalShooting>.Instance.Fire();
                _shooting = true;
            }
            if (_isTouched)
                _firstTouch = true;
        }
    }
}
