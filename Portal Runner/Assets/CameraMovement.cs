using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    void Awake()
    {
        
    }
    void LateUpdate()
    {
        Vector2 newPos = transform.position;
        newPos.y = player.position.y + 2;
        transform.position = newPos;
    }
}
