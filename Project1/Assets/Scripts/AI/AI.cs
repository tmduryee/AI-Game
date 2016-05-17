using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{

    public float seekSpeed              = 18.0f;
    public float fleeSpeed              = 20.0f;
    public float wanderSpeed            = 14.0f;

    public float seekRadius             = 30.0f;
    public float seekBoundaryRadius     = 5.0f;
    public float fleeRadius             = 50.0f;

    public void Seek(GameObject target)
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        transform.LookAt(new Vector3(targetPos.x, myPos.y, targetPos.z));
        transform.Translate(new Vector3(0, 0, 1) * (seekSpeed * Time.deltaTime));
    }

    public void Flee(GameObject target)
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        transform.LookAt(new Vector3(targetPos.x, myPos.y, targetPos.z));
		transform.Translate(new Vector3(0, 0, -1) * (fleeSpeed * Time.deltaTime));
        transform.Rotate(new Vector3(0, 1, 0), 180);
    }

    public void Wander(Vector3 lastFramePos)
    {
        transform.Rotate(Vector3.up, Random.Range(-10.0f, 10.0f));
		transform.Translate(new Vector3(0, 0, 1) * (wanderSpeed * Time.deltaTime));

        if (Vector3.Distance(transform.position, lastFramePos) < 0.05f)
        {
            transform.Rotate(new Vector3(0, 1, 0), 180);
        }
    }
}
