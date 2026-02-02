using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<CarController> cars = new List<CarController>();


    public float posUpdateRate = 0.05f;
    private float lastPosUpdateTime;



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
