using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimLineController : MonoBehaviour
{
    public Transform knifeTransform;
    public string targetTag = "Log";
    public int maxBounces = 3;
    public LayerMask bounceMask;

    private LineRenderer _lineRenderer;
    private List<Vector2> _reflectionPoints = new List<Vector2>();
    private Transform _targetTransform;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        GameObject targetObj = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObj != null)
        {
            _targetTransform = targetObj.transform;
        }
    }

    void Update()
    {
        if (knifeTransform == null || _targetTransform == null)
        {
            _lineRenderer.enabled = false;
            return;
        }

        _lineRenderer.enabled = true;
        DrawAimLine();
    }

    void DrawAimLine()
    {
        _reflectionPoints.Clear();

        Vector2 origin = knifeTransform.position;
        Vector2 direction = knifeTransform.up.normalized;
        Vector2 target = _targetTransform.position;

        _reflectionPoints.Add(origin);
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, origin);

        int pointIndex = 1;

        while (pointIndex <= maxBounces)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, bounceMask);

            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;

                float distToTarget = Vector2.Dot(target - origin, direction);
                float distToHit = Vector2.Dot(hitPoint - origin, direction);
                if (distToTarget <= distToHit)
                {
                    AddPoint(target, pointIndex);
                    break;
                }

                AddPoint(hitPoint, pointIndex);

                direction = Vector2.Reflect(direction, hit.normal);
                origin = hitPoint;
                pointIndex++;
            }
            else
            {
                AddPoint(target, pointIndex);
                break;
            }
        }
    }

    private void AddPoint(Vector2 point, int index)
    {
        _reflectionPoints.Add(point);
        _lineRenderer.positionCount = index + 1;
        _lineRenderer.SetPosition(index, point);
    }

    public List<Vector2> GetReflectionPath()
    {
        return _reflectionPoints;
    }

    public void SetKnife(Transform knife)
    {
        knifeTransform = knife;
    }

    public void Hide()
    {
        _lineRenderer.enabled = false;
    }
}
