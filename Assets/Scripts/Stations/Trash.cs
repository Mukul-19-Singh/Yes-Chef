using UnityEngine;

public class Trash : MonoBehaviour, InteractableStation
{
    public void Interact(PlayerInteraction playerInteraction)
    {
        if (playerInteraction.heldIngredient != "")
        {
            Debug.Log($"Player discarded: {playerInteraction.heldIngredient}");
            playerInteraction.heldIngredient = ""; // Clear the held ingredient
        }
        else
        {
            Debug.Log("Player is not holding any ingredient to discard.");
        }
    }
}
