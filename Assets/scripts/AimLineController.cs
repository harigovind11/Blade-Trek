// Remove the singleton logic
// public static AimLineController instance;

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimLineController : MonoBehaviour
{
    public Transform knifeTransform;
    public float maxDistance = 10f;
    public int maxBounces = 3;
    public LayerMask bounceMask;

    private LineRenderer lineRenderer;
    private List<Vector2> reflectionPoints = new List<Vector2>();

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (knifeTransform == null)
        {
            lineRenderer.enabled = false;
            return;
        }

        DrawAimLine();
    }

    void DrawAimLine()
    {
        reflectionPoints.Clear();

        Vector2 origin = knifeTransform.position;
        Vector2 direction = knifeTransform.up.normalized;

        reflectionPoints.Add(origin);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, origin);

        float remainingLength = maxDistance;
        int pointIndex = 1;

        while (pointIndex < maxBounces + 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, remainingLength, bounceMask);

            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;
                reflectionPoints.Add(hitPoint);

                lineRenderer.positionCount = pointIndex + 1;
                lineRenderer.SetPosition(pointIndex, hitPoint);

                remainingLength -= Vector2.Distance(origin, hitPoint);
                direction = Vector2.Reflect(direction, hit.normal);
                origin = hitPoint;

                pointIndex++;
            }
            else
            {
                Vector2 endPoint = origin + direction * remainingLength;
                reflectionPoints.Add(endPoint);

                lineRenderer.positionCount = pointIndex + 1;
                lineRenderer.SetPosition(pointIndex, endPoint);
                break;
            }
        }
    }

    public List<Vector2> GetReflectionPath()
    {
        return reflectionPoints;
    }

    public void SetKnife(Transform knife)
    {
        knifeTransform = knife;
    }

    public void Hide()
    {
        lineRenderer.enabled = false;
    }
}
