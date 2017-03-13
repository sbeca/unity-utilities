using UnityEngine;
using UnityEngine.UI;

public class RawImageScroller : MonoBehaviour
{
    public float SpeedX = 0f;
    public float SpeedY = 0f;
    public bool UseRealtime = false;

    private RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
    }

    void Update()
    {
        if (UseRealtime)
        {
            image.uvRect = new Rect(image.uvRect.x + SpeedX * Time.unscaledDeltaTime, image.uvRect.y + SpeedY * Time.unscaledDeltaTime, image.uvRect.width, image.uvRect.height);
        }
        else
        {
            image.uvRect = new Rect(image.uvRect.x + SpeedX * Time.deltaTime, image.uvRect.y + SpeedY * Time.deltaTime, image.uvRect.width, image.uvRect.height);
        }
    }
}
