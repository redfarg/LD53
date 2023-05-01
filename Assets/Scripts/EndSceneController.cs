using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject exitButton;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;


    // Start is called before the first frame update
    void Start()
    {
        this.startButton.GetComponent<Button>().onClick.AddListener(HandleStartButtonEvent);
        this.menuButton.GetComponent<Button>().onClick.AddListener(HandleMenuButtonEvent);
        this.exitButton.GetComponent<Button>().onClick.AddListener(HandleExitButtonEvent);

        int gameScore = PlayerPrefs.GetInt("Score");
        int highScore = PlayerPrefs.GetInt("BestScore");
        this.scoreText.text = gameScore.ToString();

        if (gameScore > highScore)
        {
            PlayerPrefs.SetInt("BestScore", gameScore);
            this.highScoreText.text = "This is a highscore!";
        }
        else
        {
            this.highScoreText.text = "Your highscore: " + PlayerPrefs.GetInt("BestScore");
        }
        PlayerPrefs.Save();
    }

    private void HandleStartButtonEvent()
    {
        
        SceneManager.LoadScene("MainScene");
    }

    private void HandleMenuButtonEvent()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void HandleExitButtonEvent()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
