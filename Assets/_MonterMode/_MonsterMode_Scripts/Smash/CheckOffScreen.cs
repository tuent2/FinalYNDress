using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOffScreen : MonoBehaviour
{
    //    public float timeThreshold = 3f; // Thời gian ngưỡng để đưa ra cảnh báo
    //    private float timer = 0f;
    //    private Vector3 screenBounds;
    //    private void Start()
    //    {
    //        UpdateScreenBounds();
    //    }

    //    public void UpdateScreenBounds()
    //    {
    //        float screenHalfHeight = Camera.main.orthographicSize;
    //        float screenHalfWidth = screenHalfHeight * Camera.main.aspect;
    //        screenBounds = new Vector3(screenHalfWidth, screenHalfHeight, 0f);
    //    }

    //    void Update()
    //    {
    //        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

    //        if (screenPos.x < -screenBounds.x + 0.5f || screenPos.x > screenBounds.x - 0.5f || screenPos.y < -screenBounds.y + 2.5f || screenPos.y > screenBounds.y - 0.5f)
    //        {
    //            timer += Time.deltaTime;
    //            if (timer > timeThreshold)
    //            {
    //                //Debug.Log("GameObject nằm ngoài màn hình quá 5 giây!");
    //                timer = 0f; // Đặt lại timer để chuẩn bị cho lần tiếp theo
    //                SmashCharacterManager.THIS.ResetTransform();
    //            }
    //        }
    //        else
    //        {
    //            timer = 0f; 
    //        }
    //    }
}
