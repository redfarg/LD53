
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject helpButton;
    [SerializeField] GameObject exitButton;

    [SerializeField] GameObject helpScreen;

    // Start is called before the first frame update
    void Start()
    {
        this.startButton.GetComponent<Button>().onClick.AddListener(HandleStartButtonEvent);
        this.helpButton.GetComponent<Button>().onClick.AddListener(HandleHelpButtonEvent);
        this.exitButton.GetComponent<Button>().onClick.AddListener(HandleExitButtonEvent);


        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("BestScore", 0);
        PlayerPrefs.Save();
    }

    private void HandleStartButtonEvent()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void HandleHelpButtonEvent()
    {
        this.helpScreen.SetActive(!this.helpScreen.activeSelf);
    }

    private void HandleExitButtonEvent()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
