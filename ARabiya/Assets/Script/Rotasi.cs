using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotasi : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Objek yang akan dirotasi. Jika kosong, akan merotasi objek ini sendiri")]
    public GameObject targetObject;
    
    [Tooltip("Kecepatan rotasi (derajat per detik)")]
    public float rotationSpeed = 45f;
    
    [Tooltip("Apakah sedang dalam mode rotasi")]
    private bool isRotating = false;

    void Start()
    {
        // Jika targetObject tidak diset, gunakan objek ini sendiri
        if (targetObject == null)
        {
            targetObject = this.gameObject;
        }
    }

    void Update()
    {
        // Rotasi terus-menerus jika dalam mode rotasi
        if (isRotating && targetObject != null)
        {
            targetObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    // Method ini dipanggil ketika tombol rotasi ditekan (Toggle on/off)
    public void ToggleRotation()
    {
        isRotating = !isRotating;
        
        if (isRotating)
        {
            Debug.Log("Rotasi dimulai");
        }
        else
        {
            Debug.Log("Rotasi dihentikan");
        }
    }

    // Method untuk memulai rotasi
    public void StartRotation()
    {
        isRotating = true;
    }

    // Method untuk menghentikan rotasi
    public void StopRotation()
    {
        isRotating = false;
    }
}
