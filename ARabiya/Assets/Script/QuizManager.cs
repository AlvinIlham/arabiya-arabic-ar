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
    private int[] selectedAnswers;
    private Question[] shuffledQuestions;

    void Start()
    {
        // Validasi awal
        if (questions == null || questions.Length == 0)
        {
            Debug.LogError("QUESTIONS belum diisi!");
            return;
        }

        // Acak urutan soal
        ShuffleQuestions();

        if (optionButtons == null || optionTexts == null)
        {
            Debug.LogError("OptionButtons atau OptionTexts belum di-assign di Inspector!");
            return;
        }

        if (optionButtons.Length != optionTexts.Length)
        {
            Debug.LogError("Jumlah optionButtons dan optionTexts TIDAK sama!");
        }

        selectedAnswers = new int[shuffledQuestions.Length];
        for (int i = 0; i < selectedAnswers.Length; i++) selectedAnswers[i] = -1;

        scorePanel.SetActive(false);
        LoadQuestion();
    }

    void ShuffleQuestions()
    {
        shuffledQuestions = new Question[questions.Length];
        System.Array.Copy(questions, shuffledQuestions, questions.Length);

        // Fisher-Yates shuffle algorithm
        for (int i = shuffledQuestions.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Question temp = shuffledQuestions[i];
            shuffledQuestions[i] = shuffledQuestions[randomIndex];
            shuffledQuestions[randomIndex] = temp;
        }
    }

    public void LoadQuestion()
    {
        if (shuffledQuestions[currentQuestionIndex].options == null ||
            shuffledQuestions[currentQuestionIndex].options.Length == 0)
        {
            Debug.LogError("Options pada soal index " + currentQuestionIndex + " kosong!");
            return;
        }

        Question q = shuffledQuestions[currentQuestionIndex];
        questionText.text = q.questionText;

        string[] optionLabels = { "A", "B", "C", "D" };

        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < q.options.Length)
            {
                optionTexts[i].text = $"{optionLabels[i]}. {q.options[i]}";
                optionButtons[i].gameObject.SetActive(true);
                optionTexts[i].gameObject.SetActive(true);

                // default warna
                optionTexts[i].color = Color.black;
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
                optionTexts[i].gameObject.SetActive(false);
                continue;
            }

            int index = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => SelectOption(index));

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
    Color yellow = new Color(1f, 0.92f, 0.016f); 
    Color brown = new Color(0.4f, 0.2f, 0.05f);
    float fadeTime = 0.05f; // TRANSISI CEPAT

    for (int i = 0; i < optionButtons.Length; i++)
    {
        if (optionButtons[i] == null || optionTexts[i] == null) continue;

        ColorBlock cb = optionButtons[i].colors;
        cb.fadeDuration = fadeTime;

        if (selectedAnswers[currentQuestionIndex] == i)
        {
            cb.normalColor = yellow;
            cb.highlightedColor = yellow;
            cb.pressedColor = yellow;
            cb.selectedColor = yellow;

            optionTexts[i].color = brown;
        }
        else
        {
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.white;
            cb.pressedColor = Color.white;
            cb.selectedColor = Color.white;

            optionTexts[i].color = Color.black;
        }

        optionButtons[i].colors = cb;
    }
}

    void UpdateButtonVisibility()
    {
        if (currentQuestionIndex == 0)
        {
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            homeButton.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(false);
        }
        else if (currentQuestionIndex == shuffledQuestions.Length - 1)
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
            homeButton.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(true);
        }
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
        if (currentQuestionIndex < shuffledQuestions.Length - 1)
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
        for (int i = 0; i < shuffledQuestions.Length; i++)
        {
            if (selectedAnswers[i] == shuffledQuestions[i].correctIndex)
                score++;
        }

        scorePanel.SetActive(true);
        scoreText.text = $"Your score: {score} / {shuffledQuestions.Length}";
    }

    public void LoadToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
