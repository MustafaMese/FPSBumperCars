using System.Collections;
using UnityEngine;

public class AIController : IController
{
    [SerializeField] LayerMask mask;
    [SerializeField] BumperCar car;
    [SerializeField, Range(0, 1)] float rotationSpeed = 10f;
    [SerializeField] float movementSpeed;
    [SerializeField] float approachDistance;
    [SerializeField] float idleTime;

    private void Start() 
    {
        StartCoroutine(Idle());
    }

    public void FindTarget()
    {
        var colliders = Physics.OverlapSphere(transform.position, 30, mask);

        if(colliders.Length > 1)
        {
            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    StartCoroutine(Turn(colliders[i].transform.position));
                    break;
                }
            }
        }
        else
            StartCoroutine(Idle());
    }

    private IEnumerator Turn(Vector3 targetPos)
    {
        bool completed = false;
        while(!completed)
        {
            completed = car.Turn(targetPos, rotationSpeed);
            yield return null;
        }

        StartCoroutine(Move(targetPos));
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        bool completed = false;
        while (!completed)
        {
            completed = car.Move(targetPos, movementSpeed, approachDistance);
            if(!RotationCheck(targetPos))
                break;
            yield return null;
        }

        StartCoroutine(Idle());
    }

    private IEnumerator Idle()
    {
        yield return new WaitForSeconds(idleTime);
        FindTarget();
    }

    private bool RotationCheck(Vector3 targetPos)
    {
        var direction = (targetPos - transform.position).normalized;
        var rotation = Quaternion.LookRotation(direction);
        return Quaternion.Angle(rotation, transform.rotation) <= 1f;
    }

    public override void Receive(RamDirection ramDirection)
    {
        car.Ram(ramDirection);
    }
}
