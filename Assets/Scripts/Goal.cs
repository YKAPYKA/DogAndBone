using UnityEngine;

public class Goal : MonoBehaviour
{
    public AudioSource goalSfx; // Assign in Inspector
    private bool played = false; // Make sure it only plays once

    void OnTriggerEnter2D(Collider2D other)
    {
        if (played) return; // Already triggered

        if (other.CompareTag("Dog"))
        {
            if (goalSfx != null)
                goalSfx.Play(); // Play goal sound
            played = true;

            // Call GameManager win function
            if (GameManager.Instance != null)
                GameManager.Instance.Win();
        }
    }
}
