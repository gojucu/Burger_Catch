using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order2DController : MonoBehaviour
{
    Vector3 screenCenter;
    Vector3 endPoint;

    [Tooltip("In m")] [SerializeField] float moveRange = 3f; //gideceği mesafe
    [Tooltip("In s")] [SerializeField] float duration=1f;//ne kadar sürede gidecek
    void Start()
    {
        screenCenter = new Vector3(0, 0, 0);
        endPoint = new Vector3(screenCenter.x - moveRange, 0, 0);
        StartCoroutine(EnterScreen());
    }

    void Update()
    {
        if (transform.localPosition == endPoint)//ekrandan çıkıp noktasına gittiğinde yok et
        {
            Destroy(gameObject);
        }
    }

    IEnumerator EnterScreen()
    {
        float time = 0;

        Vector3 startPosition = transform.localPosition;

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);//daha smooth bir geçiş için formül
            transform.localPosition = Vector3.Lerp(startPosition, screenCenter, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = screenCenter;
    }

    public void LeaveScreen()
    {
        StartCoroutine(MoveToEnd());
    }
    IEnumerator MoveToEnd()
    {
        float time = 0;

        Vector3 startPosition = transform.localPosition;

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);//daha smooth bir geçiş için formül
            transform.localPosition = Vector3.Lerp(startPosition, endPoint, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPoint;
    }
}

