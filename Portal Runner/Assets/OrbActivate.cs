using UnityEngine;

public class OrbActivate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            PlatformerMovement.hasOrb = true;
            Destroy(gameObject);
        }
    }
}
