namespace CoolGoal.Game.Command
{
    using CoolGoal.Signal;
    using UnityEngine;
    public class Move
    {
        private IMoveable _obj;

        public Move(IMoveable obj)
        {
            _obj = obj;
        }

        public void MoveForward()
        {
            _obj.GetTransform().position = Vector3.MoveTowards(_obj.GetTransform().position, _obj.GetTargetTransform().position, _obj.GetSpeed());
            _obj.GetTransform().LookAt(_obj.GetTargetTransform().position);
            if (Vector3.Distance(_obj.GetTransform().position, _obj.GetTargetTransform().position) <= .5f)
            {
                SignalBus<SignalArriveWayPoint>.Instance.Fire();
            }
        }

    }
}
