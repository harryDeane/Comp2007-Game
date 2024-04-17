using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager instance;

    void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndGame()
    {
        // Load the menu scene when the player dies
        SceneManager.LoadScene("MainMenu");
    }
}