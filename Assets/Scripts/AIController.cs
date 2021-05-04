using System.Collections;
using UnityEngine;

public class AIController : Controller
{
    [SerializeField] LayerMask mask;
    [SerializeField] BumperCar car;
    [Space]
    [SerializeField, Range(0, 1)] float rotationSpeed = 10f;
    [SerializeField] float movementSpeed;
    [SerializeField] float approachDistance;
    [SerializeField] float idleTime;

    private Transform target;

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
                    int index = GetRandomTarget(colliders);
                    target = colliders[index].transform;
                    StartCoroutine(Turn(target.position));
                    break;
                }
            }
        }
        else
            StartCoroutine(Idle());
    }

    private int GetRandomTarget(Collider[] colliders)
    {
        int number = Random.Range(0, colliders.Length);
        return number;
    }

    private IEnumerator Turn(Vector3 targetPos)
    {
        bool completed = false;
        while(!completed)
        {
            if(DistanceCheck())
                break;
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

    private bool DistanceCheck()
    {
        return Vector3.Distance(target.position, transform.position) > 30f;
    }

    public override void Receive(RamDirection ramDirection)
    {
        car.Ram(ramDirection);
    }
}
