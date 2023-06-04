using System;
using UnityEngine;
using UnityEngine.TextCore.Text;


namespace AlexzanderCowell
{
    public class ShoeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bootsToSpawn;
        [SerializeField] private GameObject[] spawnList;

        private float maxTimer = 10f;
        private float currentTime;
        private float spawnTime = 0f;
        private bool sBoots;

        private void Awake()
        {
            spawnList = GameObject.FindGameObjectsWithTag("SpawnPoint");
        }

        private void Start()
        {
            currentTime = maxTimer;
        }
        private void Update()
        {
            int secretArea = UnityEngine.Random.Range(0, spawnList.Length);
            
            if (currentTime < 0.2f && sBoots)
            {
                Instantiate(bootsToSpawn, spawnList[secretArea].transform.position, Quaternion.identity);
                
                currentTime = maxTimer;
            }
            currentTime -= 0.8f * Time.deltaTime;
        }

        private void OnEnable()
        {
            CharacterMovement.StartSpawningThemBoots += GetSetAndSpawn;
        }

        private void OnDisable()
        {
            CharacterMovement.StartSpawningThemBoots -= GetSetAndSpawn;
        }

        private void GetSetAndSpawn(bool spawnThemBootsSirPlease)
        {
            if (spawnThemBootsSirPlease)
            {
                sBoots = true;
            }
            else
            {
                sBoots = false;
            }
            
        }
    }
    
   
}
