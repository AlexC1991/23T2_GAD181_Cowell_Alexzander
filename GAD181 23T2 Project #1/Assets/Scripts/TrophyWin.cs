using System;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AlexzanderCowell
{
    
    public class TrophyWin : MonoBehaviour
    {
        private float spiningSpeed = 10;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene("You Won!");
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward *spiningSpeed * Time.deltaTime);
        }
    }
}