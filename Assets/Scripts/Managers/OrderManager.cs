using UnityEngine;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set;}

    private string[] availableIngredients = {"Vegetable", "Meat", "Cheese"};

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public List<string> GenerateRandomOrders()
    {
        List<string> newOrder = new List<string>();
        // Orders have a 50% chance to be 2 ingredients and a 50% chance to be 3 ingredients.
        int ingredientCount = Random.Range(2,4);
        for (int i = 0; i < ingredientCount; i++)
        {
            int index = Random.Range(0, availableIngredients.Length);
            newOrder.Add(availableIngredients[index]);
        }

        return newOrder;
    }

    public int GetIngredientScore(string ingredient)
    {
        // Score values for ingredients
        switch (ingredient)
        {
            case "Vegetable": return 20;
            case "Meat": return 30;
            case "Cheese": return 10;
            default: return 0;
        }
    }
}
