using EZCameraShake;
using UnityEngine;

public class PlayerController : Controller
{
    //[SerializeField] PlayerCamera playerCamera;
    [SerializeField] BumperCar car;
    [SerializeField] VariableJoystick joystick;
    [Space]
    [SerializeField] Vector3 turnPower = new Vector3(0, 80, 0);
    [SerializeField] float rotSpeed = 100f;
    [SerializeField] float enginePower = 20;

    public override void Receive(RamDirection ramDirection)
    {
        //playerCamera.CameraShake();
        CameraShaker.Instance.ShakeOnce(2, 2, 0.1f, 0.15f);
        car.Ram(ramDirection);
    }

    private void FixedUpdate()
    {
        var x = joystick.Vertical;
        var z = joystick.Horizontal;

        car.Move(x, enginePower);
        car.Turn(z, turnPower, rotSpeed);
    }
}
