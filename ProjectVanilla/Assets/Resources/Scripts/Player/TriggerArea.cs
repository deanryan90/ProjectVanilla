using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("enter area " + other.name);
        if (other.name != "ground")
            if (!CharController.TriggerObjects.Contains(other.transform))
                CharController.TriggerObjects.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        //print("exit area " + other.name);
        if (other.name != "ground")
            if (CharController.TriggerObjects.Contains(other.transform))
                CharController.TriggerObjects.Remove(other.transform);
    }
}