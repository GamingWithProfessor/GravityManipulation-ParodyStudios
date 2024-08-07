using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public float timeLimit = 120f; 
    public float fallThreshold = 6f; 
    public GameObject player; 
    public GameObject[] collectibleObjects; 
    public TextMeshProUGUI collectibleCountText; 

    private float timeRemaining;
    private bool gameIsOver = false;
    private bool gameCompleted = false;
    private float fallTime = 0f; 
    private int collectedCount = 0; 

    private CharacterMovement characterMovement; // Reference to CharacterMovement script

    void Start()
    {
        timeRemaining = timeLimit;
        characterMovement = player.GetComponent<CharacterMovement>(); // Get the CharacterMovement component

        if (collectibleObjects.Length == 0)
        {
            collectibleObjects = GameObject.FindGameObjectsWithTag("Collectible");
        }
    }

    void Update()
    {
        if (!gameIsOver && !gameCompleted)
        {
            UpdateTimer();
            CheckGameCompletion();
        }
    }

    void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            GameOver();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Convert float to minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckGameCompletion()
    {
        // Check if all collectible objects have been collected
        bool allCollected = collectedCount >= collectibleObjects.Length;

        if (allCollected)
        {
            GameCompleted();
        }
    }

    public void HandleFalling()
    {
        fallTime += Time.deltaTime; // Increment fall time if falling
        if (fallTime > fallThreshold)
        {
            GameOver(); // Call GameOver if falling time exceeds threshold
        }
    }

    public void IncrementCollectedCount()
    {
        collectedCount++;
        UpdateCollectibleCountText();
    }

    public int GetCollectedCount()
    {
        return collectedCount;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Checking if the collision object is tagged as ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("OnCollisionEnter - Grounded: True, Object: " + collision.gameObject.name);            
        }
        // Check if the collision object is a collectible
        else if (collision.gameObject.CompareTag("Collectible"))
        {
            CollectibleCollected(collision.gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the collision object is tagged as ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("OnCollisionExit - Grounded: False, Object: " + collision.gameObject.name);            
        }
    }

    void CollectibleCollected(GameObject collectible)
    {
        // Deactivate the collected collectible
        collectible.SetActive(false);
        collectedCount++;
        Debug.Log("Collectible Collected: " + collectible.name);

        // Update the collectible count text
        UpdateCollectibleCountText();

        // Check if all collectibles are collected
        CheckGameCompletion();
    }

    void UpdateCollectibleCountText()
    {
        if (collectibleCountText != null)
        {
            collectibleCountText.text = "Collectibles: " + collectedCount;
        }
    }

    void GameOver()
    {
        gameIsOver = true;
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameCompleted()
    {
        gameCompleted = true;
        Debug.Log("Game Completed!");
    }
}
