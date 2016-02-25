using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{

    public int wanderSpeed;
    Vector3 direction;

    // Use this for initialization
    void Start()
    {
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
        direction.Normalize();
    }

    public void Execute()
    {
        direction.x += Random.Range(-0.3f, 0.3f);
        direction.z += Random.Range(-0.3f, 0.3f);
        direction.Normalize();
        transform.Translate(direction * wanderSpeed / 10);
    }
}
