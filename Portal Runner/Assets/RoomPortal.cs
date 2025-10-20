using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomPortal : MonoBehaviour
{
    // makes a field in the scene editor to type in the name of the room you want the object to warp to
    [SerializeField] private string room;

    /* when an object enters the collision box for the object,
     * activate this method. collider is the object that is in the hitbox*/
    private void OnTriggerEnter2D(Collider2D collider)
    {   // if the collider has the tag player, load the scene with the String that the variable "name" has
        if (collider.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(room);
        }
    }
    
}
