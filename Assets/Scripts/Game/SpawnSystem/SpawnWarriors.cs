using Units.Warrior;
using UnityEngine;

namespace Game.SpawnSystem
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
            BoostType[] pistolBoosts = GetBoostTypes(WarriorType.Pistol);
            _spawner.Spawn(pistol, 10, pistolBoosts);
        }

        public void MusketeerSpawn()
        {
            BoostType[] musketeerBoosts = GetBoostTypes(WarriorType.Musketeer);
            _spawner.Spawn(musketeer, 30, musketeerBoosts);
        }

        public void GunnerSpawn()
        {
            BoostType[] gunnerBoosts = GetBoostTypes(WarriorType.Gunner);
            _spawner.Spawn(gunner, 50, gunnerBoosts);
        }

        public void SniperSpawn()
        {
            BoostType[] sniperBoosts = GetBoostTypes(WarriorType.Sniper);
            _spawner.Spawn(sniper, 100, sniperBoosts);
        }

        public void MashineGunnerSpawn()
        {
            BoostType[] mashineGunnerBoosts = GetBoostTypes(WarriorType.MashineGunner);
            _spawner.Spawn(mashineGunner, 150, mashineGunnerBoosts);
        }

        public void ShielderSpawn()
        {
            BoostType[] shielderBoosts = GetBoostTypes(WarriorType.Shielder);
            _spawner.Spawn(shielder, 180, shielderBoosts);
        }

        public void TankSpawn()
        {
            BoostType[] tankBoosts = GetBoostTypes(WarriorType.Tank);
            _spawner.Spawn(tank, 300, tankBoosts);
        }

        public void ArtillerySpawn()
        {
            BoostType[] artilleryBoosts = GetBoostTypes(WarriorType.Artillery);
            _spawner.Spawn(artillery, 500, artilleryBoosts);
        }

        private BoostType[] GetBoostTypes(WarriorType warriorType)
        {
            BoostType[] boostTypes = new BoostType[3];
            Boosts[] boosts = GetComponentsInChildren<Boosts>();

            for (int i = 0; i < boosts.Length; i++)
            {
                if (boosts[i].WarriorType == warriorType)
                {
                    boostTypes = boosts[i].BoostsChoose;
                    break;
                }
            }

            return boostTypes;
        }
    }
}
