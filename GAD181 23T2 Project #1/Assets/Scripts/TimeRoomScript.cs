using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TimeRoomScript : MonoBehaviour
    {
        [SerializeField] private Text bottomText;
        [SerializeField] private Text topText;
        private bool nextMessagePlease;
        private int maxMessages = 11;
        [HideInInspector]
        public int currentTMessages;

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
                currentTMessages = Mathf.Clamp(currentTMessages +1, 0, maxMessages);

            }

            if (currentTMessages == 1)
            {
                topText.text = ("Hello! This is").ToString();
                bottomText.text = ("The Time Room.").ToString();
            }

            if (currentTMessages == 2)
            {
                topText.text = ("Here We Have").ToString();
                bottomText.text = ("A Clock..").ToString();
            }

            if (currentTMessages == 3)
            {
                topText.text = ("This Keeps Your").ToString();
                bottomText.text = ("Time Always").ToString();
            }

            if (currentTMessages == 4)
            {
                topText.text = ("At 10 Seconds.").ToString();
                bottomText.text = ("This Keeps You").ToString();
            }
            
            if (currentTMessages == 5)
            {
                topText.text = ("Moving Through").ToString();
                bottomText.text = ("The Maze.").ToString();
            }
            
            if (currentTMessages == 6)
            {
                topText.text = ("If You Collect").ToString();
                bottomText.text = ("This Clock In").ToString();
            }
            
            if (currentTMessages == 7)
            {
                topText.text = ("The Maze, It").ToString();
                bottomText.text = ("Will Go Away.").ToString();
            }
            
            if (currentTMessages == 8)
            {
                topText.text = ("So Be Strategic").ToString();
                bottomText.text = ("Around the Maze").ToString();
            }
            
            if (currentTMessages == 9)
            {
                topText.text = ("And Don't Get").ToString();
                bottomText.text = ("Lost!. See").ToString();
            }
            
            if (currentTMessages == 10)
            {
                topText.text = ("That Clock There?").ToString();
                bottomText.text = ("Go & Collect It").ToString();
            }
            
            if (currentTMessages == 11)
            {
                topText.text = ("And See What").ToString();
                bottomText.text = ("Happens & Be Ready!").ToString();
            }

        }
    }      
}

