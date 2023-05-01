using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    [SerializeField] CollisionZone collisionZone;

    [SerializeField] Dropoff dropoff01;
    [SerializeField] Dropoff dropoff02;
    [SerializeField] Dropoff dropoff03;
    [SerializeField] Dropoff dropoff04;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI modeText;

    public AudioClip[] plopSounds;
    public AudioSource audioSourcePlop;

    public AudioSource audioSourceErr;
    public AudioSource audioSourceDestr;


    private int score = 0;
    private int lives = 3;

    private float switchTimeMin = 5f;
    private float switchTimeMax = 8f;

    Mode currentMode;

    ModeColor[] valuesColors = (ModeColor[]) Enum.GetValues(typeof(ModeColor));
    ModeShape[] valuesShapes = (ModeShape[]) Enum.GetValues(typeof(ModeShape));
    ModeSymbol[] valuesSymbols = (ModeSymbol[]) Enum.GetValues(typeof(ModeSymbol));

    Dropoff[] dropoffs = new Dropoff[4];

    System.Random rng = new System.Random();

    void Start()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        modeText.text = "";

        collisionZone.PackageDestroyedEvent.AddListener(HandlePackageDestroyedEvent);

        dropoff01.PackageDeliveredCorrectEvent.AddListener(HandlePackageDeliveredCorrectEvent);
        dropoff02.PackageDeliveredCorrectEvent.AddListener(HandlePackageDeliveredCorrectEvent);
        dropoff03.PackageDeliveredCorrectEvent.AddListener(HandlePackageDeliveredCorrectEvent);
        dropoff04.PackageDeliveredCorrectEvent.AddListener(HandlePackageDeliveredCorrectEvent);

        dropoff01.PackageDeliveredIncorrectEvent.AddListener(HandlePackageDeliveredIncorrectEvent);
        dropoff02.PackageDeliveredIncorrectEvent.AddListener(HandlePackageDeliveredIncorrectEvent);
        dropoff03.PackageDeliveredIncorrectEvent.AddListener(HandlePackageDeliveredIncorrectEvent);
        dropoff04.PackageDeliveredIncorrectEvent.AddListener(HandlePackageDeliveredIncorrectEvent);

        dropoffs[0] = dropoff01;
        dropoffs[1] = dropoff02;
        dropoffs[2] = dropoff03;
        dropoffs[3] = dropoff04;

        SwitchModes(Mode.Color);
        StartCoroutine(ModeSwitching());
    }

    IEnumerator ModeSwitching()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(this.switchTimeMin, this.switchTimeMax));
            Mode newMode = this.currentMode;
            while(newMode == this.currentMode)
            {
                newMode = (Mode) UnityEngine.Random.Range(0, Enum.GetNames(typeof(Mode)).Length);
            }
            SwitchModes(newMode);
        }
       
    }

    void SwitchModes(Mode newMode)
    {
        Debug.Log("New mode: " + newMode.ToString());
        this.currentMode = newMode;
        this.modeText.text = newMode.ToString();

        ShuffleDropoffs();
    }

    void ShuffleDropoffs()
    {
        ShuffleArray<ModeColor>(rng, valuesColors);
        ShuffleArray<ModeShape>(rng, valuesShapes);
        ShuffleArray<ModeSymbol>(rng, valuesSymbols);

        for(int i = 0; i < valuesColors.Length; i++)
        {
            Debug.Log("Updating dropoff #" + (i+1) );
            this.dropoffs[i].UpdateMode(this.currentMode, valuesColors[i], valuesShapes[i], valuesSymbols[i]);
        }
    }

    void ShuffleArray<T>(System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    void HandlePackageDeliveredCorrectEvent()
    {
        audioSourcePlop.clip = plopSounds[UnityEngine.Random.Range(0, plopSounds.Length)];
        audioSourcePlop.Play();
        score += 1;
        scoreText.text = "Score: " + score;
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    void HandlePackageDeliveredIncorrectEvent()
    {
        audioSourceErr.Play();
        lives -= 1;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            Debug.Log(PlayerPrefs.GetInt("Score"));
            SceneManager.LoadScene("EndScene");
        }
    }

    void HandlePackageDestroyedEvent()
    {
        audioSourceDestr.Play();
        lives -= 1;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            Debug.Log(PlayerPrefs.GetInt("Score"));
            SceneManager.LoadScene("EndScene");
        }
    }
}
