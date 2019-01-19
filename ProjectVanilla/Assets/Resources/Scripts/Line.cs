using UnityEngine;

public class Line : MonoBehaviour
{
    public Transform objectToRewind;
    private Vector3 offset;

    // Use this for initialization
    private void Start()
    {
        offset = transform.position - objectToRewind.transform.position;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        var rewindTime = other.GetComponent<RewindTime>();
        rewindTime.isActive = true;
        other.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        print("Hello ");
    }

    private void LateUpdate()
    {
        //transform.position = objectToRewind.transform.position + offset;
    }

    private void Update()
    {
        //Vector3 direction = objectToRewind.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(direction);
        //transform.rotation = rotation;
    }
}