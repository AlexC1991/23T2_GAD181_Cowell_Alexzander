using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class RocketBootRelocatorScript : MonoBehaviour
    {
        private void Update()
        {
            GetComponent<ParticleSystem>().Play();
        }

        private void OnEnable()
        {
            CharacterMovement.DestroyTheBootsEvent += KillBoots;
        }

        private void OnDisable()
        {
            CharacterMovement.DestroyTheBootsEvent -= KillBoots;
        }

        private void KillBoots(bool shoesOn)
        {
            if (!shoesOn!)
            {
                Destroy(GameObject.FindWithTag("RocketBoots"));
            }
        }
    }
}
