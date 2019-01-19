using UnityEngine;
using UnityEngine.UI;

public class Weapon : Item, IItem
{
    private bool isWeaponAdded;

    public GameObject Go
    {
        get { return gameObject; }
    }

    public new void PickUp()
    {
        Debug.Log("Pick Up Object " + gameObject.name);

        if (UiManager.Inventory.childCount < 6)
        {
            if (isWeaponAdded == false)
            {
                var weaponItem = Instantiate(Resources.Load("Items/UI/WeaponItem")) as GameObject;
                weaponItem.transform.SetParent(UiManager.Inventory);
                weaponItem.transform.FindAnyChild("Text").GetComponent<Text>().text = gameObject.name;
                isWeaponAdded = true;
                base.PickUp();
            }

            Debug.Log("Inventory Count " + UiManager.Inventory.childCount);
        }
        else
        {
            Debug.Log("Inventory No Room ");
        }
    }
}