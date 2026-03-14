using UnityEngine;

namespace Game.Spawner
{
    public class SpawnWarriors : MonoBehaviour
    {
        [SerializeField] private GameObject pistol;
        [SerializeField] private GameObject musketeer;
        [SerializeField] private GameObject gunner;
        [SerializeField] private GameObject sniper;
        [SerializeField] private GameObject mashineGunner;
        [SerializeField] private GameObject shielder;
        [SerializeField] private GameObject tank;
        [SerializeField] private GameObject artillery;

        private Spawner _spawner;

        private void Awake()
        {
            _spawner = GetComponentInChildren<Spawner>();
        }

        public void PistolSpawn()
        {
            _spawner.Spawn(pistol, 10);
        }

        public void MusketeerSpawn()
        {
            _spawner.Spawn(musketeer, 30);
        }

        public void GunnerSpawn()
        {
            _spawner.Spawn(gunner, 50);
        }

        public void SniperSpawn()
        {
            _spawner.Spawn(sniper, 100);
        }

        public void MashineGunnerSpawn()
        {
            _spawner.Spawn(mashineGunner, 150);
        }

        public void ShielderSpawn()
        {
            _spawner.Spawn(shielder, 180);
        }

        public void TankSpawn()
        {
            _spawner.Spawn(tank, 300);
        }

        public void ArtillerySpawn()
        {
            _spawner.Spawn(artillery, 500);
        }
    }
}
