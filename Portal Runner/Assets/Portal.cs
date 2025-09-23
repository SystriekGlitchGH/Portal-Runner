using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private Transform destination;

    public float delayTime = 1f;
    private float currentTimer = 0f;

    public Color disabledColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentTimer > 0)
            return;

        if (portalObjects.Contains(collision.gameObject))
        {
            return; // makes sure objects aren't constantly teleporting 
        }
        if (destination.TryGetComponent(out Portal destinationPortal))
        {
            destinationPortal.portalObjects.Add(collision.gameObject);
        }

        collision.transform.position = destination.position;
        currentTimer = delayTime;

        GetComponent<SpriteRenderer>().color = disabledColor;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        portalObjects.Remove(collision.gameObject);
    }

    private void Update()
    {
        if(currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
