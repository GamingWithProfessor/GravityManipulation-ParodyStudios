using UnityEngine;
using TMPro; 

public class Collectible : MonoBehaviour
{
    public TextMeshProUGUI collectibleCountText; 

    private GameManager gameManager; // Reference to the GameManager script

    void Start()
    {
        // Find and assign the GameManager instance
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.IncrementCollectedCount();
            Destroy(gameObject);
            UpdateCollectibleCountText();
        }
    }
    void UpdateCollectibleCountText()
    {
        if (collectibleCountText != null)
        {
            collectibleCountText.text = "Collectibles: " + gameManager.GetCollectedCount();
        }
    }
}
