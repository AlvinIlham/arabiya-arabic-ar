using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scale : MonoBehaviour
{
    [Header("Scale Settings")]
    [Tooltip("Objek yang akan di-scale. Jika kosong, akan menggunakan objek ini sendiri")]
    public GameObject targetObject;
    
    [Tooltip("Nilai perubahan scale setiap kali tombol ditekan")]
    public float scaleIncrement = 0.1f;
    
    [Tooltip("Batas minimum scale (agar objek tidak terlalu kecil)")]
    public float minScale = 0.1f;
    
    [Tooltip("Batas maximum scale (agar objek tidak terlalu besar)")]
    public float maxScale = 5f;
    
    [Tooltip("Durasi animasi scale (dalam detik)")]
    public float scaleDuration = 0.3f;
    
    private bool isScaling = false;
    private Vector3 targetScale;

    void Start()
    {
        // Jika targetObject tidak diset, gunakan objek ini sendiri
        if (targetObject == null)
        {
            targetObject = this.gameObject;
        }
        
        targetScale = targetObject.transform.localScale;
    }

    // Method untuk memperbesar objek (dipanggil dari tombol +)
    public void ScaleUp()
    {
        if (!isScaling)
        {
            Vector3 newScale = targetObject.transform.localScale + (Vector3.one * scaleIncrement);
            
            // Cek batas maksimum
            if (newScale.x <= maxScale && newScale.y <= maxScale && newScale.z <= maxScale)
            {
                StartCoroutine(ScaleToSize(newScale));
            }
            else
            {
                Debug.Log("Ukuran maksimum tercapai!");
            }
        }
    }

    // Method untuk memperkecil objek (dipanggil dari tombol -)
    public void ScaleDown()
    {
        if (!isScaling)
        {
            Vector3 newScale = targetObject.transform.localScale - (Vector3.one * scaleIncrement);
            
            // Cek batas minimum
            if (newScale.x >= minScale && newScale.y >= minScale && newScale.z >= minScale)
            {
                StartCoroutine(ScaleToSize(newScale));
            }
            else
            {
                Debug.Log("Ukuran minimum tercapai!");
            }
        }
    }

    // Coroutine untuk animasi scale yang smooth
    private IEnumerator ScaleToSize(Vector3 newScale)
    {
        isScaling = true;
        
        Vector3 startScale = targetObject.transform.localScale;
        float elapsedTime = 0f;
        
        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / scaleDuration;
            
            // Smooth interpolation
            targetObject.transform.localScale = Vector3.Lerp(startScale, newScale, t);
            
            yield return null;
        }
        
        // Pastikan scale final tepat
        targetObject.transform.localScale = newScale;
        
        isScaling = false;
    }

    // Method alternatif untuk scale instant (tanpa animasi)
    public void ScaleUpInstant()
    {
        Vector3 newScale = targetObject.transform.localScale + (Vector3.one * scaleIncrement);
        
        if (newScale.x <= maxScale && newScale.y <= maxScale && newScale.z <= maxScale)
        {
            targetObject.transform.localScale = newScale;
        }
    }

    public void ScaleDownInstant()
    {
        Vector3 newScale = targetObject.transform.localScale - (Vector3.one * scaleIncrement);
        
        if (newScale.x >= minScale && newScale.y >= minScale && newScale.z >= minScale)
        {
            targetObject.transform.localScale = newScale;
        }
    }

    // Method untuk reset scale ke ukuran awal
    public void ResetScale()
    {
        if (!isScaling)
        {
            StartCoroutine(ScaleToSize(Vector3.one));
        }
    }
}
