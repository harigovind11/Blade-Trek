using UnityEngine;

public class AimLineController : MonoBehaviour
{
    public Transform knifeTransform;
    public float lineLength = 3f;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (knifeTransform == null) return;

        Vector3 start = knifeTransform.position;
        Vector3 direction = knifeTransform.up; // Knife points up in your setup
        Vector3 end = start + direction * lineLength;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}