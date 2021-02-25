using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] ingredients;

    [SerializeField] float spawnXRange = 2f;
    [SerializeField] float secondsBetweenSpawns = 5f;

    void Start()
    {
        StartCoroutine(SpawnIngredients());
    }

    IEnumerator SpawnIngredients()//Todo bir farklı zaman daha ekle o iki zaman arasında rastgele zamanlarda düşsün
    {
        yield return new WaitForSecondsRealtime(2);//ilk başta açılış geri sayımını bekle
        while (true)//bu niye böyle ?**Herzaman çalışsın diye olabilir
        {
            int ingredientToSpawn = Random.Range(0, ingredients.Length);
            float spawnPosition = Random.Range(-spawnXRange, spawnXRange);

            Vector3 spawn = new Vector3(spawnPosition,0,0);
            GameObject ingredient = Instantiate(ingredients[ingredientToSpawn], transform.position+spawn, ingredients[ingredientToSpawn].transform.rotation);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }


}
