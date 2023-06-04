using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class CheckPoint : MonoBehaviour
    {
        [HideInInspector]
        private bool newSavePoint; // This bool detects when a player goes through the collider 

        public static event Action<bool> SaveHereInstead;

        private void OnTriggerEnter(Collider other)
            {
                if (other.CompareTag("Player"))
                {
                    newSavePoint = true;
                    NewSaveEvent();
                    Destroy(gameObject);
                }
                else
                {
                    newSavePoint = false;
                }
            }

            private void NewSaveEvent()
            {
                SaveHereInstead?.Invoke(newSavePoint);
            }

    }
}
