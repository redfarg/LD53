using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;

    [SerializeField] Package packagePrefab;

    [SerializeField] ModeMappings modeMappings;

    [SerializeField] float spawnTimeMin;
    [SerializeField] float spawnTimeMax;

    [SerializeField] Dropoff dropoff01;
    [SerializeField] Dropoff dropoff02;
    [SerializeField] Dropoff dropoff03;
    [SerializeField] Dropoff dropoff04;

    [SerializeField] GameObject distractionPoint1;
    [SerializeField] GameObject distractionPoint2;
    [SerializeField] GameObject distractionPoint3;

    GameObject[] distractionPoints;

    [SerializeField] Distraction distractionPrefab;

    private Dictionary<GameObject, (Dropoff, Dropoff)> distractedDropoffs;

    private bool spawnerActive = false;
    private bool distractionsActive = false;

    System.Random random = new System.Random();

    readonly Array valuesColors = Enum.GetValues(typeof(ModeColor));
    readonly Array valuesShapes = Enum.GetValues(typeof(ModeShape));
    readonly Array valuesSymbols = Enum.GetValues(typeof(ModeSymbol));

    public AudioClip[] buhSounds;
    public AudioSource audioSourceBuh;

    void Start()
    {
        distractionPoints = new GameObject[] {distractionPoint1, distractionPoint2, distractionPoint3 };
        distractedDropoffs = new Dictionary<GameObject, (Dropoff, Dropoff)>()
            {
                { distractionPoint1, (dropoff01, dropoff02) },
                { distractionPoint2, (dropoff02, dropoff03) },
                { distractionPoint3, (dropoff03, dropoff04) }
            };

        StartCoroutine(PackageSpawning());
        StartCoroutine(TimerEscalation());
        StartCoroutine(DistractionCountdown());
        StartCoroutine(DistractionSpawning());
        this.spawnerActive = true;
    }

    IEnumerator PackageSpawning()
    {
        while (true)
        {
            if (this.spawnerActive)
            {


                ModeColor randomColor = (ModeColor)valuesColors.GetValue(random.Next(valuesColors.Length));
                ModeShape randomShape = (ModeShape)valuesShapes.GetValue(random.Next(valuesShapes.Length));
                ModeSymbol randomSymbol = (ModeSymbol)valuesSymbols.GetValue(random.Next(valuesSymbols.Length));

                Package spawnedPackage = Instantiate(this.packagePrefab, this.spawnPoint.transform.position, Quaternion.identity);
                spawnedPackage.Init(randomColor, randomShape, randomSymbol);

                spawnedPackage.GetComponent<SpriteRenderer>().sprite = modeMappings.colorToSprite[randomColor];
                spawnedPackage.transform.Find("Symbol").GetComponent<SpriteRenderer>().sprite = modeMappings.symbolToSprite[randomSymbol];
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(this.spawnTimeMin, this.spawnTimeMax));
        }
    }

    IEnumerator TimerEscalation()
    {
        while (this.spawnTimeMin >= 0.25f) 
        {
            yield return new WaitForSeconds(10);
            this.spawnTimeMin -= 0.1f;
            this.spawnTimeMax -= 0.1f;
        }
    }

    IEnumerator DistractionCountdown()
    {
        yield return new WaitForSeconds(20);

        this.distractionsActive = true;
    }

    IEnumerator DistractionSpawning()
    {
        while (true)
        {
            if (this.distractionsActive)
            {
                GameObject designatedSpawnPoint = distractionPoints[UnityEngine.Random.Range(0, distractionPoints.Length)];
                Distraction distraction = Instantiate(distractionPrefab, designatedSpawnPoint.transform.position, Quaternion.identity);
                distractedDropoffs[designatedSpawnPoint].Item1.Distract(distraction);
                distractedDropoffs[designatedSpawnPoint].Item2.Distract(distraction);
                audioSourceBuh.clip = buhSounds[UnityEngine.Random.Range(0, buhSounds.Length)];
                audioSourceBuh.Play();

            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(4, 10));

        }
    }

}
