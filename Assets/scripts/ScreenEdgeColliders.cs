using UnityEngine;

public class ScreenEdgeColliders : MonoBehaviour
{
    public float thickness = 0.1f;
    public bool makeReflective = true;

    void Start()
    {
        CreateEdge("Left", new Vector2(-1, 0), new Vector2(thickness, 1));
        CreateEdge("Right", new Vector2(1, 0), new Vector2(thickness, 1));
        // CreateEdge("Top", new Vector2(0, 1), new Vector2(1, thickness));
        // CreateEdge("Bottom", new Vector2(0, -1), new Vector2(1, thickness));
    }

    void CreateEdge(string name, Vector2 anchor, Vector2 size)
    {
        GameObject edge = new GameObject(name);
        edge.transform.parent = transform;

        // Convert to world position
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(
            (anchor.x + 1) / 2, (anchor.y + 1) / 2, 0));
        worldPos.z = 0;
        edge.transform.position = worldPos;

        BoxCollider2D col = edge.AddComponent<BoxCollider2D>();

        float height = 2f * Camera.main.orthographicSize;
        float width = Camera.main.aspect;
        col.size = new Vector2(size.x * width, size.y * height);

        // Assign tag if needed
        if (makeReflective)
        {
            edge.tag = "Reflective";
        }

        // Assign the layer
        edge.layer = LayerMask.NameToLayer("BounceSurface");

        Rigidbody2D rb = edge.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

}