using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
        public float hareketHizi = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Yatay ve dikey girişleri al
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        // Hareket vektörünü oluştur
        Vector3 hareket = new Vector3(yatay, 0f, dikey) * hareketHizi * Time.deltaTime;

        // Hareketi uygula
        transform.Translate(hareket);
    }
}
