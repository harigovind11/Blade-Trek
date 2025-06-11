using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimLineController : MonoBehaviour
{
    private static AimLineController _instance;
    
    public Transform knifeTransform;
    public string targetTag = "Log";
    public int maxBounces = 10;
    public LayerMask bounceMask;

    private LineRenderer _lineRenderer;
    private List<Vector2> _reflectionPoints = new List<Vector2>();
    private Transform _targetTransform;
    

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;

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

        _reflectionPoints.Add(origin);
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, origin);

        int pointIndex = 1;

        for (int i = 0; i < maxBounces; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, bounceMask);

            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;
                string hitTag = hit.collider.tag;

                Debug.Log($"Ray hit: {hit.collider.name}, tag: {hitTag}");

                AddPoint(hitPoint, pointIndex);

                // ❗ STOP if we hit Target or Obstacle
                if (hitTag == targetTag || hitTag == "Obstacle")
                    break;

                // Reflect off surface (like wall)
                direction = Vector2.Reflect(direction, hit.normal);
                origin = hitPoint;
                pointIndex++;
            }
            else
            {
                // No hit, just draw long line forward
                AddPoint(origin + direction * 20f, pointIndex);
                break;
            }
        }

        _lineRenderer.enabled = true;
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
