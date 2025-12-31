using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScore_Level3 : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI coinText, gemText, diamondText;
    public TextMeshProUGUI messageText, timerText, healthText;

    [Header("Settings")]
    public float messageDuration = 2f;
    public float gameTime = 60f;

    private int coinScore = 0, gemScore = 0, diamondScore = 0;
    private bool keyCollected = false, gameEnded = false;
    public int maxHealth = 5;
    private int currentHealth;

    private Coroutine messageCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        UpdateScoreUI();

        if (messageText != null)
            messageText.gameObject.SetActive(false);

        StartCoroutine(GameTimer());
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameEnded) return;

        if (other.CompareTag("Coin")) { coinScore += 1; other.gameObject.SetActive(false); }
        else if (other.CompareTag("Gem")) { gemScore += 2; other.gameObject.SetActive(false); }
        else if (other.CompareTag("Diamond")) { diamondScore += 5; other.gameObject.SetActive(false); }

        else if (other.CompareTag("Bomb"))
        {
            TakeDamage(1);
            coinScore = Mathf.Max(coinScore - 1, 0);
            gemScore = Mathf.Max(gemScore - 2, 0);
            diamondScore = Mathf.Max(diamondScore - 5, 0);
            other.gameObject.SetActive(false);
        }

        else if (other.CompareTag("Blade"))
        {
            TakeDamage(1);
        }

        // ⭐ LEVEL 3 COMPLETION CHECK
        else if (other.CompareTag("Key") && !keyCollected)
        {
            if (coinScore >= 5 && gemScore >= 6&& diamondScore >= 10)
            {
                keyCollected = true;
                other.gameObject.SetActive(false);
                ShowMessage("Level 3 Completed!");
                gameEnded = true;

                SaveLevel3Progress();  // Save only Level 3 stars

                StartCoroutine(LoadSceneAfterDelay("levels"));
            }
            else
            {
                ShowMessage("Need 5Coins, 3Gems, 2Diamonds!");
            }
        }

        UpdateScoreUI();
    }

    void SaveLevel3Progress()
    {
        int lostHealth = maxHealth - currentHealth;
        int stars = lostHealth == 0 ? 3 : lostHealth == 1 ? 2 : 1;

        PlayerPrefs.SetInt("Level3Stars", stars);   // ⭐ Only save Level 3 stars
        PlayerPrefs.Save();
    }

    void UpdateScoreUI()
    {
        if (coinText != null) coinText.text = coinScore.ToString();
        if (gemText != null) gemText.text = gemScore.ToString();
        if (diamondText != null) diamondText.text = diamondScore.ToString();
    }

    void UpdateHealthUI()
    {
        if (healthText != null) healthText.text = "Health: " + currentHealth;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0 && !gameEnded)
        {
            gameEnded = true;
            ShowMessage("You Died!");
            PlayerPrefs.SetInt("Level3Stars", 0);
            PlayerPrefs.Save();
            StartCoroutine(LoadSceneAfterDelay("levels"));
        }
    }

    private void ShowMessage(string msg)
    {
        if (messageText == null) return;
        if (messageCoroutine != null) StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(DisplayMessage(msg));
    }

    private IEnumerator DisplayMessage(string msg)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messageText.gameObject.SetActive(false);
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(messageDuration);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator GameTimer()
    {
        float remainingTime = gameTime;

        while (remainingTime > 0 && !gameEnded)
        {
            remainingTime -= Time.deltaTime;

            if (timerText != null)
                timerText.text = "Time: " + Mathf.CeilToInt(remainingTime);

            yield return null;
        }

        if (!gameEnded)
        {
            gameEnded = true;
            ShowMessage("Time's Up! Game Over!");
            PlayerPrefs.SetInt("Level3Stars", 0);
            PlayerPrefs.Save();
            StartCoroutine(LoadSceneAfterDelay("levels"));
        }
    }
}
