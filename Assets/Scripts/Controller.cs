using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    [SerializeField] ControllerType controllerType;

    public abstract void Receive(RamDirection ramDir);
    public ControllerType GetControllerType() { return controllerType; }
}
