using UnityEngine;
using System.Collections;

public class Node
{
    public Vector3 position;
    public Node next;
    public Node previous;

    private float waypointRadius;

    public Node(Vector3 p)
    {
        position = p;
        next = null;
        previous = null;
    }
}

 public class Path
{
    public Node current;
    public Node initial;
    public Node final;

    public Path()
    {
        current = null;
        initial = null;
        final = null;
    }

    public void AddNode(Node toAdd)
    {
        if(initial == null)
        {
            initial = toAdd;
            current = initial;
        }
        else
        {
            Node start = initial;

            while(initial.next != null)
            {
                initial = initial.next;
            }

            initial.next = toAdd;
            initial.next.previous = initial;
            final = initial.next;

            initial = start;
        }
    }

    public void Follow(bool next, Transform transform, float speed)
    {
        transform.LookAt(new Vector3(current.position.x, transform.position.y, current.position.z));
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, current.position) < 3.0f)
        {
            if (next && current.next != null)
                current = current.next;
            else if(current.previous != null)
                current = current.previous;
        }
    }

    public void FindClosest(Vector3 pos)
    {
        Node start = initial;
        Node closest = initial;
        float distanceToNode = 0.0f;

        while(initial.next != null)
        {
            initial = initial.next;
            distanceToNode = Vector3.Distance(pos, initial.position);

            if (distanceToNode < Vector3.Distance(pos, closest.position))
            {
                closest = initial;
            }
        }
        
        initial = start;
        current = closest;
    }
}

public class PathFollow
{
    private Path path;
    private Transform transform;

	public PathFollow(Transform t)
    {
        path = new Path();
        transform = t;
	}

    public void AddWaypoint(Node toAdd)
    {
        path.AddNode(toAdd);
    }

    // Next == true --> go to next node
    // Next == false --> go to previous node
    public void Follow(bool next, float speed, bool findClosest = false)
    {
        if(findClosest)
        {
            path.FindClosest(transform.position);
        }

        path.Follow(next, transform, speed);
    }
}
