using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    public bool isRewind;

    //private List<Vector3> positions;
    private List<PointInTime> pointsInTime;

    private Rigidbody rb;

    public float RecordTime = 10f;

    // Use this for initialization
    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        Debug.Log("Time Travel OK");
    }

    private void StartRewind()
    {
        isRewind = true;
        rb.isKinematic = true;
        Camera.main.GetComponent<TimeManagement>().timeScale = 10;
    }

    private void StopRewind()
    {
        isRewind = false;
        rb.isKinematic = false;
        Camera.main.GetComponent<TimeManagement>().timeScale = 1;
    }

    private void FixedUpdate()
    {
        if (isRewind)
            Rewind();
        else
            Record();
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            var pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(RecordTime / Time.fixedDeltaTime)) pointsInTime.RemoveAt(pointsInTime.Count - 1);
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) StartRewind();
        if (Input.GetKeyUp(KeyCode.R)) StopRewind();
    }
}