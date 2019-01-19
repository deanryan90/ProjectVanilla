using UnityEngine;

public class TimeManagement : MonoBehaviour
{
    public float timeScale = 1;

    // Use this for initialization
    private void Start()
    {
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    private void Update()
    {
        Time.timeScale = timeScale;
    }
}