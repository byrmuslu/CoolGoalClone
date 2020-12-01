namespace CoolGoal.Game
{
    using System;
    using System.Collections;
    using CoolGoal.Signal;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform[] _levelsPosition = null;

        [Space(10)]
        [SerializeField] private float _speed = 0;
        [SerializeField] private float _rotateSpeed = 0;

        private Transform _target;

        private void Awake()
        {
            SignalBus<SignalNextLevel, int>.Instance.Register(NextLevel);
        }

        private void NextLevel(int obj)
        {
            if (obj >= _levelsPosition.Length)
                return;
            _target = _levelsPosition[obj];
            StartCoroutine(ToTarget());
        }

        private IEnumerator ToTarget()
        {
            var wait = new WaitForSeconds(.033f);
            while(transform.position != _target.position || transform.rotation != _target.rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _target.rotation, _rotateSpeed);
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
                yield return wait;
            }
        }
    }
}
