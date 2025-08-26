using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public float lifetime = 5f;                 // Lifetime of a point
    public float minimumVertexDistance = 0.1f;  // Distance moved before adding a point
    public Vector3 velocity;                    // Direction the trail drifts

    private LineRenderer line;
    private List<Vector3> points;
    private Queue<float> spawnTimes = new Queue<float>();

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;

        // index 0 = transform.position
        points = new List<Vector3>() { transform.position };
        line.positionCount = 1;
        line.SetPosition(0, transform.position);
    }

    void AddPoint(Vector3 position)
    {
        points.Insert(1, position);  // insert after transform
        spawnTimes.Enqueue(Time.time);
    }

    void RemovePoint()
    {
        if (spawnTimes.Count > 0)
            spawnTimes.Dequeue();

        if (points.Count > 1)
            points.RemoveAt(points.Count - 1);
    }

    void Update()
    {
        // remove expired points
        while (spawnTimes.Count > 0 && spawnTimes.Peek() + lifetime < Time.time)
        {
            RemovePoint();
        }

        // move old points backwards
        Vector3 diff = -velocity * Time.deltaTime;
        for (int i = 1; i < points.Count; i++)
        {
            points[i] += diff;
        }

        // add a new solidified point if moved enough
        if (points.Count < 2 || Vector3.Distance(transform.position, points[1]) > minimumVertexDistance)
        {
            AddPoint(transform.position);
        }

        // update current position
        points[0] = transform.position;

        // update LineRenderer
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
}
