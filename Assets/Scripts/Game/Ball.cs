namespace CoolGoal.Game
{
    using System;
    using CoolGoal.Game.Command;
    using CoolGoal.Signal;
    using UnityEngine;

    public class Ball : MonoBehaviour, IMoveable
    {
        [SerializeField] private int _levelIndex = 0;
        [SerializeField] private float _defaultSpeed = 0f;
        private float _speed;

        private WayPoint[] _wayPoints = null;
        private int _wayPointIndex;
        
        private ICommand _moveForward;

        private Rigidbody _body;
        private Vector3 _startingPosition;

        private bool _shooting;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _startingPosition = transform.position;
            _body = GetComponent<Rigidbody>();
            var move = new Move(this);
            _moveForward = new Command<Move>(move, m => m.MoveForward());
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

        private void OnDisable()
        {
            SignalBus<SignalArriveWayPoint>.Instance.UnRegister(OnArriveWayPoint);
            SignalBus<SignalGoal>.Instance.UnRegister(OnGoal);
            SignalBus<SignalShooting>.Instance.UnRegister(Shooting);
            SignalBus<SignalHitTheObstacle>.Instance.UnRegister(HitTheObject);
            SignalBus<SignalRestartGame>.Instance.UnRegister(Restart);
        }

        private void OnEnable()
        {
            SignalBus<SignalArriveWayPoint>.Instance.Register(OnArriveWayPoint);
            SignalBus<SignalGoal>.Instance.Register(OnGoal);
            SignalBus<SignalShooting>.Instance.Register(Shooting);
            SignalBus<SignalHitTheObstacle>.Instance.Register(HitTheObject);
            SignalBus<SignalRestartGame>.Instance.Register(Restart);
        }

        public void Restart()
        {
            transform.position = _startingPosition;
            transform.rotation = Quaternion.identity;
            _wayPointIndex = 0;
            _body.velocity = Vector3.zero;
            _shooting = false;
            _body.useGravity = false;
            _speed = _defaultSpeed;
        }

        private void HitTheObject()
        {
            _shooting = false;
            _body.useGravity = true;
        }

        private void Shooting()
        {
            _shooting = true;
            _speed = _defaultSpeed;
        }

        private void FixedUpdate()
        {
            if(_shooting)
                _moveForward.Execute();
        }

        private void OnGoal()
        {
            _speed = 0;
            _body.useGravity = true;
            _body.AddForce(transform.forward * 200f);
            _shooting = false;
        }

        public void SetWayPoints(WayPoint[] wayPoints)
        {
            _wayPoints = wayPoints;
        }

        private void OnArriveWayPoint()
        {
            if(_wayPointIndex + 1 >= _wayPoints.Length)
            {
                _speed = 0;
            }
            else
            {
                _wayPointIndex++;
            }
        }
        
        #region Implementations

        public float GetSpeed()
        {
            return _speed * Time.fixedDeltaTime;
        }

        public Transform GetTargetTransform()
        {
            return _wayPoints[_wayPointIndex].transform;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        #endregion
    }
}
