using UnityEngine;

public class TriggerReceiver : MonoBehaviour
{
    [SerializeField] IController controller;
    [SerializeField] RamDirection ramDirection;

    private void OnTriggerEnter(Collider other) 
    {
        controller.Receive(ramDirection);
    }
}
