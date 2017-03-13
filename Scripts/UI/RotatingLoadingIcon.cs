using UnityEngine;

public class RotatingLoadingIcon : MonoBehaviour
{
    public float DegreesToRotate = 1f;
    public float TimeToRotate = 5f;

    private Transform t;
    private float timePerSubRotation;
    private float lastRotationTime;

    void Start()
    {
        t = transform;
        timePerSubRotation = (Mathf.Abs(DegreesToRotate) / 360f) * TimeToRotate;
        lastRotationTime = Time.time;
    }

    void Update()
    {
        if ((Time.time - lastRotationTime) >= timePerSubRotation)
        {
            t.localRotation = Quaternion.Euler(t.localRotation.eulerAngles.x, t.localRotation.eulerAngles.y, t.localRotation.eulerAngles.z + DegreesToRotate);
            lastRotationTime = Time.time;
        }
    }
}
