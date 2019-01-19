using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour
{
    public enum RewindType
    {
        Global = 0,
        Player = 1,
        Object = 2
    }

    private float counter;
    private int indexVal;

    public bool isActive;
    private bool isRewinding;
    private readonly int listLimit = 5000;
    private List<Vector3> positionVal;
    private Rigidbody rb;

    public int rewindSpeed = 2;

    public RewindType rewindType;
    private List<Vector3> rotationVal;
    private readonly int timeLimitInSeconds = 60;
    private List<Vector3> velocityVal;

    private void Awake()
    {
        positionVal = new List<Vector3>();
        rotationVal = new List<Vector3>();
        velocityVal = new List<Vector3>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (rewindType)
        {
            case RewindType.Global:
                GlobalRewind();
                break;

            case RewindType.Player:
                PlayerRewind();
                break;

            case RewindType.Object:
                ObjectRewind();
                break;
        }

        //we use a counter variable to limit the rewind mechanic usage to 10 seconds
        //if (Input.GetKey(KeyCode.E))
        if (isActive)
        {
            if (counter > 0)
            {
                counter -= Time.deltaTime;
                isRewinding = true;
                Rewind();
            }
        }
        else
        {
            if (counter < timeLimitInSeconds) counter += Time.deltaTime;
            isRewinding = false;
        }

        //if time is moving forward, keep adding new elements to the arrays
        if (!isRewinding)
        {
            positionVal.Add(transform.position);
            rotationVal.Add(transform.eulerAngles);
            if (rb != null) velocityVal.Add(rb.velocity);
            //increase the index every frame
            if (indexVal < listLimit) indexVal++;
        }

        //keep removing old data if the list size exceeds a certain value
        //this is extremely important because without this logic, the list size
        //will continue to increase which is not desirable if(indexVal>listLimit && !isRewinding)
        if (indexVal >= listLimit && !isRewinding)
        {
            positionVal.RemoveAt(0);
            rotationVal.RemoveAt(0);
            velocityVal.RemoveAt(0);
        }
    }

    //method that actually 'rewinds' the game
    private void Rewind()
    {
        print("INdex Value " + indexVal);
        //if current index is not 0
        if (indexVal > 0)
        {
            //decrease index
            // indexVal--;
            if (indexVal == 1) indexVal = 2;
            indexVal = indexVal - rewindSpeed;
            //get last data of this gameobject and apply it to the gameobject
            //remove the used data thereby decreasing the list size
            transform.position = positionVal[indexVal];
            positionVal.RemoveAt(indexVal);
            transform.eulerAngles = rotationVal[indexVal];
            rotationVal.RemoveAt(indexVal);
            rb.velocity = velocityVal[indexVal];
            velocityVal.RemoveAt(indexVal);
        }

        if (indexVal == 0 && isActive)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            isActive = false;
        }
    }

    public void PlayerRewind()
    {
    }

    public void ObjectRewind()
    {
    }

    public void GlobalRewind()
    {
    }
}