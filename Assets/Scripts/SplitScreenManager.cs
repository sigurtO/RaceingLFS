using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreenManager : MonoBehaviour
{
    public GameObject carPrefab;

    private void Start()
    {
        var keyboard = Keyboard.current;

        if (keyboard != null)
        {
            PlayerInput p1 = PlayerInput.Instantiate(carPrefab, controlScheme: "Keyboard", pairWithDevice: keyboard);


            PlayerInput p2 = PlayerInput.Instantiate(carPrefab, controlScheme: "ArrowKeys", pairWithDevice: keyboard);
        }
    }
}
