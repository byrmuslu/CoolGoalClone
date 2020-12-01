namespace CoolGoal.Game.Command
{
    using UnityEngine;

    public interface IMoveable
    {
        Transform GetTransform();
        Transform GetTargetTransform();
        float GetSpeed();
    }
}
