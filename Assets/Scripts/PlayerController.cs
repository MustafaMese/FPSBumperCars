using EZCameraShake;
using UnityEngine;

public class PlayerController : IController
{
    //[SerializeField] PlayerCamera playerCamera;
    [SerializeField] BumperCar car;
    [SerializeField] VariableJoystick joystick;

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

        car.Move(x);
        car.Turn(z);
    }
}
