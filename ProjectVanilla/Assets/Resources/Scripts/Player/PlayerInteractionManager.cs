using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    private static IItem _iItem;

    public IItem IItem
    {
        get { return _iItem; }
        set { _iItem = value; }
    }

    private void OnTriggerEnter(Collider col)
    {
        _iItem = col.GetComponent<IItem>();
        if (_iItem != null) _iItem.PickUp();
    }
}