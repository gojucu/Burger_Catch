using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrder : MonoBehaviour
{
    [Tooltip("In m")] [SerializeField] float order2DStartDistance= 3f; //Resmin oluşturulacağı mesafe

    public RecipeConfig Order;

    public void ShowNewOrder(RecipeConfig order)
    {
        Vector3 spawn = new Vector3(order2DStartDistance, 0, 0);

        GameObject order2D =  Instantiate(order.GetOrder2D(), transform.parent.localPosition + spawn, order.GetOrder2D().transform.rotation);//siparişin resmini istenen bölgede oluştur
        order2D.transform.parent = transform;//siparişi ShowOrder scriptinin olduğu Screen gameobjectinin child ı yap
    }
}
