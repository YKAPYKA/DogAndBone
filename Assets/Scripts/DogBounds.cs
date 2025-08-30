using UnityEngine;

public class DogBounds : MonoBehaviour
{
    private Camera cam;
    private float halfWidth, halfHeight;

    void Start()
    {
        cam = Camera.main;
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Assuming your dog pivot is centered
        halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        halfHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // Or if using CircleCollider2D:
        // halfWidth = GetComponent<CircleCollider2D>().radius;
        // halfHeight = GetComponent<CircleCollider2D>().radius;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        pos.x = Mathf.Clamp(pos.x, cam.transform.position.x - horzExtent + halfWidth, cam.transform.position.x + horzExtent - halfWidth);
        pos.y = Mathf.Clamp(pos.y, cam.transform.position.y - vertExtent + halfHeight, cam.transform.position.y + vertExtent - halfHeight);

        transform.position = pos;
    }
}
