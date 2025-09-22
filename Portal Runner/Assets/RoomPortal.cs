using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomPortal : MonoBehaviour
{
    [SerializeField] private string room;
    private void OnTriggerEnter2D(Collider2D changeScene)
    {
        if (changeScene.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(room);
        }
    }
    
}
