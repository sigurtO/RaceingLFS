using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<CarController> cars = new List<CarController>();

    public Transform[] spawnPoints;


    public float posUpdateRate = 0.05f;
    private float lastPosUpdateTime;

    public int playersToBegin = 2;
    public bool gameStarted = false;


    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Time.time - lastPosUpdateTime > posUpdateRate)
        {
            lastPosUpdateTime = Time.time;
            UpdateCarRacePos();
        }

        if (!gameStarted && cars.Count == playersToBegin)
        {
            gameStarted = true;
            StartCountDown();
        }

    }

    void StartCountDown()
    {
        PlayerUi[] uis = FindObjectsOfType<PlayerUi>();
        foreach (PlayerUi ui in uis)
        {
            ui.StartCountDownDisplay();
        }
        Invoke("StartRace", 3.0f);
    }

    void StartRace()
    {
        foreach (CarController car in cars)
        {
            car.canControl = true;
        }
    }
    void UpdateCarRacePos()
    {
        cars.Sort(SortPos);

        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].racePos = cars.Count - i;
        }
    }

    int SortPos(CarController a, CarController b)
    {
        if (a.zonesPassed > b.zonesPassed)
        {
            return -1;
        }
        else if (a.zonesPassed < b.zonesPassed)
        {
            return 1;
        }
        else
        {
            //same zones passed, compare distance to next zone
            float aDist = Vector3.Distance(a.transform.position, a.currentTrackZone.transform.position);
            float bDist = Vector3.Distance(b.transform.position, b.currentTrackZone.transform.position);
            return aDist > bDist ? 1 : -1;
        }
    }
}
