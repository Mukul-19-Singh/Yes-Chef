using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class Window : MonoBehaviour, InteractableStation
{
    private List<string> requiredIngredients = new List<string>();
    private bool hasActiveOrder = false;
    private float orderTimeActiveFor = 0f;
    private int currentScore = 0;
    private float waitTime = 3f;
    private float newOrderWaitTime = 5f;

    [SerializeField] private TMP_Text orderListText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scorePopupText;

    private void Start()
    {
        scorePopupText.text = "";
        // The game starts with all 4 open orders automatically
        GenerateNewOrder();
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying) return;
        UpdateActiveOrderTime();
    }

    private void UpdateActiveOrderTime()
    {
        if (hasActiveOrder)
        {
            orderTimeActiveFor += Time.deltaTime;
            // Timer indicating how long the order has been open
            timerText.text = Mathf.FloorToInt(orderTimeActiveFor).ToString() + "s";
        }
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        // If the ingredient is not required or there is no order it remains in hand
        if (!hasActiveOrder || playerInteraction.heldIngredient == "") return;
        if (requiredIngredients.Contains(playerInteraction.heldIngredient))
        {
            requiredIngredients.Remove(playerInteraction.heldIngredient);
            playerInteraction.heldIngredient = "";
            UpdateOrderUI();
            if (requiredIngredients.Count == 0)
            {
                CompleteOrder();
            }
        }
    }    

    private void CompleteOrder()
    {
        hasActiveOrder = false;
        orderListText.text = "";
        timerText.text = "";
        // Score is the sum of ingredient values minus seconds passed
        int timePenalty = Mathf.FloorToInt(orderTimeActiveFor);
        int finalScore = currentScore - timePenalty;
        GameManager.Instance.AddScore(finalScore);

        StartCoroutine(ShowPopupScore(finalScore));
        StartCoroutine(RespawnOrder());
    }

    private void GenerateNewOrder()
    {
        requiredIngredients = OrderManager.Instance.GenerateRandomOrders();
        currentScore = 0;
        orderTimeActiveFor = 0f;
        // Pre-calculate the maximum score this order can yield
        foreach (string order in requiredIngredients)
        {
            currentScore += OrderManager.Instance.GetIngredientScore(order);
        }
        hasActiveOrder = true;
        UpdateOrderUI();
    }

    private void UpdateOrderUI()
    {
        // Indicates required ingredients needed to complete the order
        orderListText.text = string.Join("\n", requiredIngredients);
    }

    private IEnumerator ShowPopupScore(int score)
    {
        // Displays score added near the window then fades it away
        scorePopupText.text = score >= 0 ? "+" + score : score.ToString();
        scorePopupText.color = score >= 0 ? Color.green : Color.red;
        yield return new WaitForSeconds(waitTime);
        scorePopupText.text = "";
    }

    private IEnumerator RespawnOrder()
    {
        // It takes 5 seconds for an order to respawn on the window
        yield return new WaitForSeconds(newOrderWaitTime);
        if (GameManager.Instance.IsPlaying) GenerateNewOrder();
    }
}
