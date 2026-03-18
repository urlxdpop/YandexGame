using NUnit.Framework;
using System;
using Units.Warrior;
using UnityEngine;


namespace Game.SpawnSystem
{
    public enum  BoostType
    {
        Damage,
        Range,
        Speed,
        AttackSpeed,
        Defence,
        Hp,
        None
    }

    public class TimerToBoost : MonoBehaviour
    {
        private WarriorType[] _warriorNext;

        private int _currentWarriorIndex;
        private float _timeToGetBoost;
        private float _currentTimeToGetBoost;
        private int _numRebots = 0;

        private readonly int _numWarriorsTypes = Enum.GetValues(typeof(WarriorType)).Length;

        public event Action<WarriorType> OnBoostGet;

        private void Start()
        {
            _warriorNext = new WarriorType[_numWarriorsTypes];

            _currentWarriorIndex = _numWarriorsTypes - 1;
            _timeToGetBoost = 20f;
            _currentTimeToGetBoost = 0;

            ShuffleWarriorType();
        }

        private void Update()
        {
            if (_numRebots >= 3) return;

            _currentTimeToGetBoost += Time.deltaTime;
            if (_currentTimeToGetBoost >= _timeToGetBoost)
            {
                GetBoost();
                _currentTimeToGetBoost = 0;
            }
        }

        private void GetBoost()
        {
            OnBoostGet?.Invoke(_warriorNext[_currentWarriorIndex--]);
            if (_currentWarriorIndex < 0)
            {
                ShuffleWarriorType();
                _currentWarriorIndex = _numWarriorsTypes - 1;
                _numRebots++;
            }
        }

        private void ShuffleWarriorType()
        {
            _warriorNext = (WarriorType[])Enum.GetValues(typeof(WarriorType));

            for (int i = _warriorNext.Length - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);

                (_warriorNext[randomIndex], _warriorNext[i]) = (_warriorNext[i], _warriorNext[randomIndex]);
            }
        }
    }
}
