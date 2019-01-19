using UnityEngine;
using UnityEngine.UI;

public class Health : Item, IItem
{
    private bool isHealthAdded;

    public GameObject Go
    {
        get { return gameObject; }
    }

    public new void PickUp()
    {
        //UnityEngine.Debug.Log("Pick Up Object " + gameObject.name);

        //UiManager.healthValue.text = CharController.Health.ToString();

        //base.PickUp();

        if (UiManager.Inventory.childCount < 6)
        {
            if (isHealthAdded == false)
            {
                var weaponItem = Instantiate(Resources.Load("Items/UI/WeaponItem")) as GameObject;
                weaponItem.transform.SetParent(UiManager.Inventory);
                weaponItem.transform.FindAnyChild("Text").GetComponent<Text>().text = gameObject.name;
                isHealthAdded = true;
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