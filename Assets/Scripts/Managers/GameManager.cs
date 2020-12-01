namespace CoolGoal.Manager
{
    using System;
    using CoolGoal.Game;
    using CoolGoal.Signal;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private Player _player;
        [SerializeField] private int _totalLevel = 0;
        private int _level;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
                return;
            }
            Initialize();
            Instance = this;
        }

        private void Start()
        {
            SignalBus<SignalNextLevel, int>.Instance.Fire(0);
        }

        private void Initialize()
        {
            _player = new Player();
            SignalBus<SignalGoal>.Instance.Register(OnGoal);
            SignalBus<SignalHitTheObstacle>.Instance.Register(OnHitTheObstacle);
            SignalBus<SignalNextLevel>.Instance.Register(NextLevel);
        }

        private void NextLevel()
        {
            if(_level + 1 >= _totalLevel)
            {
                _level = 0;
            }
            else
            {
                _level++;
            }
            SignalBus<SignalNextLevel, int>.Instance.Fire(_level);
        }

        private void OnHitTheObstacle()
        {
            SignalBus<SignalLoseGame>.Instance.Fire();
        }

        private void OnGoal()
        {
            SignalBus<SignalWinGame>.Instance.Fire();
        }
    }
}
