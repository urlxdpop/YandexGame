using Game.SpawnSystem;
using UnityEngine;

namespace Bot
{
    [RequireComponent(typeof(SpawnWarriors))]
    public class BotSpawnWarrior : MonoBehaviour
    {
        [SerializeField] private float _minTimeToSpawn;
        [SerializeField] private float _maxTimeToSpawn;

        private SpawnWarriors _spawnWarriors;
        private IBoostSelectable[] _boostSelectables;

        private int _randomWarrior;
        private int _maxRandomWarrior;
        
        private float _currentTimeToSpawn;
        private float _timeToSpawn;
        private const int MaxRandomWarriors = 9;

        private void Awake()
        {
            _boostSelectables = GetComponentsInChildren<IBoostSelectable>();

            _timeToSpawn = 3;
            _spawnWarriors = GetComponent<SpawnWarriors>();
            _maxRandomWarrior = 1;
        }

        private void Start()
        {
            foreach(var boostSelectable in _boostSelectables)
            {
                boostSelectable.OnReadyChoose += BoostSelectable_OnReadyChoose;
            }
        }

        private void Update()
        {
            if (_currentTimeToSpawn >= _timeToSpawn)
            {
                SpawnRandomWarrior();
                _currentTimeToSpawn = 0;
                _timeToSpawn = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);
            } else
            {
                _currentTimeToSpawn += Time.deltaTime;
            }
        }

        private void SpawnRandomWarrior()
        {
            _randomWarrior = Random.Range(0, _maxRandomWarrior);
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
            if (_maxRandomWarrior < MaxRandomWarriors && _randomWarrior + 1 == _maxRandomWarrior)
            {
                _maxRandomWarrior++;
            }
        }

        private void BoostSelectable_OnReadyChoose(IBoostSelectable boostSelectable)
        {
            float rand = Random.Range(0, 2);
            if (rand < 1)
            {
                boostSelectable.LeftButton();
            } else
            {
                boostSelectable.RightButton();
            }
        }
    }
}
