using UnityEngine;

public class SetCameraObliqueness : MonoBehaviour
{
    public Camera CameraToModify;
    [Range(-1f, 1f)]
    public float HorizontalObliqueness = 0f;
    [Range(-1f, 1f)]
    public float VerticalObliqueness = 0f;
    public bool SetOnStart = true;

    void Start()
    {
        SetObliqueness();
    }

    public void SetObliqueness()
    {
        Matrix4x4 mat = CameraToModify.projectionMatrix;
        mat[0, 2] = HorizontalObliqueness;
        mat[1, 2] = VerticalObliqueness;
        CameraToModify.projectionMatrix = mat;
    }
}
