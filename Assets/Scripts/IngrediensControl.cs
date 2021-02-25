using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngrediensControl : MonoBehaviour
{
    Vector3 finalPos;
    [Tooltip("In ms^-1")] [SerializeField] float fallSpeed = 1f;
    [Tooltip("In m")] [SerializeField] float fallRange = 5f; //Dusecegi mesafe
    [SerializeField] float fallAngle = -30;
    public int IngredientId;

    public bool isOnPlate, hitForbidden, hitWrongIngredient;
    OrderControl orderControl;

    [SerializeField] GameObject destroyParticles=null;

    private float colliderYSize;
    // Start is called before the first frame update
    void Start()
    {
        //finalPos pozisyonunu ayarla
        colliderYSize = this.GetComponent<Collider>().bounds.size.y;
        gameObject.transform.rotation = Quaternion.Euler(fallAngle, 0, 0);
        isOnPlate = false;
        hitForbidden = false;
        hitWrongIngredient = false;
        finalPos = new Vector3(transform.position.x, transform.position.y - fallRange, transform.position.z);
        orderControl= FindObjectOfType<OrderControl>();
        
    }

    void Update()
    {
        if (isOnPlate != true)
        {
            //ilk pozisyondan finalPos pozisyonuna fallSpeed hızıyla ilerle
            transform.position = Vector3.Lerp(transform.position, finalPos, fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "DestroyFood")//Sahne dışına çıkan malzemeleri yok etmek için
        {
            Destroy(gameObject);
        }

        if (collider.gameObject.tag == "Forbidden"&& isOnPlate!=true && hitForbidden!=true && hitWrongIngredient!=true)
        {
            hitForbidden = true;
            orderControl.RemoveOnTop(this);
        }
    }

    public float GetColliderBoundSizeY()
    {
        return colliderYSize;
    }
    public void DestroyIngredient()
    {
        GameObject go= Instantiate(destroyParticles, transform.position, transform.rotation) as GameObject;
        go.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }

}
