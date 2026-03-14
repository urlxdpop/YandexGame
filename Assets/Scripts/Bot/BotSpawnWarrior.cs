using Game.Spawner;
using UnityEngine;

namespace Bot
{
    [RequireComponent(typeof(SpawnWarriors))]
    public class BotSpawnWarrior : MonoBehaviour
    {
        private SpawnWarriors _spawnWarriors;
        
        private int _randomWarrior;
        private int _maxRandomWarriors;
        private float _timeToSpawn;
        private float _currentTimeToSpawn;

        private const int MaxRandomWarriors = 9;

        private void Awake()
        {
            _spawnWarriors = GetComponent<SpawnWarriors>();
            _maxRandomWarriors = 1;
        }

        private void Update()
        {
            if (_currentTimeToSpawn >= _timeToSpawn)
            {
                SpawnRandomWarrior();
                _currentTimeToSpawn = 0;
                _timeToSpawn = Random.Range(1, 4);
            }
            else
            {
                _currentTimeToSpawn += Time.deltaTime;
            }
        }

        private void SpawnRandomWarrior()
        {
            _randomWarrior = Random.Range(0, _maxRandomWarriors);
            switch (_randomWarrior)
            {
                case 0:
                    _spawnWarriors.PistolSpawn();
                    break;
                case 1:
                    _spawnWarriors.MusketeerSpawn();
                    break;
                case 2:
                    _spawnWarriors.GunnerSpawn();
                    break;
                case 3:
                    _spawnWarriors.SniperSpawn();
                    break;
                case 4:
                    _spawnWarriors.MashineGunnerSpawn();
                    break;
                case 5:
                    _spawnWarriors.ShielderSpawn();
                    break;
                case 6:
                    _spawnWarriors.TankSpawn();
                    break;
                case 7:
                    _spawnWarriors.ArtillerySpawn();
                    break;
            }
            if (_maxRandomWarriors < MaxRandomWarriors && _randomWarrior + 1 == _maxRandomWarriors)
            {
                _maxRandomWarriors++;
            }
        }
    }
}
