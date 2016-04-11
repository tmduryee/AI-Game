using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{

    public float seekSpeed              = 5.0f;
    public float fleeSpeed              = 8.0f;
    public float wanderSpeed            = 3.0f;

    public float seekRadius             = 15.0f;
    public float seekBoundaryRadius     = 5.0f;
    public float fleeRadius             = 100.0f;

    public void Seek(GameObject target)
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        transform.LookAt(new Vector3(targetPos.x, myPos.y, targetPos.z));
        transform.Translate(new Vector3(0, 0, 1) * seekSpeed / 10);
    }

    public void Flee(GameObject target)
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        transform.LookAt(new Vector3(targetPos.x, myPos.y, targetPos.z));
        transform.Translate(new Vector3(0, 0, -1) * fleeSpeed / 10);
        transform.Rotate(new Vector3(0, 1, 0), 180);
    }

    public void Wander(Vector3 lastFramePos)
    {
        transform.Rotate(Vector3.up, Random.Range(-20.0f, 20.0f));
        transform.Translate(new Vector3(0, 0, 1) * wanderSpeed / 10);

        if (Vector3.Distance(transform.position, lastFramePos) < 0.5f)
        {
            transform.Rotate(new Vector3(0, 1, 0), 180);
        }
    }
}
