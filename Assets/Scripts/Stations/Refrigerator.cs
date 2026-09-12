using UnityEngine;

public class Refrigerator : MonoBehaviour, InteractableStation
{
    public string ingredientToDispense = "Vegetable"; 

    public void Interact(PlayerInteraction playerInteraction)
    {
        // The player can only hold 1 ingredient at a time
        if (playerInteraction.heldIngredient == "")
        {
            playerInteraction.heldIngredient = ingredientToDispense;
            Debug.Log($"Player picked up: {ingredientToDispense}");
        }
        else
        {
            Debug.Log("Player is already holding an ingredient.");
        }
    }
}
