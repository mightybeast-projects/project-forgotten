using UnityEngine;

public class ShockwaveBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Bullet"))
            Destroy(other.gameObject);
    }
}