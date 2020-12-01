namespace CoolGoal.Game.Command
{
    using UnityEngine;

    public interface IRotatable
    {
        Transform GetTransform();
        float GetRotateSpeed();
        float GetDirectionValue();
        float GetMaxRotationValue();
        float GetMinRotationValue();
    }
}
