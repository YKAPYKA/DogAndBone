using UnityEngine;

public class DogController : MonoBehaviour
{
    [Header("Movement Settings")]
    public LayerMask wallMask;
    public float extraPadding = 0.01f;

    private CircleCollider2D circle;
    private Camera cam;
    private Vector3 mouseOffset;

    void Awake()
    {
        circle = GetComponent<CircleCollider2D>();
        cam = Camera.main;

        // Hide OS cursor, but don't lock it
        Cursor.visible = false;

        // Store the initial offset between dog and mouse
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        mouseOffset = transform.position - mouseWorld;
    }

    void Update()
    {
        if (cam == null) return;

        // Mouse position → world position
        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(cam.transform.position.z);
        Vector3 target = cam.ScreenToWorldPoint(mouse);
        target.z = 0f;

        // Apply offset so dog starts at its position
        target += mouseOffset;

        // Check wall collisions
        Vector2 from = transform.position;
        Vector2 to = target;
        Vector2 delta = to - from;
        float dist = delta.magnitude;

        float radius = circle.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);

        if (dist > 0f)
        {
            RaycastHit2D hit = Physics2D.CircleCast(from, radius, delta.normalized, dist + extraPadding, wallMask);
            if (hit.collider != null && GameManager.Instance != null)
            {
                GameManager.Instance.Lose();
                return;
            }
        }

        transform.position = target;
    }

    void OnDestroy()
    {
        // Show cursor again when dog is destroyed (e.g. scene ends)
        Cursor.visible = true;
    }
}
