using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class CheckPointRoom : MonoBehaviour
    {
        [SerializeField] private Text bottomText;
        [SerializeField] private Text topText;
        private bool nextMessagePlease;
        private int maxMessages = 13;
        [HideInInspector]
        public int currentCMessages;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                nextMessagePlease = true;
            }
            else
            {
                nextMessagePlease = false;
            }
        }
        private void Update()
        {

            if (nextMessagePlease && Input.GetKeyDown(KeyCode.E))
            {
                currentCMessages = Mathf.Clamp(currentCMessages +1, 0, maxMessages);

            }

            if (currentCMessages == 1)
            {
                topText.text = ("Welcome Sir!").ToString();
                bottomText.text = ("Here We Have").ToString();
            }

            if (currentCMessages == 2)
            {
                topText.text = ("A CheckPoint!").ToString();
                bottomText.text = ("It's So Amazing!.").ToString();
            }

            if (currentCMessages == 3)
            {
                topText.text = ("... Well You").ToString();
                bottomText.text = ("Must Be Wondering").ToString();
            }

            if (currentCMessages == 4)
            {
                topText.text = ("Why It's So").ToString();
                bottomText.text = ("Amazing? Well").ToString();
            }
            
            if (currentCMessages == 5)
            {
                topText.text = ("We Have That").ToString();
                bottomText.text = ("Clock Up Top").ToString();
            }
            
            if (currentCMessages == 6)
            {
                topText.text = ("If It Counts").ToString();
                bottomText.text = ("Down To 0").ToString();
            }
            
            if (currentCMessages == 7)
            {
                topText.text = ("Then.. Your").ToString();
                bottomText.text = ("Screwed. But").ToString();
            }
            
            if (currentCMessages == 8)
            {
                topText.text = ("With This Thing").ToString();
                bottomText.text = ("We Will Always").ToString();
            }
            
            if (currentCMessages == 9)
            {
                topText.text = ("Be Able To").ToString();
                bottomText.text = ("Well Not Be").ToString();
            }
            
            if (currentCMessages == 10)
            {
                topText.text = ("Screwed.. Haha").ToString();
                bottomText.text = ("So Make Sure").ToString();
            }
            
            if (currentCMessages == 11)
            {
                topText.text = ("You Always Go").ToString();
                bottomText.text = ("Through One In").ToString();
            }
            
            if (currentCMessages == 12)
            {
                topText.text = ("The Maze &").ToString();
                bottomText.text = ("Try Not To").ToString();
            }
            
            if (currentCMessages == 13)
            {
                topText.text = ("To Get Lost!.").ToString();
                bottomText.text = ("Try It Out.").ToString();
            }

        }
    }      
}