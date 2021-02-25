using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;

public class OrderControl : MonoBehaviour
{
    public List<GameObject> IngredientsOnplate = new List<GameObject>();
    public List<RecipeConfig> RecipeList = new List<RecipeConfig>();//Rastgele getirilebilecek listeler

    //public List<RecipeConfig> Orders = new List<RecipeConfig>();//Birden fazla order olursa bu kullanılıcak muhtemelen
    public RecipeConfig Order;
    GameObject forbidden;
    ScoreBoard scoreBoard;
    ShowOrder showOrder;
    Order2DController order2DController;

    public GameObject newIngredient, gameOver;

    int siradaki = 0;

    public float allHeight, plateHeight;
    public int plateFoodCount = 0;
    float colliderPos = 0f;

    [SerializeField] AudioClip mistake=null;
    [SerializeField] AudioClip correctIngredient = null;
    AudioSource audioSource;

    //public void deneme() //Burada birden fazla order denemesinin başlangıcını yaptıydın daha sonra yaparken aklına gelmez ise burdan dene
    //{
    //    GameObject[] hey = Orders[0].GetIngredients();
    //    foreach(var order in Orders)
    //    {
    //        if(order.GetIngredients())
    //    }
    //}

    void Start()
    {
        plateHeight = GetComponent<Collider>().bounds.size.y;
        forbidden = GameObject.FindGameObjectWithTag("Forbidden");
        scoreBoard = FindObjectOfType<ScoreBoard>();
        showOrder = FindObjectOfType<ShowOrder>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(WaitforCountdown());//Oyun başlamadan önce beklet sonra siparişi al.
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))//Test 0: siparişi bitirip yeni siparişi hızlıca getirip görmek için.
        {
            FinishOrder();
            NewOrder();
        }

        if (PlayerPrefs.GetInt("sounds", 1) == 0)
        {
            audioSource.mute = true;
        }else if(PlayerPrefs.GetInt("sounds", 1) == 1)
        {
            audioSource.mute = false;
        }
    }

    IEnumerator WaitforCountdown()
    {
        yield return new WaitForSecondsRealtime(2);
        NewOrder();
    }

    private void NewOrder()
    {
        Order = null;//bu null new orderın içine girmeli mi ? ve bu şart mı ?
        int randomIndex = UnityEngine.Random.Range(0, RecipeList.Count);
        Order = RecipeList[randomIndex];

        showOrder.ShowNewOrder(Order);
    }

    private void OnTriggerEnter(Collider collider)
    {
        newIngredient = collider.gameObject;

        var ingControl = newIngredient.GetComponent<IngrediensControl>();

        if (newIngredient.GetComponent<IngrediensControl>() != null && newIngredient != null && CompareTag("Plate") && Order != null)
        {
            GameObject[] orderIng = Order.GetIngredients();
            var NextId = orderIng[siradaki].GetComponent<IngrediensControl>().IngredientId;
            
            var CollidedId = newIngredient.GetComponent<IngrediensControl>().IngredientId;
            if (ingControl.isOnPlate != true && ingControl.hitForbidden!=true && ingControl.hitWrongIngredient != true)
            {
                //bu if le üstteki if && ile birleşirmi bak
                if (NextId == CollidedId)
                {
                    newIngredient.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);//Rotasyonlarını sıfırla 
                    PutOnTop(collider);
                    if (siradaki==orderIng.Length-1)// Sipariş bitti ise buraya giricek siparişi silip index'i 0 yapıcak. Skor ekleme işini burdan çağır
                    {
                        siradaki = 0;
                        
                        FinishOrder();
                        NewOrder();
                    }
                    else if(siradaki < orderIng.Length)
                    {
                        siradaki += 1;
                    }
                }
                else//Yanlış malzeme geldiyse
                {
                    ingControl.hitWrongIngredient = true;
                    RemoveOnTop(ingControl);
                }

            }
        }
    }


    private void FinishOrder()
    {
        var score = Order.GetRecipeScore();
        scoreBoard.AddScore(score);

        GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);//Tepsinin collider ini ilk yerine getir.
        foreach (GameObject go in IngredientsOnplate)
        {
            go.GetComponent<IngrediensControl>().DestroyIngredient();
        }
        IngredientsOnplate.Clear();
        plateFoodCount = 0;
        allHeight = 0f;
        colliderPos = 0f;

        Collider fCollider = forbidden.GetComponent<Collider>();
        fCollider.enabled = false;
        forbidden.GetComponent<BoxCollider>().size = new Vector3(1, 0, 1);//Sadece enable false yapsam yeterlimi ? bunu 0 lamaya gerek var mı ?

        order2DController = FindObjectOfType<Order2DController>();
        order2DController.LeaveScreen();
    }

    private void PutOnTop(Collider collider)
    {
        //ayrılabilir
        IngredientsOnplate.Add(newIngredient);

        newIngredient.GetComponent<IngrediensControl>().isOnPlate = true;//nesne tabakta
        newIngredient.GetComponent<Transform>().SetParent(transform.parent);//Dokunduktan sonra All parent olsun
          //ayrılabilir kapa

        float ingredientHeight = collider.bounds.size.y;
        float ingredientYPosition;

        //Yeni malzemenin y pozisyonunu belirleme
        if (plateFoodCount > 0)//tabakta zaten malzeme varsa
        {

            ingredientYPosition = (ingredientHeight / 2) - (plateHeight / 2) + allHeight;
        }
        else//tabak bos ise
        {
            allHeight = plateHeight;
            ingredientYPosition = (ingredientHeight / 2) + (allHeight / 2);
        }


        allHeight = allHeight + ingredientHeight;// tabagin yuksekligine yeni malzemenin yuksekligini ekle

        //**
        newIngredient.GetComponent<Transform>().localPosition = new Vector3(//Yeni malzemenin poziyonunu değiştir
            0,
            ingredientYPosition,
            0);
        newIngredient.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);//Tepsi oynadığı için z rotasyonu oynuyor bozuk gözüküyor gibi test lazım. böyle oldu gibi ama test lazım

        //***
        plateFoodCount++;//tabaktaki yemek sayisini arttir

        audioSource.Stop();
        audioSource.PlayOneShot(correctIngredient);

        NewPlateColliderPosition(plateHeight); // Tabagin yeni collider y pozisyonu
        NewForbiddenColliderSize(plateHeight); //Forbidden colliderin yeni boyutunu ayarla

        //GetComponent<BoxCollider>().size = new Vector3(1, 1+, 1); bune sil temizle

    }

    private void NewPlateColliderPosition(float plateHeight)// Tabagin yeni collider y pozisyonu
    {
        //colliderPos = (allHeight / plateHeight) - 1; Formuülü anlamadım neden bölüp çıkarıyorum Şimdi aşağıdaki gibi çalışıyor.
        colliderPos = allHeight -plateHeight;
        
        GetComponent<BoxCollider>().center = new Vector3(0, colliderPos, 0);
    }


    //Forbidden collider ayarlari
    private void NewForbiddenColliderSize(float plateHeight)
    {
        Collider fCollider = forbidden.GetComponent<Collider>();
        fCollider.enabled = true;

        float fHeight = allHeight - plateHeight;
        float fPos = (fHeight - (plateHeight)) / 2;
        forbidden.GetComponent<Transform>().localPosition = new Vector3(
            0,
            fPos,
            0);
        forbidden.GetComponent<BoxCollider>().size = new Vector3(1, fHeight, 1);
    }


    public void RemoveOnTop(IngrediensControl ingControl)//Tabakta yemek yoksa onun içinde kod yaz ??? bunu neden yazdım kontrol et==çözüldü neden olduğu tabakta yemek yoksa yanlış malzemeler tabağa değince yok olmuyor.
    {
        if (siradaki > 0)//BUGGGGGG ??? varmı yok gibi sonra temizle Bu if e gerek var mı direk alttaki if in içinde olsa olur mu ?
        {
            siradaki--;
        }

        if (plateFoodCount > 0)//Bug olduğunu sandığın yer bug yok gibi
        {
            GameObject topmost = IngredientsOnplate[IngredientsOnplate.Count - 1];// En üstteki silinicek bu

            float topmostHeight = topmost.GetComponent<IngrediensControl>().GetColliderBoundSizeY();
            //float topmostHeight = topmost.GetComponent<Collider>().bounds.size.y;//bu tepsi açılı gittiği için değeri olduğundan büyük duruyor

            allHeight = allHeight - topmostHeight;

            IngredientsOnplate.Remove(topmost);//Tabaktaki yemek listesinden en üsttekini çıkar.

            topmost.GetComponent<IngrediensControl>().DestroyIngredient();//en üstteki malzemeyi yok et
            //Destroy(topmost); Üstteki düzgünse bunu sil

            plateFoodCount--;//Tabaktaki yemek sayısını azalt
            //var hitForbidden = newIngredient.GetComponent<IngrediensControl>().hitForbidden;

            NewPlateColliderPosition(plateHeight); //Tabagin yeni collider y pozisyonu
            NewForbiddenColliderSize(plateHeight); //Forbidden colliderin yeni boyutunu ayarla
        }

        ingControl.DestroyIngredient();//Düşen malzemeyi yok et
        //Debug.Log("hata sesi");
        audioSource.Stop();
        audioSource.PlayOneShot(mistake);
        GameOver.health -= 1; // Canı 1 Azalt
    }

}
