using UnityEngine;

public class Item : MonoBehaviour
{
    public void PickUp()
    {
        Debug.Log("Pick Up Object " + gameObject.name);
        Destroy(gameObject);
    }
}