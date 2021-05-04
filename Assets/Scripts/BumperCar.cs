using UnityEngine;

public class BumperCar : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform wheel;
    [SerializeField] ControllerType controllerType;
    [Space]
    [SerializeField] float ramPower;

    private bool canMove;
    
    private void Start() 
    {
        canMove = true;
    }

    // TODO Geri gitmiyo.
    public void Move(float xAxis, float enginePower)
    {
        if(!canMove) return;

        if(xAxis > 0)
            rb.AddForce(transform.forward * xAxis * enginePower, ForceMode.Force);
        else if(xAxis < 0)
            rb.AddForce(transform.forward * xAxis * (2 * enginePower / 3), ForceMode.Force);
    }

    public void Turn(float zAxis, Vector3 turnPower, float rotSpeed)
    {
        Quaternion deltaRotation = Quaternion.Euler(zAxis * turnPower * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        WheelControl(zAxis, rotSpeed);
    }

    public bool Move(Vector3 targetPos, float movementSpeed, float approachDistance)
    {
        var direction = (targetPos - transform.position).normalized;
        rb.MovePosition(rb.position + transform.forward * movementSpeed * Time.deltaTime);
        return Vector3.Distance(transform.position, targetPos) <= approachDistance;
    }

    public bool Turn(Vector3 targetPos, float rotationSpeed)
    {
        var direction = (targetPos - transform.position).normalized;
        var rotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, rotationSpeed));
        return Quaternion.Angle(rotation, transform.rotation) <= 0.1f;
    }

    public void Ram(RamDirection ramDirection)
    {
        switch (ramDirection)
        {
            case RamDirection.FRONT:
                rb.AddForce(-transform.forward * ramPower, ForceMode.Force);
                break;
            case RamDirection.BACK:
                rb.AddForce(transform.forward * ramPower, ForceMode.Force);
                break;
            case RamDirection.RIGHT:
                rb.AddForce(-transform.right * ramPower, ForceMode.Force);
                break;
            case RamDirection.LEFT:
                rb.AddForce(transform.right * ramPower, ForceMode.Force);
                break;
        }
    }

    private static Vector3 GetDirection(Vector3 pos, Vector3 targetPos)
    {
        var heading = targetPos - pos;
        var distance = heading.magnitude;
        var direction = heading / distance;
        return direction;
    }

    private void WheelControl(float zAxis, float rotSpeed)
    {
        if(zAxis != 0)
        {
            var angle = wheel.eulerAngles;
            angle.z -= zAxis * Time.deltaTime * rotSpeed;
            angle.z = Mathf.Clamp(angle.z, 30, 70);
            wheel.eulerAngles = angle;
        }
        else 
        {
            var angle = wheel.eulerAngles;
            angle.z = Mathf.Lerp(angle.z, 50, Time.deltaTime * rotSpeed / 4);
            wheel.eulerAngles = angle;
        } 
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Dedector")
        {
            if(controllerType == ControllerType.PLAYER)
                UIManager.Instance.OpenEndCanvas(true);
            else
            {
                gameObject.SetActive(false);
                UIManager.Instance.UpdateScore();
            }
               
        }    
    }
}
