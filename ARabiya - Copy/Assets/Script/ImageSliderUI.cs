using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class SlideElement
{
    [Header("Visual")]
    [Tooltip("Gambar untuk slide ini")]
    public Sprite slideImage;
    
    [Header("Audio")]
    [Tooltip("Audio clip untuk sound button 1")]
    public AudioClip sound1;
    [Tooltip("Audio clip untuk sound button 2")]
    public AudioClip sound2;
    [Tooltip("Audio clip untuk sound button 3")]
    public AudioClip sound3;
}

public class ImageSliderUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image displayImage;
    public Button nextButton;
    public Button prevButton;
    public Button homeButton;

    [Header("Sound Buttons")]
    public Button soundButton1;
    public Button soundButton2;
    public Button soundButton3;

    [Header("Slides")]
    [Tooltip("Daftar slide - setiap elemen berisi gambar dan 3 audio clip")]
    public List<SlideElement> slides = new List<SlideElement>();

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Scene Management")]
    [Tooltip("Masukkan nama scene yang akan dituju saat tombol Home ditekan")]
    public string homeSceneName = "MainMenu";

    private int currentIndex = 0;

    void Start()
    {
        ShowImage();
        UpdateButtons();

        // Setup audio source jika belum ada
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Hapus pemanggilan listener agar pakai OnClick() manual dari Inspector
        // nextButton.onClick.AddListener(NextImage);
        // prevButton.onClick.AddListener(PrevImage);
        // homeButton.onClick.AddListener(GoHome);
    }

    public void ShowImage()
    {
        if (slides.Count > 0 && currentIndex >= 0 && currentIndex < slides.Count)
            displayImage.sprite = slides[currentIndex].slideImage;
    }

    public void NextImage()
    {
        if (currentIndex < slides.Count - 1)
        {
            currentIndex++;
            ShowImage();
            UpdateButtons();
        }
    }

    public void PrevImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowImage();
            UpdateButtons();
        }
    }

    public void PlaySound1()
    {
        PlaySoundForCurrentPage(1);
    }

    public void PlaySound2()
    {
        PlaySoundForCurrentPage(2);
    }

    public void PlaySound3()
    {
        PlaySoundForCurrentPage(3);
    }

    private void PlaySoundForCurrentPage(int soundIndex)
    {
        if (audioSource == null || slides.Count == 0)
        {
            Debug.LogWarning("Audio Source atau Slides belum di-setup!");
            return;
        }

        if (currentIndex >= 0 && currentIndex < slides.Count)
        {
            SlideElement currentSlide = slides[currentIndex];
            AudioClip clipToPlay = null;

            switch (soundIndex)
            {
                case 1:
                    clipToPlay = currentSlide.sound1;
                    break;
                case 2:
                    clipToPlay = currentSlide.sound2;
                    break;
                case 3:
                    clipToPlay = currentSlide.sound3;
                    break;
            }

            if (clipToPlay != null)
            {
                audioSource.clip = clipToPlay;
                audioSource.Play();
                Debug.Log($"Playing sound {soundIndex} for slide {currentIndex + 1}");
            }
            else
            {
                Debug.LogWarning($"Sound {soundIndex} untuk slide {currentIndex + 1} belum di-assign!");
            }
        }
    }

    public void GoHome()
    {
        Debug.Log("Home button pressed - Loading scene: " + homeSceneName);
        
        // Pindah ke scene yang ditentukan di Inspector
        if (!string.IsNullOrEmpty(homeSceneName))
        {
            LoadToScene(homeSceneName);
        }
        else
        {
            Debug.LogWarning("Home Scene Name belum diset di Inspector!");
        }
    }

    public void LoadToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateButtons()
    {
        if (currentIndex == 0)
        {
            homeButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            prevButton.gameObject.SetActive(false);
        }
        else if (currentIndex == slides.Count - 1)
        {
            homeButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            prevButton.gameObject.SetActive(true);
        }
        else
        {
            homeButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            prevButton.gameObject.SetActive(true);
        }
    }
}
