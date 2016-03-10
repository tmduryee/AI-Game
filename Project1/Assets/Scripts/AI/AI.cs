using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    public float seekSpeed   = 2.0f;
    public float fleeSpeed   = 8.0f;
    public float wanderSpeed = 5.0f;
    
    public string toFlee = "";

    public float seekRadius = 15.0f;
    public float seekBoundaryRadius = 5.0f;
    public float fleeRadius = 100.0f;

    Vector3 wanderDirection = new Vector3();

    public void Seek(GameObject target)
    {

        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        Vector3 direction = targetPos - myPos;
        direction.y = 0;
        transform.LookAt(new Vector3(targetPos.x, 0, targetPos.z));
        transform.Translate(new Vector3(0, 0, 1) * seekSpeed / 10);
    }

	public void Flee(GameObject target)
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        Vector3 direction = myPos - targetPos;
        direction.y = 0;
        transform.LookAt(-new Vector3(targetPos.x, 0, targetPos.z));
        transform.Translate(new Vector3(0, 0, 1) * fleeSpeed / 10);
    }

    public void Wander()
    {
        wanderDirection.x += Random.Range(-0.3f, 0.3f);
        wanderDirection.z += Random.Range(-0.3f, 0.3f);
        wanderDirection.Normalize();
        transform.LookAt(-new Vector3(wanderDirection.x, 0, wanderDirection.z));
        transform.Translate(new Vector3(0, 0, 1) * wanderSpeed / 10);
    }
}
