namespace CoolGoal.Game
{
    using System;
    using System.Collections;
    using CoolGoal.Signal;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private int _levelIndex = 0;

        private Vector3 _startingPosition;
        private Vector3 _startingRotation;
        private Rigidbody _body;

        private bool _deActive;
        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _startingPosition = transform.position;
            _startingRotation = transform.rotation.eulerAngles;
            SignalBus<SignalRestartGame>.Instance.Register(Restart);
            SignalBus<SignalNextLevel, int>.Instance.Register(CheckLevelIndex);
        }

        private void OnEnable()
        {
            SignalBus<SignalGoal>.Instance.Register(DeActive);
        }

        private void OnDisable()
        {
            SignalBus<SignalGoal>.Instance.UnRegister(DeActive);
        }

        private void DeActive()
        {
            _deActive = true;
        }

        private void CheckLevelIndex(int level)
        {
            if (level == _levelIndex)
            {
                gameObject.SetActive(true);
                Restart();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        private void Restart()
        {
            _deActive = false;
            transform.position = _startingPosition;
            transform.rotation = Quaternion.Euler(_startingRotation);
            if(gameObject.activeSelf)
                StartCoroutine(ForceStopped());
        }

        private IEnumerator ForceStopped()
        {
            _body.isKinematic = true;
            yield return new WaitForSeconds(.5f);
            _body.isKinematic = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball") && !_deActive)
            {
                SignalBus<SignalHitTheObstacle>.Instance.Fire();
            }
        }
    }
}
