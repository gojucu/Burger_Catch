using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("New Recipe"))]
public class RecipeConfig : ScriptableObject
{
    [SerializeField] GameObject[] ingredients=null;
    [SerializeField] int recipeScore = 10;

    [SerializeField] GameObject order2D=null;


    public GameObject[] GetIngredients()
    {
        return ingredients;
    }

    public int GetRecipeScore()
    {
        return recipeScore;
    }

    public GameObject GetOrder2D()
    {
        return order2D;
    }

}
