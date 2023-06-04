using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

namespace AlexzanderCowell
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Scripts")] 
        [SerializeField] private TimerScript timeScript;
        [SerializeField] private RocketRoomTrainer roomRScript;
        [SerializeField] private TimeRoomScript timeRoomS;
        [SerializeField] private CheckPointRoom checkRoomPoint;
        [SerializeField] private TestingCheckP testCheck;
        
        [Header("Character Run Speed"), SerializeField]
        [HideInInspector]
        public float runSpeed = 8f;
        [SerializeField] private float maxRunSpeed = 12;
        private float originalRunSpeed;
        private bool timeToMoveFaster;

        [Header("Character Jump Height")] 
        [SerializeField] private float jumpHeight = 3;

        [Header("SpawnLocations")]
        [SerializeField] private Transform startPosition;
        [SerializeField] public Transform timeSpawnPoint;
        [SerializeField] private Transform rocketSpawnPoint;
        [SerializeField] private Transform checkSpawnPoint;
        [HideInInspector] public bool insideOfRocketRoom;
        [HideInInspector] public bool insideOfCheckPointRoom;
        
        [Header("Character info")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private float mouseSensitivity = 5;
        
        private float mouseXposition;
        private Vector3 newPosition;
        [SerializeField] private float characterGravity = 20;
        private Vector3 moveDirection;
        private float moveHorizontal;
        private float moveVertical;
        private bool alwaysCheckSpeed;
        [HideInInspector] public bool moreTime;

        [Header("UI Elements")] 
        [SerializeField] private Text counterText;
        [SerializeField] private GameObject beforeItStarts;
        [SerializeField] private GameObject inGameMenuStart;
        [SerializeField] private GameObject soundOnYes;
        [SerializeField] private GameObject soundOnNo;
        [SerializeField] private Text soundIsWhat;
        
        private int maxTries = 5;
        [HideInInspector]
        public int currentTries;
        private int startingTries = 0;
        private float startCount = 1;
        private bool relocatedToSpawnPoint;
        
        [Header("Boots")] 
        private GameObject bootLeft;
        private GameObject bootRight;
        
        private Vector3 footLeft;
        private Vector3 footRight;
        private bool shoesOn;
        private float maxBootTimer = 5;
        private float currentBootTimer;
        private readonly float finishedBootTimer = 0;
        private GameObject slideTimer;

        private bool shouldTeleport = false;
        private bool spawnThemBootsSirPlease = false;
        private bool insideOfTimeRoom = false;

        private float quickGetThem = 4;
        private bool beforeGameStarts = false;
        private bool menuDuringGame = false;
        [HideInInspector]
        public bool respawnStart = false;
        
        [Header("Sounds")] 
        [SerializeField] private AudioSource _mainGameSound;
        [SerializeField] private AudioSource _jumpSounds;
        [SerializeField] private AudioSource _timeSounds;
        [SerializeField] private AudioSource _rocketTimeSounds;
        
        public static event Action<bool> DestroyTheBootsEvent;
        public static event Action<bool> StartSpawningThemBoots;

        private void Awake()
        {
            footLeft = GameObject.Find("RocketShoeLeftLocation").transform.position;
            footRight = GameObject.Find("RocketShoeRightLocation").transform.position;
            slideTimer = GameObject.Find("Slider");
            slideTimer.GetComponent<CanvasGroup>().alpha = 0;
        }
        private void Start()
        {
            
            Time.timeScale = 1;
            currentTries = startingTries;
            alwaysCheckSpeed = true;
            originalRunSpeed = runSpeed;
            runSpeed = 0;
            menuDuringGame = false;
            roomRScript.currentRMessages = 0;
            checkRoomPoint.currentCMessages = 0;
            timeRoomS.currentTMessages = 0;
            CheckPointSpawnRoom();
        }

        public void MainSoundOn()
        {
            _mainGameSound.Play();
            soundOnYes.GetComponent<CanvasGroup>().alpha = 0;
            soundOnYes.GetComponent<CanvasGroup>().interactable = false;
            soundOnYes.GetComponent<CanvasGroup>().blocksRaycasts = false;
            
            soundOnNo.GetComponent<CanvasGroup>().alpha = 1;
            soundOnNo.GetComponent<CanvasGroup>().interactable = true;
            soundOnNo.GetComponent<CanvasGroup>().blocksRaycasts = true;

            soundIsWhat.text = ("Sound Is On").ToString();
            soundIsWhat.GetComponent<Text>().color = Color.green;
        }

        public void MainSoundOff()
        {
            _mainGameSound.Stop();
            soundOnYes.GetComponent<CanvasGroup>().alpha = 1;
            soundOnYes.GetComponent<CanvasGroup>().interactable = true;
            soundOnYes.GetComponent<CanvasGroup>().blocksRaycasts = true;
            
            soundOnNo.GetComponent<CanvasGroup>().alpha = 0;
            soundOnNo.GetComponent<CanvasGroup>().interactable = false;
            soundOnNo.GetComponent<CanvasGroup>().blocksRaycasts = false;
            
            soundIsWhat.text = ("Sound Is Off").ToString();
            soundIsWhat.GetComponent<Text>().color = Color.red;
        }

        private void TimeSpawnRoom()
        {
            controller.transform.position = timeSpawnPoint.position;
            runSpeed = 0;
            insideOfTimeRoom = true;
            timeScript.currentTime = 10;
            shouldTeleport = false;
        }
        
        public void MainStartRoomSpawn()
        {
            inGameMenuStart.SetActive(false);
            beforeGameStarts = true;
            Time.timeScale = 0;
            spawnThemBootsSirPlease = true;
            timeScript.currentTime = 10;
            roomRScript.currentRMessages = 0;
            insideOfRocketRoom = false;
            SpawnSomeBootsEvent();
        }

        private void CheckPointSpawnRoom()
        {
            controller.transform.position = checkSpawnPoint.position;
            insideOfCheckPointRoom = true;
        }

        private void RocketBootRoomSpawnPoint()
        {
            checkRoomPoint.currentCMessages = 0;
            timeRoomS.currentTMessages = 0;
            insideOfRocketRoom = true;
            insideOfTimeRoom = false;
            runSpeed = 0;
            controller.transform.position = rocketSpawnPoint.position;

        }
        private void OnEnable()
        {
            CheckPoint.SaveHereInstead += SavePointUpdated;
            ClockObject.addMoreTime += IncreasedTimeSir;
        }
        
        private void OnDisable()
        {
            CheckPoint.SaveHereInstead -= SavePointUpdated;
            ClockObject.addMoreTime -= IncreasedTimeSir;
        }

        private void FixedUpdate()
        {
            if (roomRScript.currentRMessages == 11)
            {
                runSpeed = originalRunSpeed;
            }
            
            if (timeRoomS.currentTMessages == 11 && insideOfTimeRoom)
            {
                insideOfCheckPointRoom = false;
                runSpeed = originalRunSpeed;
            }
            
            if (checkRoomPoint.currentCMessages == 13)
            {
                runSpeed = originalRunSpeed;
            }
            
            if (testCheck.playerTestingIt)
            {
                shouldTeleport = true;
            }

            if (shouldTeleport)
            {
                TimeSpawnRoom();
                testCheck.playerTestingIt = false;
                checkRoomPoint.currentCMessages = 0;
            }
            
            if (timeScript.currentTime < 5 && insideOfTimeRoom)
            {
                RocketBootRoomSpawnPoint();
                timeScript.currentTime = 10;
                timeRoomS.currentTMessages = 0;
            }

            if (!shoesOn! && roomRScript.currentRMessages == 11)
            {
                if (quickGetThem < 0.2f)
                {
                    MainStartRoomSpawn();
                    controller.transform.position = startPosition.position;
                    quickGetThem = 5;
                    
                }
            }
        }

        public void PlayTheGame()
        {
            Time.timeScale = 1;
            beforeItStarts.SetActive(false);
            timeRoomS.currentTMessages = 11;
            MainSoundOn();
            beforeGameStarts = false;
            menuDuringGame = true;
        }

        public void ExitInGameMenu()
        {
            menuDuringGame = false;
            Time.timeScale = 1;
            inGameMenuStart.SetActive(false);
            menuDuringGame = true;

        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape) && menuDuringGame)
            {
                Time.timeScale = 0;
                inGameMenuStart.SetActive(true);
            }
            
            if (Time.timeScale == 0 && beforeGameStarts)
            {
                beforeItStarts.SetActive(true);
            }
            if (roomRScript.currentRMessages == 11)
            {
                quickGetThem -= 0.4f * Time.deltaTime;
            }
            Debug.Log(testCheck.playerTestingIt);
            counterText.text = (currentTries + "/" + maxTries).ToString();
            BootsDisabled();
            BootsEnabled();
            JumpMovement();
            mouseXposition = mouseSensitivity * Input.GetAxis("Mouse X");
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            //if (currentTries == maxTries)
            //{
                //startCount -= 0.3f * Time.deltaTime;
                //if (startCount < 0.2f)
                //{
                    //SceneManager.LoadScene("Maze 1");
                //}
                
            //}
            
            // Calculate the movement vector
            Vector3 movement = new Vector3(-moveHorizontal, 0f, -moveVertical);
            movement = transform.TransformDirection(movement) * runSpeed;
            
            transform.Rotate(Vector3.up, mouseXposition * 120 * Time.deltaTime);
            
            // Apply gravity
            if (!controller.isGrounded)
            {
                moveDirection.y -= characterGravity * Time.deltaTime;
            }

            // Move the character using CharacterController's Move method
            controller.Move((movement + moveDirection) * Time.deltaTime);

            if (timeScript.currentTime < 0.2f)
            {
                controller.transform.position = newPosition;
                relocatedToSpawnPoint = true;
            }
            else relocatedToSpawnPoint = false;
            
        }
        private void JumpMovement()
        {
            if (Input.GetKeyDown(KeyCode.Space) && (controller.isGrounded))
            {
                _jumpSounds.Play();
                moveDirection.y = Mathf.Sqrt(2f * jumpHeight * characterGravity);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("RocketBoots")) return;
            shoesOn = true;
            _rocketTimeSounds.Play();
            currentBootTimer = maxBootTimer;
        }
        private void SavePointUpdated(bool newSavePoint)
        {
            if (newSavePoint)
            {
                newPosition = controller.transform.position;
            }
        }
        private void ChangeSpeed()
        {
            
            runSpeed = maxRunSpeed;
            
        }

        private void ResetSpeed()
        {
            runSpeed = originalRunSpeed;
        }

        private void IncreasedTimeSir(bool newSavePoint)
        {
            if (newSavePoint)
            {
                moreTime = true;
                _timeSounds.Play();
            }
            else
                moreTime = false;
        }
        
        private void BootsEnabled()
        {
            if (shoesOn)
            {
                bootLeft = GameObject.FindWithTag("LeftRocketShoe");
                bootRight = GameObject.FindWithTag("RightRocketShoe");
                footLeft = GameObject.Find("RocketShoeLeftLocation").transform.position;
                footRight = GameObject.Find("RocketShoeRightLocation").transform.position;
                ChangeSpeed();
                slideTimer.GetComponent<Slider>().value = SliderCountDown();
                transform.Rotate(Vector3.up, mouseXposition * 120 * Time.deltaTime);
                bootLeft.transform.position = footLeft;
                bootRight.transform.position = footRight;
                currentBootTimer -= 0.8f * Time.deltaTime;
                AppearingSlider();
            }
        }

        private void BootsDisabled()
        {
            if (currentBootTimer < 0.1f && shoesOn)
            {
                ResetSpeed();
                shoesOn = false;
                BootsWillBeDestroyedEvent();
                slideTimer.GetComponent<CanvasGroup>().alpha = 0;
                currentBootTimer = finishedBootTimer;
            }
        }

        private float SliderCountDown()
        {
            return (currentBootTimer / maxBootTimer);
        }
        private void AppearingSlider()
        {
            slideTimer.GetComponent<CanvasGroup>().alpha = 1;
        }

        private void BootsWillBeDestroyedEvent()
        {
            DestroyTheBootsEvent?.Invoke(shoesOn);
        }

        private void SpawnSomeBootsEvent()
        {
            StartSpawningThemBoots?.Invoke(spawnThemBootsSirPlease);
        }
        
        
    } 
}

