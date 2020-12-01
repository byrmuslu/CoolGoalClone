namespace CoolGoal.Game.Command
{
    using UnityEngine;

    public class Rotate
    {
        private IRotatable _obj;

        public Rotate(IRotatable obj)
        {
            _obj = obj;
        }

        public void RotateZAxis()
        {
            if (_obj.GetDirectionValue() == 0)
                return;
            Vector3 euler = _obj.GetTransform().rotation.eulerAngles;
            float direc = _obj.GetDirectionValue() * _obj.GetMaxRotationValue();
            Vector3 target = new Vector3(euler.x, euler.y, direc);
            _obj.GetTransform().rotation = Quaternion.RotateTowards(_obj.GetTransform().rotation, Quaternion.Euler(target), _obj.GetRotateSpeed());
        }

        public void RotateYAxis()
        {
            if (_obj.GetDirectionValue() == 0)
                return;
            Vector3 euler = _obj.GetTransform().rotation.eulerAngles;
            float direc = _obj.GetDirectionValue() * _obj.GetMaxRotationValue();
            Vector3 target = new Vector3(euler.x, direc, euler.z);
            _obj.GetTransform().rotation = Quaternion.RotateTowards(_obj.GetTransform().rotation, Quaternion.Euler(target), _obj.GetRotateSpeed());
        }

        public void RotateXAxis()
        {
            if (_obj.GetDirectionValue() == 0)
                return;
            Vector3 euler = _obj.GetTransform().rotation.eulerAngles;
            float direc = _obj.GetDirectionValue() * _obj.GetMaxRotationValue();
            Vector3 target = new Vector3(direc,euler.y, euler.z);
            _obj.GetTransform().rotation = Quaternion.RotateTowards(_obj.GetTransform().rotation, Quaternion.Euler(target), _obj.GetRotateSpeed());
        }

    }
}
