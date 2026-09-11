using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // The player can only hold 1 ingredient in hand at a time
    public string heldIngredient = "";
    public float interactionRange = 1.5f;

    private void Update()
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
}
