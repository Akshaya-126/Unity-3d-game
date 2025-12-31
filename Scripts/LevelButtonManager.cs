using UnityEngine;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    [Header("Level 1 Stars")]
    public Image level1Star1;
    public Image level1Star2;
    public Image level1Star3;

    [Header("Level 2 Stars")]
    public Image level2Star1;
    public Image level2Star2;
    public Image level2Star3;

    [Header("Level 3 Stars")]
    public Image level3Star1;
    public Image level3Star2;
    public Image level3Star3;

    [Header("Level Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    [Header("Star Sprites")]
    public Sprite emptyStar;
    public Sprite fullStar;

    void Start()
    {
        SetStarSizes();     // Sets all stars to 50x50
        ShowLevelStars();
        UnlockLevels();     // FIXED unlocking logic
    }

    void ShowLevelStars()
    {
        // ⭐ LEVEL 1
        int l1 = PlayerPrefs.GetInt("Level1Stars", 0);
        level1Star1.sprite = (l1 >= 1) ? fullStar : emptyStar;
        level1Star2.sprite = (l1 >= 2) ? fullStar : emptyStar;
        level1Star3.sprite = (l1 >= 3) ? fullStar : emptyStar;

        // ⭐ LEVEL 2
        int l2 = PlayerPrefs.GetInt("Level2Stars", 0);
        level2Star1.sprite = (l2 >= 1) ? fullStar : emptyStar;
        level2Star2.sprite = (l2 >= 2) ? fullStar : emptyStar;
        level2Star3.sprite = (l2 >= 3) ? fullStar : emptyStar;

        // ⭐ LEVEL 3
        int l3 = PlayerPrefs.GetInt("Level3Stars", 0);
        level3Star1.sprite = (l3 >= 1) ? fullStar : emptyStar;
        level3Star2.sprite = (l3 >= 2) ? fullStar : emptyStar;
        level3Star3.sprite = (l3 >= 3) ? fullStar : emptyStar;
    }

    // ⭐ FIXED UNLOCKING SYSTEM
    void UnlockLevels()
    {
        // Level 1 is always unlocked
        level1Button.interactable = true;

        // ⭐ Level 2 unlocks if Level 1 has ANY stars
        bool level2Unlocked = PlayerPrefs.GetInt("Level1Stars", 0) > 0;
        level2Button.interactable = level2Unlocked;

        // ⭐ Level 3 unlocks if Level 2 has ANY stars
        bool level3Unlocked = PlayerPrefs.GetInt("Level2Stars", 0) > 0;
        level3Button.interactable = level3Unlocked;
    }

    // ⭐ Set all stars to 50x50 and disable preserveAspect
    void SetStarSizes()
    {
        Vector2 fixedSize = new Vector2(50, 50);
        Image[] stars =
        {
            level1Star1, level1Star2, level1Star3,
            level2Star1, level2Star2, level2Star3,
            level3Star1, level3Star2, level3Star3
        };

        foreach (Image img in stars)
        {
            img.rectTransform.sizeDelta = fixedSize;
            img.preserveAspect = false;   // VERY IMPORTANT
        }
    }
}
