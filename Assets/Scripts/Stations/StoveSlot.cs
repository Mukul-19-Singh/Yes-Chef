using UnityEngine;
using TMPro;
using System.Collections;

public class StoveSlot : MonoBehaviour, InteractableStation
{
    private bool isOccupied = false;
    private bool isFinished = false;

    [SerializeField] private GameObject ingredient;
    [SerializeField] private Material rawMeat;
    [SerializeField] private Material cookedMeat;
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        ResetSlot();
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        // Place raw meat on the stove
        if (!isOccupied && playerInteraction.heldIngredient == "Meat")
        {
            playerInteraction.heldIngredient = "";
            StartCoroutine(CookRoutine());
            Debug.Log("Player placed raw meat on the stove.");
        }
        // Pick up cooked meat
        else if (isFinished && playerInteraction.heldIngredient == "")
        {
            playerInteraction.heldIngredient = "Cooked_Meat";
            Debug.Log("Player picked up cooked meat.");
            ResetSlot();
        }
    }

    private IEnumerator CookRoutine()
    {
        isOccupied = true;
        ingredient.SetActive(true);
        timerText.gameObject.SetActive(true);
        ingredient.GetComponent<MeshRenderer>().material = rawMeat;

        float cookTime = 6f; // Time it takes to cook the meat
        while (cookTime > 0)
        {
            cookTime -= Time.deltaTime;
            timerText.text = cookTime.ToString("F1") + "s";
            yield return null;
        }

        isFinished = true;
        ingredient.GetComponent<MeshRenderer>().material = cookedMeat;
        timerText.text = "DONE";
        Debug.Log("Meat is done cooking.");
    }

    private void ResetSlot()
    {
        isOccupied = false;
        isFinished = false;
        ingredient.SetActive(false);
        if (timerText != null) timerText.text = "";
        Debug.Log("Slot reset.");
    }
}
