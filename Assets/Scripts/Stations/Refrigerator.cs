using UnityEngine;
using TMPro;

public class Refrigerator : MonoBehaviour, InteractableStation
{
    private string[] availableIngredients = {" Vegetable"," Meat","Cheese"};
    private int cycleIndex = 0;

    [SerializeField] private TMP_Text fridgeText;

    public void Interact(PlayerInteraction playerInteraction)
    {
        // Allows pickup if hands are empty or if the player is already holding a raw ingredient to swap it
        if (playerInteraction.heldIngredient == "" || IsRawIngredient(playerInteraction.heldIngredient))
        {

            playerInteraction.heldIngredient = availableIngredients[cycleIndex];
            // Updates the cycle index for the next interaction
            cycleIndex = (cycleIndex + 1) % availableIngredients.Length;
            Debug.Log($"Player picked up: {playerInteraction.heldIngredient}");

            if (fridgeText != null)
            {
                fridgeText.text = availableIngredients[cycleIndex];
            }
        }
    }

    private bool IsRawIngredient(string item)
    {
        return item == " Vegetable" || item == " Meat" || item == "Cheese";
    }
}
