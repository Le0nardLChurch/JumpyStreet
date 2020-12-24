using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Extensions;

public class UIController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Animator animator;
    [Header("Text")]
    [SerializeField] TextMeshProUGUI topScoreText;
    [SerializeField] TextMeshProUGUI[] scoreText;
    [SerializeField] TextMeshProUGUI[] coinText;
    [SerializeField] TextMeshProUGUI timerText;
    [Header("Buttons")]
    [SerializeField] Button resumeButton;
    [Header("Panels")]
    [SerializeField] GameObject[] menuPanels;       //Main, Loss, Help, Credits, Store
    [SerializeField] GameObject transitionPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pausePanel;
#pragma warning restore 0649

    InputData inputData;
    WaitForSecondsRealtime oneSecond = new WaitForSecondsRealtime(1.0f);
    int lastActivePanel;
    int currentActivePanel;
    public static bool gameStarted = false;
    public bool GameStarted
    {
        get { return gameStarted; }
        private set { gameStarted = value; }
    }


    public void UpdateTopScore(int topScore)
    {
        topScoreText.text = "Top " + topScore.ToString();
    }
    public void UpdateScore(int score)
    {
        foreach (var text in scoreText)
        {
            text.text = score.ToString();
        }
    }
    public void UpdateCoins(int coins)
    {
        PlayerPrefs.SetInt("Coins", coins);

        foreach (var text in coinText)
        {
            text.text = coins.ToString();
        }
    }

    IEnumerator Countdown()
    {
        float duration = 3.0f;
        while (duration > 0)
        {
            timerText.text = duration.ToString();
            yield return oneSecond;

            duration--;
        }
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void OnClickPause()
    {
        if (!pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            resumeButton.gameObject.SetActive(true);
            timerText.gameObject.SetActive(false);
        }
        else
        {
            resumeButton.gameObject.SetActive(false);
            timerText.gameObject.SetActive(true);
            StartCoroutine(Countdown());
        }
    }


    //Toggles all panel in menuPanels.
    public void SwitchPanel(int panelIndex)
    {
        GameObjectExt.SetAllActive(menuPanels, false);
        if (panelIndex != -1)
        {
            menuPanels[panelIndex].SetActive(true);
            lastActivePanel = currentActivePanel;
            currentActivePanel = panelIndex;
        }
    }
    public void OnBack()
    {
        if (!FindObjectOfType<Player>().IsDead)
        {
            SwitchPanel(0);
        }
        else
        {
            SwitchPanel(1);
        }
    }
    IEnumerator OnLoadTrans()
    {
        yield return new WaitForSeconds(60.0f / 60.0f);
        SwitchPanel(0);
    }
    IEnumerator OnPlayTrans()
    {
        //SlideOut
        animator.Play("SlideOut");
        yield return new WaitForSeconds(30.0f / 60.0f);
        transitionPanel.SetActive(false);
    }
    void OnPlay()
    {
        SwitchPanel(-1);
        StartCoroutine(OnPlayTrans());
        GameStarted = true;
    }
    IEnumerator OnReplayTrans()
    {
        animator.SetBool("GameOver", true);
        yield return new WaitForSeconds(120.0f / 60.0f);
        OnReset();
    }
    public void OnReplay()
    {
        transitionPanel.SetActive(true);
        StartCoroutine(OnReplayTrans());
        SwitchPanel(-1);
    }
    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnLoss()
    {
        gamePanel.SetActive(false);
        SwitchPanel(1);
    }
    public void OnQuit()
    {
        Debug.LogWarning("Quit");
        Application.Quit();
    }

    void Update()
    {
        if (!GameStarted)
        {
            if (Input.GetKeyDown(inputData.Forward) ||
                Input.GetKeyDown(inputData.Back) ||
                Input.GetKeyDown(inputData.Left) ||
                Input.GetKeyDown(inputData.Right))
            {
                OnPlay();
            }
        }
        else
        {
            if (menuPanels[0].activeSelf)
            {
                menuPanels[0].SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickPause();
        }
#if UNITY_EDITOR//Debugging Keys
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReset();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetInt("Duck", 0);
            PlayerPrefs.SetInt("CurrentSkin", 0);
            PlayerPrefs.SetInt("Coins", 60);
        } 
        if (Input.GetKeyDown(KeyCode.M))
        {
            OnReplay();
        }
#endif
    }
    void Awake()
    {
        inputData = FindObjectOfType<PlayerMovement>().PlayerInputData;
    }
    void Start()
    {
        transitionPanel.SetActive(true);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);

        animator.SetBool("GameOver", false);

        StartCoroutine(OnLoadTrans());

        GameStarted = false;
    }
}





