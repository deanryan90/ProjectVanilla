using UnityEngine;

public interface IItem
{
    GameObject Go { get; }

    void PickUp();
}