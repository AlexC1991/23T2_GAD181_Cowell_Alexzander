using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class ClockObject : MonoBehaviour
    {
        private bool moreTimeAdded;
        public static event Action<bool> addMoreTime;
        private float spiningSpeed = 20;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                moreTimeAdded = true;
                MoreTimePlease();
                Destroy(gameObject);
            }
            else
            {
                moreTimeAdded = false;
            }
        }

        private void MoreTimePlease()
        {
            addMoreTime?.Invoke(moreTimeAdded); 
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward *spiningSpeed * Time.deltaTime);
        }
    }
}
