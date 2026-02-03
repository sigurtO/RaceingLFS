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

    public TrackZone currentTrackZone;
    public int zonesPassed;
    public int racePos;
    public int curLap;

    public bool canControl;

    public Rigidbody rb;


    private void Start()
    {
        startModelOffset = carModel.transform.localPosition;
        curYRotate = carModel.transform.eulerAngles.y;

        GameManager.instance.cars.Add(this);
        transform.position = GameManager.instance.spawnPoints[GameManager.instance.cars.Count - 1].position;
    }
    private void Update()
    {
        if(!canControl)
        {
            return;
        }
        float turnRate = Vector3.Dot(rb.linearVelocity.normalized, carModel.forward); // get number between -1 and 1 based on car moving forward or backward
        turnRate = Mathf.Abs(turnRate); // make it always positive


        curYRotate += turnInput * turnSpeed * turnRate * Time.deltaTime;
        carModel.transform.position = transform.position + startModelOffset; //setting car model position
        CheckGround();
        
    }

    private void FixedUpdate()
    {
        if (!canControl)
        {
            turnInput = 0.0f;
        }
        if (accelerateInput)
        {
            rb.AddForce(carModel.forward * acceleration, ForceMode.Acceleration);
        }
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, -0.7f, 0), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            carModel.up = hit.normal;
        }
        else
        {
            carModel.up = Vector3.up;
        }

        carModel.Rotate(new Vector3(0, curYRotate, 0), Space.Self);
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
