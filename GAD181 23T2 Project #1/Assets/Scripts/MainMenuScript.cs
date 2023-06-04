using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexzanderCowell
{
   
    public class MainMenuScript : MonoBehaviour
    {    
        [SerializeField] private CharacterMovement characterM;

        public void StartGame()
        {
            SceneManager.LoadScene("Maze 1");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        
        public void FirstTutorialPlease()
        {
            SceneManager.LoadScene("BeforeMaze1");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("StartMenu");
        }

        //public void ResetGame()
        //{
            //characterM.respawnStart = true;
            //characterM.MainStartRoomSpawn();
        //}
    }
}
