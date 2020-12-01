namespace CoolGoal.Game
{
    using CoolGoal.Game.Command;
    using CoolGoal.Signal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class WayPointController : MonoBehaviour, IRotatable
    {
        [SerializeField] private int _levelIndex = 0;
        [SerializeField] private Ball _ball = null;
        [Space(10)]
        [SerializeField] private float _rotateSpeed = 0;
        [SerializeField] private float _maxRotationValue = 0;
        [SerializeField] private float _minRotationValue = 0;

        private List<WayPoint> _wayPoints;

        private float _rotateValue;

        private ICommand _rotateXAxis;

        private Vector3 _startingRotation;

        private bool _shooting;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _startingRotation = transform.rotation.eulerAngles;
            _wayPoints = GetComponentsInChildren<WayPoint>().ToList();
            _wayPoints.Sort((a, b) =>
            {
                if (a.Index > b.Index)
                    return 1;
                else if (a.Index < b.Index)
                    return -1;
                return 0;
            });
            var rotate = new Rotate(this);
            _rotateXAxis = new Command<Rotate>(rotate, r => r.RotateXAxis());
            SignalBus<SignalNextLevel, int>.Instance.Register(CheckLevelIndex);

        }

        private void CheckLevelIndex(int level)
        {
            if (level == _levelIndex)
            {
                Restart();
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            SignalBus<SignalRotateValue, float>.Instance.Register(SetRotateValue);
            SignalBus<SignalTouch, bool>.Instance.Register(OnTouch);
            SignalBus<SignalRestartGame>.Instance.Register(Restart);
            SignalBus<SignalShooting>.Instance.Register(OnShooting);
        }

        private void OnDisable()
        {
            SignalBus<SignalRotateValue, float>.Instance.UnRegister(SetRotateValue);
            SignalBus<SignalTouch, bool>.Instance.UnRegister(OnTouch);
            SignalBus<SignalRestartGame>.Instance.UnRegister(Restart);
            SignalBus<SignalShooting>.Instance.UnRegister(OnShooting);
        }

        private void OnShooting()
        {
            _shooting = true;
        }

        private void Restart()
        {
            _shooting = false;
            transform.rotation = Quaternion.Euler(_startingRotation);
        }

        private void Start()
        {
            _ball.SetWayPoints(_wayPoints.ToArray());
        }

        private void SetRotateValue(float rotateValue)
        {
            _rotateValue = rotateValue;
            _rotateXAxis.Execute();
        }

        private void OnTouch(bool isTouched)
        {
            if (isTouched && !_shooting)
            {
                for(int i = 0; i < _wayPoints.Count; i++)
                {
                    _wayPoints[i].Active();
                }
            }
        }

        #region Implementations
        
        public float GetDirectionValue()
        {
            return _rotateValue * -1;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public float GetRotateSpeed()
        {
            return _rotateSpeed * Time.deltaTime;
        }

        public float GetMaxRotationValue()
        {
            return _maxRotationValue;
        }

        public float GetMinRotationValue()
        {
            return _minRotationValue;
        }

        #endregion

    }
}
