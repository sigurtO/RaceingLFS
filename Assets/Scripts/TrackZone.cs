using UnityEngine;

public class TrackZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CarController car = other.GetComponent<CarController>();
            car.currentTrackZone = this;
            car.zonesPassed ++;
        }
    }

}
