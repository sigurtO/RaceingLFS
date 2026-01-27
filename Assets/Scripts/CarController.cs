using UnityEngine;
using UnityEngine.InputSystem;
public class CarController : MonoBehaviour
{
    public float acceleration;
    public float turnSpeed;

    public Transform carModel;

    private Vector3 startModelOffset;

    public float groundCheckRate;
    private float lastGroundCheckTime;
    private float curYRotate;

    private bool accelerateInput;
    private float turnInput;


    public Rigidbody rb;


    private void Start()
    {
        startModelOffset = carModel.transform.localPosition;
        curYRotate = carModel.transform.eulerAngles.y;
    }
    private void Update()
    {
        float turnRate = Vector3.Dot(rb.linearVelocity.normalized, carModel.forward); // get number between -1 and 1 based on car moving forward or backward
        turnRate = Mathf.Abs(turnRate); // make it always positive


        curYRotate += turnInput * turnSpeed * turnRate * Time.deltaTime;
        carModel.transform.position = transform.position + startModelOffset; //setting car model position
       carModel.transform.eulerAngles = new Vector3(0, curYRotate, 0); //dont rotate model with car but keep it static

        
    }

    private void FixedUpdate()
    {
        if (accelerateInput)
        {
            rb.AddForce(carModel.forward * acceleration, ForceMode.Acceleration);
        }
    }
    public void OnAccelerateInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            accelerateInput = true;
        }
        else
        {
            accelerateInput = false;
        }
    }
    public void OnTurnInput(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }

}
