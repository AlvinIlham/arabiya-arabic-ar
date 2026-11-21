using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] options;
    public int correctIndex;
}

public class QuizManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI[] optionTexts;
    public Button nextButton;
    public Button prevButton;
    public Button submitButton;
    public Button homeButton;
    public GameObject scorePanel;
    public TextMeshProUGUI scoreText;

    [Header("Quiz Data")]
    public Question[] questions;

    private int currentQuestionIndex = 0;
    private int[] selectedAnswers; // Menyimpan jawaban user

    void Start()
    {
        selectedAnswers = new int[questions.Length];
        for (int i = 0; i < selectedAnswers.Length; i++) selectedAnswers[i] = -1;

        // Pastikan score panel tersembunyi saat game dimulai
        scorePanel.SetActive(false);

        LoadQuestion();
    }

    public void LoadQuestion()
    {
        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        // Array label untuk opsi A, B, C, D
        string[] optionLabels = { "A", "B", "C", "D" };

        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < q.options.Length)
            {
                // Set text dengan format "A. jawaban", "B. jawaban", dst
                optionTexts[i].text = $"{optionLabels[i]}. {q.options[i]}";
                optionButtons[i].gameObject.SetActive(true);
                optionTexts[i].gameObject.SetActive(true);
                
                // Reset warna teks ke default
                optionTexts[i].color = Color.black;
            }
            else
            {
                // Sembunyikan button dan text jika tidak ada opsi untuk index ini
                optionButtons[i].gameObject.SetActive(false);
                optionTexts[i].gameObject.SetActive(false);
                continue;
            }

            int index = i; // untuk closure lambda
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => SelectOption(index));
            
            // Pastikan button dapat ditekan
            optionButtons[i].interactable = true;
        }

        UpdateOptionColors();
        UpdateButtonVisibility();
    }

    public void SelectOption(int index)
    {
        selectedAnswers[currentQuestionIndex] = index;
        UpdateOptionColors();
    }

    void UpdateOptionColors()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            // Update warna button
            ColorBlock cb = optionButtons[i].colors;
            if (selectedAnswers[currentQuestionIndex] == i)
            {
                cb.normalColor = Color.green;
                // Update warna teks option yang dipilih
                optionTexts[i].color = Color.white; // Teks putih untuk kontras dengan background hijau
            }
            else
            {
                cb.normalColor = Color.white;
                // Update warna teks option yang tidak dipilih
                optionTexts[i].color = Color.black; // Teks hitam untuk kontras dengan background putih
            }
            optionButtons[i].colors = cb;
        }
    }

    void UpdateButtonVisibility()
    {
        // Soal nomor 1 (index 0): hanya tombol Next
        if (currentQuestionIndex == 0)
        {
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            homeButton.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(false);
        }
        // Soal terakhir (index questions.Length-1): hanya tombol Previous dan Submit
        else if (currentQuestionIndex == questions.Length - 1)
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
            homeButton.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(true);
        }
        // Soal nomor 2-9 (index 1 sampai questions.Length-2): tombol Previous dan Next
        else
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(false);
            homeButton.gameObject.SetActive(false);
        }
    }

    public void NextQuestion()
    {
        if (currentQuestionIndex < questions.Length - 1)
        {
            currentQuestionIndex++;
            LoadQuestion();
        }
    }

    public void PrevQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            LoadQuestion();
        }
    }

    public void SubmitQuiz()
    {
        int score = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            if (selectedAnswers[i] == questions[i].correctIndex)
                score++;
        }

        scorePanel.SetActive(true);
        scoreText.text = $"Your score: {score} / {questions.Length}";
    }

    public void LoadToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
