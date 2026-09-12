using UnityEngine;
using System.Collections;
using TMPro;

public class Table : MonoBehaviour, InteractableStation
{
    private bool isOccupied = false;
    private bool isFinished = false;

    [SerializeField] private GameObject ingredient;
    [SerializeField] private Material rawMaterial;
    [SerializeField] private Material choppedMaterial;
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        ResetStation();
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        // Place raw vegetable on the table
        if (!isOccupied && playerInteraction.heldIngredient == "Vegetable")
        {
            playerInteraction.heldIngredient = "";
            StartCoroutine(ChopRoutine());
            Debug.Log("Player placed raw vegetables on the table.");
        }
        // Pick up chopped vegetable
        else if (isFinished && playerInteraction.heldIngredient == "")
        {
            playerInteraction.heldIngredient = "Chopped_Vegetables";
            ResetStation();
            Debug.Log("Player picked up chopped vegetables.");
        }
    }

    private IEnumerator ChopRoutine()
    {
        isOccupied = true;
        ingredient.SetActive(true);
        timerText.gameObject.SetActive(true);
        ingredient.GetComponent<MeshRenderer>().material = rawMaterial;

        float chopTime = 2f; // Time it takes to chop the vegetable
        while (chopTime > 0)
        {
            chopTime -= Time.deltaTime;
            timerText.text = chopTime.ToString("F1") + "s";
            yield return null;
        }

        isFinished = true;
        ingredient.GetComponent<MeshRenderer>().material = choppedMaterial;
        timerText.text = "DONE";
        Debug.Log("Vegetables are done chopping.");
    }

    private void ResetStation()
    {
        isOccupied = false;
        isFinished = false;
        ingredient.SetActive(false);
        if (timerText != null) timerText.text = "";
        Debug.Log("Station reset.");
    }
}
