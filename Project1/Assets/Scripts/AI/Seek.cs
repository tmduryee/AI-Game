using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour
{

    public int seekRadius = 0;
    public int seekSpeed = 0;

    // Use this for initialization
    void Start()
    {

    }

    public void Execute(GameObject toSeek)
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = toSeek.transform.position;
        Vector3 direction = targetPos - myPos;

        transform.Translate(direction.normalized * seekSpeed / 10);
    }
}
