using UnityEngine;

public class ShowHideObject : MonoBehaviour
{
    public bool HiddenOnStart = false;
    public float ShrinkTime = 1f;
    public float GrowTime = 1f;
    public bool DisablePhysicsDuringChange = true;

    private Transform t;
    private Vector3 originalScale;
    private bool currentlyChanging = false;
    private bool showing;
    private float changeStartTime;
    private float changeTime;
    private Vector3 fromScale;
    private Vector3 toScale;
    private Renderer[] renderers;
    private Collider[] colliders;

    void Start()
    {
        t = transform;

        originalScale = t.localScale;

        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();

        if (HiddenOnStart)
        {
            SetPhysics(false);
            SetRenderers(false);
            t.localScale = Vector3.zero;
        }
    }

    void Update()
    {
        if (currentlyChanging)
        {
            t.localScale = Vector3.Lerp(fromScale, toScale, (Time.time - changeStartTime) / changeTime);

            if ((Time.time - changeStartTime) >= changeTime)
            {
                t.localScale = toScale;

                if (showing)
                {
                    if (DisablePhysicsDuringChange)
                    {
                        SetPhysics(true);
                    }
                }
                else
                {
                    SetRenderers(false);
                }
            }
        }
    }

    public void ShowObject()
    {
        if (DisablePhysicsDuringChange)
        {
            SetPhysics(false);
        }
        SetRenderers(true);

        showing = true;
        fromScale = t.localScale;
        toScale = originalScale;
        changeStartTime = Time.time;
        changeTime = GrowTime;
        currentlyChanging = true;
    }

    public void HideObject()
    {
        if (DisablePhysicsDuringChange)
        {
            SetPhysics(false);
        }

        showing = false;
        fromScale = t.localScale;
        toScale = Vector3.zero;
        changeStartTime = Time.time;
        changeTime = ShrinkTime;
        currentlyChanging = true;
    }

    private void SetRenderers(bool enable)
    {
        int length = renderers.Length;
        for (int i = 0; i < length; i++)
        {
            renderers[i].enabled = enable;
        }
    }

    private void SetPhysics(bool enable)
    {
        int length = colliders.Length;
        for (int i = 0; i < length; i++)
        {
            colliders[i].enabled = enable;
        }
    }
}
