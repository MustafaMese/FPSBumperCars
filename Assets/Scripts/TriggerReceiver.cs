using UnityEngine;

public class TriggerReceiver : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] RamDirection ramDirection;

    private void OnTriggerEnter(Collider other) 
    {
        controller.Receive(ramDirection);
    }
}
