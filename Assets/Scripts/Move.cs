using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 12f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f; //local x de gidebileceği mesafe

    [Header("Control-throw Based")]
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;

    private Touch touch;

    float xThrow;
    bool isControlEnabled = true;


    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }
    private void ProcessRotation()// tepsi hareket ederken tepsiyi hareket ettiği yönde eğ
    {
        xThrow = touch.deltaPosition.x/10;
        float yaw = transform.localPosition.x * positionYawFactor;//tepsinin pozisyonuna göre kamera için açısı
        float roll = xThrow * controlRollFactor; //Tepsinin hareket yönüne göre eğileceği açı
        transform.localRotation = Quaternion.Euler(transform.localPosition.y, yaw, roll);
    }

    //TODO hareket hissiyatı çok iyi değil gibi. tepsinin altına slider ekleyip kontrolü onla deniyebilirsin.
    private void ProcessTranslation()//pozisyonu değişitir +x veya -x
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float xOffset = touch.deltaPosition.x * controlSpeed * Time.deltaTime;

                float rawXPos = transform.localPosition.x + xOffset;

                float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

                transform.localPosition = new Vector3(
                    clampedXPos,
                    transform.localPosition.y,
                    transform.localPosition.z);
            }
        }
    }
}
