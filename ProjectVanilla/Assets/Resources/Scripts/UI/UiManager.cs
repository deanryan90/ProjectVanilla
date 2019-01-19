using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static Transform Canvas;
    public static Text healthValue;

    public static Text timeTravelValue;

    public static Transform Inventory;

    // Use this for initialization

    private void Awake()
    {
        Canvas = GameObject.Find("Canvas").transform;
        Inventory = Canvas.FindAnyChild("Inventory").transform;
        healthValue = Canvas.FindAnyChild("Health_Value").GetComponent<Text>();
        timeTravelValue = Canvas.FindAnyChild("Time_Travel_Value").GetComponent<Text>();

        healthValue.text = CharController.Health.ToString();
        timeTravelValue.text = CharController.TimeTravelJuice.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}