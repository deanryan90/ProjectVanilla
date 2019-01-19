using UnityEngine;

public class RealTime : MonoBehaviour
{
    private float fps;
    private int frames;

    private double lastInterval;

    // Use this for initialization
    public float updateInterval = 0.5F;

    private void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    private void Update()
    {
        ++frames;
        var timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float) (frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }
    }
}