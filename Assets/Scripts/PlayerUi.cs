using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerUi : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public CarController playerCar;

    public TextMeshProUGUI countDownText;

    void Update()
    {
        rankText.text = "Position: " + playerCar.racePos.ToString() + "/" + GameManager.instance.cars.Count.ToString();
    }

    public void StartCountDownDisplay()
    {
        StartCoroutine(CountDown());
        IEnumerator CountDown()
        {
            countDownText.gameObject.SetActive(true);
            countDownText.text = "3";
            yield return new WaitForSeconds(1.0f);
            countDownText.text = "2";
            yield return new WaitForSeconds(1.0f);
            countDownText.text = "1";
            yield return new WaitForSeconds(1.0f);
            countDownText.text = "GO!";
            yield return new WaitForSeconds(1.0f);
            countDownText.gameObject.SetActive(false);
        }
    }
}
