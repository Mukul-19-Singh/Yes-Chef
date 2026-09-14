using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 1f;
    [SerializeField] private TMP_Text heldItemText;

    // The player can only hold 1 ingredient in hand at a time
    private string _heldIngredient = "";
    // Using this property ensures the visual text updates automatically every time the ingredient changes
    public string heldIngredient
    {
        get { return _heldIngredient;}
        set 
        { 
            _heldIngredient = value;
            UpdateIngredientVisual();
        }
    }

    private void Start()
    {
        UpdateIngredientVisual();
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        // Does not allow player interaction if the game is not in a playing state
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying) return;
        // E key used for station interactions
        if (Input.GetKeyDown(KeyCode.E))
        {
            AttemptInteraction();
        }
    }

    private void AttemptInteraction()
    {
        // Checks for colliders within a short radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in hitColliders)
        {
            // If the object hit has a script implementing InteractableStation, trigger it
            if (collider.TryGetComponent(out InteractableStation interactableStation))
            {
                interactableStation.Interact(this);
                break; // Only interact with the first valid station found
            }
        }
    }

    private void UpdateIngredientVisual()
    {
        if (heldItemText != null)
        {
            // Displays the item name or "Empty" if holding nothing
            heldItemText.text = string.IsNullOrEmpty(_heldIngredient) ? "" : _heldIngredient;  
        }
    }
}
