using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPointerDownHandler
{
    public static GameManager Instance; //Singleton

    [SerializeField] List<GameObject> racerPositions;

    [SerializeField] TextMeshProUGUI racerNameText;
    [SerializeField] TextMeshProUGUI playerOrderText;

    [SerializeField] Image playerImage;
    [SerializeField] Image opponentImage;
    [SerializeField] SpriteRenderer playerTagImage;

    [SerializeField] GameObject touchStartScreen;
    [SerializeField] GameObject timerObject;
    [SerializeField] GameObject particleEffects;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI raceRealTimeText;
    [SerializeField] float startTimerValue = 3f;
    private float _raceRealTimeValue = 0f;

    [SerializeField] List<GameObject> levels;
    [SerializeField] int levelIndex;

    [SerializeField] CinemachineVirtualCamera virtualCam;
    [SerializeField] GameObject paintWall;
    [SerializeField] GameObject player;
    [SerializeField] GameObject paintedBar;

    [SerializeField] Text textPieValue;
    [SerializeField] Text textPiePercent;

    [SerializeField] GameObject levelSuccessButton;
    [SerializeField] GameObject levelSuccessScreen;

    [SerializeField] GameObject danceCamPos;

    [SerializeField] CollisionControllerPlayer collisionControllerPlayer;

    private GameObject level;

    public bool isStartGame;
    private bool _isTimerStart;
    public bool _isLevelButtonActivate;
    public bool _isLevelScreenActivate;

    private int pieValue;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        isStartGame = false;
        _isTimerStart = false;
        _isLevelButtonActivate = false;
        _isLevelScreenActivate = false;


        touchStartScreen.SetActive(true);
        particleEffects.SetActive(false);
        levelSuccessButton.SetActive(false);
        levelSuccessScreen.SetActive(false);

        paintWall.GetComponent<MeshCollider>().enabled = false;

        racerPositions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Racer"));
        racerPositions.Add(GameObject.FindGameObjectWithTag("Player"));

        RacersNames();

        levelIndex = PlayerPrefs.GetInt("LevelSave");

        if(SceneManager.GetActiveScene().buildIndex < levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }

    }

    public void OnPointerDown(PointerEventData eventData) //Closes The Touch to Start Screen and Counts Down From 3
    {
        touchStartScreen.SetActive(false);
        timerObject.SetActive(true);
        _isTimerStart = true;
    }

    private void Update()
    {
        TimerStart();
        RacersOrder();

        textPiePercent.text = "%" + textPieValue.GetComponent<Text>().text;

        int.TryParse(textPieValue.GetComponent<Text>().text, out pieValue); //Calculate Percentage and Show Level Success Button
        if (pieValue >= 75 && !_isLevelButtonActivate)
        {
            levelSuccessButton.SetActive(true);
            _isLevelButtonActivate = true;
            Debug.Log("Level Success");
        }
    }

    private void RacersNames() //Character Name Edit Function
    {

        for (int i = 0; i < racerPositions.Count; i++)
        {
            racerPositions[i].gameObject.name = i.ToString();

            switch (i)
            {
                case 0:
                    racerPositions[i].gameObject.name = "Lucie";
                    break;
                case 1:
                    racerPositions[i].gameObject.name = "Eloise";
                    break;
                case 2:
                    racerPositions[i].gameObject.name = "Paula";
                    break;
                case 3:
                    racerPositions[i].gameObject.name = "Marie";
                    break;
                case 4:
                    racerPositions[i].gameObject.name = "Alice";
                    break;
                case 5:
                    racerPositions[i].gameObject.name = "Rachael";
                    break;
                case 6:
                    racerPositions[i].gameObject.name = "Constance";
                    break;
                case 7:
                    racerPositions[i].gameObject.name = "Shane";
                    break;
                case 8:
                    racerPositions[i].gameObject.name = "Arabella";
                    break;
                case 9:
                    racerPositions[i].gameObject.name = "Zahra";
                    break;
                case 10:
                    racerPositions[i].gameObject.name = "Player";
                    break;
            }
        }
    }

    private void RacersOrder() //Character Sorting Algorithm Function
    {
        for (int i = 0; i < racerPositions.Count; i++)
        {
            var item = racerPositions[i];
            var currentIndex = i;
            while (currentIndex > 0 && racerPositions[currentIndex - 1].transform.position.z < item.transform.position.z)
            {
                racerPositions[currentIndex] = racerPositions[currentIndex - 1];
                currentIndex--;
          
            }
            if (racerPositions[i] == GameObject.FindGameObjectWithTag("Player"))
            {
                playerOrderText.text = (i + 1) + " / 11";
                if (i == 0)
                {
                    playerImage.enabled = true;
                    opponentImage.enabled = false;
                }
                else
                {
                    opponentImage.enabled = true;
                    playerImage.enabled = false;
                }
            }
            racerPositions[currentIndex] = item;

            racerNameText.text = racerPositions[racerPositions.Count - (i + 1)].gameObject.name.ToString();
        }
      
    }
    private void TimerStart() //Timer Controller
    {
        if (_isTimerStart)
        {
            startTimerValue -= Time.deltaTime;
            if (startTimerValue <= 1)
            {
                isStartGame = true;
                timerObject.SetActive(false);
                particleEffects.SetActive(true);
                startTimerValue = 0;
            }
        }
        timerText.text = Mathf.Round(startTimerValue).ToString();

        if (isStartGame && !collisionControllerPlayer.isRaceFinished)
        {
            _raceRealTimeValue += Time.deltaTime;
            raceRealTimeText.text = Mathf.Round(_raceRealTimeValue).ToString();
        }
    }

    public void JumpToBase(GameObject gameObject)
    {
        gameObject.transform.DOLocalJump(Vector3.zero, 1f, 2, 1f);
    }

    public void RotatingPlatformLeft(GameObject gameObject, float interactTork)
    {
        gameObject.transform.Translate(Vector3.left * interactTork * Time.deltaTime);
    }

    public void RotatingPlatformRight(GameObject gameObject, float interactTork)
    {
        gameObject.transform.Translate(Vector3.right * interactTork * Time.deltaTime);
    }

    public void NextLevel() //Next Level Button 
    {
        if (levelIndex < 2)
        {
            levelIndex++;
        }
        else
        {
            levelIndex = 0;
        }

        PlayerPrefs.SetInt("LevelSave", levelIndex);
        SceneManager.LoadScene(levelIndex);

    }

    public void PaintAction(GameObject paintWall, GameObject player, Vector3 paintPos) //EndGame Cam and Character Position Edit Function
    {
        playerTagImage.gameObject.SetActive(false);
        player.transform.DOMove(paintPos, 1f);
        paintWall.transform.DOLocalJump(Vector3.zero, 2f, 2, 3f);
        StartCoroutine(nameof(CamLook));  
    }

    IEnumerator CamLook()
    {
        yield return new WaitForSeconds(3f);
        paintWall.GetComponent<MeshCollider>().enabled = true;
        virtualCam.LookAt = paintWall.transform;
        paintedBar.SetActive(true); 
    }
    
    public void LevelSuccess()
    {
        virtualCam.Follow = danceCamPos.transform;


        paintedBar.SetActive(false);
        levelSuccessButton.SetActive(false);

        levelSuccessScreen.SetActive(true);
        playerImage.gameObject.SetActive(false);
        playerOrderText.enabled = false;
        opponentImage.gameObject.SetActive(false);
        racerNameText.enabled = false;

        _isLevelScreenActivate = true;
    }

    public void EndGameDance(GameObject player,Animator _playerAnim)
    {
        _playerAnim.SetBool("isDance", true);
        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180, player.transform.eulerAngles.z);
    }
}
