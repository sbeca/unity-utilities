using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{
    public bool IsGlobalInstance = false;
    public float FadeInTime = 0.5f;
    public float FadeOutTime = 0.5f;
    public bool UseRealtime = true;
    public Image FadeSprite;

    private System.Action callback;
    private bool fading;
    private Coroutine updateCoroutine = null;
    private float startAlpha;
    private float endAlpha;
    private float fadeDuration;

    public static Fader GlobalInstance { get; private set; }

    void Awake()
    {
        if (IsGlobalInstance)
        {
            if (GlobalInstance != null && GlobalInstance != this)
            {
                Debug.LogError("[Fader] More than one Fader has been marked as the Global Instance");
            }
            GlobalInstance = this;
        }
    }

    void OnDestroy()
    {
        if (IsGlobalInstance && GlobalInstance == this)
        {
            GlobalInstance = null;
        }
    }

    public void FadeToOpacity(float opacity, float duration, System.Action callback)
    {
        if (opacity != FadeSprite.color.a)
        {
            startAlpha = FadeSprite.color.a;
            endAlpha = opacity;
            fadeDuration = duration;
            this.callback = callback;
            fading = true;
            if (updateCoroutine != null) StopCoroutine(updateCoroutine);
            updateCoroutine = StartCoroutine(UpdateCoroutine());
        }
        else
        {
            fading = false;
            if (callback != null) callback();
        }
    }

    public void StartFadeIn(System.Action callback)
    {
        FadeToOpacity(0f, FadeInTime, callback);
    }

    public void StartFadeOut(System.Action callback)
    {
        FadeToOpacity(1f, FadeOutTime, callback);
    }

    public void FadeInImmediately()
    {
        FadeSprite.color = new Color(FadeSprite.color.r, FadeSprite.color.g, FadeSprite.color.b, 0f);
        fading = false;
    }

    public void FadeOutImmediately()
    {
        FadeSprite.color = new Color(FadeSprite.color.r, FadeSprite.color.g, FadeSprite.color.b, 1f);
        fading = false;
    }

    // Using a Coroutine here instead of normal Update() because Update() doesn't work when Time.timeScale == 0 but Coroutines do
    private IEnumerator UpdateCoroutine()
    {
        float startTime = UseRealtime ? Time.realtimeSinceStartup : Time.time;

        while (fading)
        {
            float currentTime = UseRealtime ? Time.realtimeSinceStartup : Time.time;

            if ((currentTime - startTime) > fadeDuration)
            {
                fading = false;
                FadeSprite.color = new Color(FadeSprite.color.r, FadeSprite.color.g, FadeSprite.color.b, endAlpha);
                if (callback != null) callback();
                yield break;
            }
            else
            {
                float alpha = Mathf.Lerp(startAlpha, endAlpha, (currentTime - startTime) / fadeDuration);
                FadeSprite.color = new Color(FadeSprite.color.r, FadeSprite.color.g, FadeSprite.color.b, alpha);
            }

            yield return null;
        }
    }
}
