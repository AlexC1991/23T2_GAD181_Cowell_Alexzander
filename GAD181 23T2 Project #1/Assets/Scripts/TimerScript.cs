using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Text countDownClockTxT;
    private float startTime = 10;
    [HideInInspector]
    public float currentTime;
    private float finishTime = 0;
    [HideInInspector]
    public bool timeIsUp;
    private float previousTime;
    private bool previousTimeIsUp = false;
    [Header("Scripts")] 
    [SerializeField] private CharacterMovement character;
    [SerializeField] private TimeRoomScript timerRoom;
    

    private void Awake()
    {
        timeIsUp = false;
        currentTime = startTime;
    }

    private void Start()
    {
        previousTime = currentTime;
    }
    private void Update()
    {
        if (previousTime > finishTime && currentTime == finishTime)
        {
            if (!previousTimeIsUp) // Check if timeIsUp transitioned from false to true
            {
                character.currentTries += 1;
                character.currentTries = Mathf.Clamp(character.currentTries, 0, 5);

            }
            previousTimeIsUp = true;
        }
        else
        {
            previousTimeIsUp = false;
        }

        if (timerRoom.currentTMessages == 11 && character.insideOfRocketRoom == false && character.insideOfCheckPointRoom == false)
        {
            StartCountingDown();
        }
        
        countDownClockTxT.text = (currentTime).ToString("F0");

        if (currentTime < finishTime)
        {
            timeIsUp = true;
            currentTime = startTime;
            previousTime = currentTime;
        }
        
        if (character.moreTime == true)
        {
            currentTime = startTime;
            previousTime = currentTime;
            character.moreTime = false;
        }
        
        previousTime = currentTime;
    }

    private void StartCountingDown()
    {
        currentTime -= 0.7f * Time.deltaTime;
    }
    
}
}
