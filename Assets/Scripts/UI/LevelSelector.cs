using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private Button[] levelButtons;

    private int levelPlayable = 1 ;

    public static LevelSelector Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        levelPlayable = PlayerPrefs.GetInt("levelPlayable", 1);
        FindAndInitializeButtons();
        UpdateLevelButtons();
    }

    public void FindAndInitializeButtons()
    {
        // Similar to InitializeButtons, find and assign buttons
        Debug.Log("Inside intialize level button");
        // Assuming buttons are tagged appropriately in the Unity Editor
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton").Select(obj => obj.GetComponent<Button>()).ToArray();
        if (levelButtons.Length == 0)
        {
            Debug.LogError("No level buttons found. Make sure they are tagged correctly.");
        }
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("Scene loaded: " + scene.name);
    //    InitializeButtons();
    //    UpdateLevelButtons();
    //}

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    //private void InitializeButtons()
    //{
    //    Debug.Log("Inside intialize level button");
    //    // Assuming buttons are tagged appropriately in the Unity Editor
    //    levelButtons = GameObject.FindGameObjectsWithTag("LevelButton").Select(obj => obj.GetComponent<Button>()).ToArray();
    //    if (levelButtons.Length == 0)
    //    {
    //        Debug.LogError("No level buttons found. Make sure they are tagged correctly.");
    //    }
    //}

    public void LevelUnlocked(int leveltounlock)
    {
        levelPlayable = leveltounlock;
        //PlayerPrefs.SetInt("levelPlayable", levelPlayable); // Save the new highest level
        //PlayerPrefs.Save();
        Debug.Log("New level unlocked: " + levelPlayable);       
    }

    public void UpdateLevelButtons()
    {
        FindAndInitializeButtons();
        Debug.Log("Inside update level button");
        //Debug.Log("Number of level buttons: " + levelButtons.Length); // Check the length of the array
        if (levelButtons.Length == 0)
        {
            Debug.LogError("Level buttons array is empty. Please assign the buttons in the inspector.");
            return;
        }
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Debug.Log("inside update level button for loop");
            if (i + 1 > levelPlayable)
            {
                levelButtons[i].interactable = false;
                Debug.Log("Level buton " + levelButtons[i] + " is Deactivated ");
            }
            else
            {
                Debug.Log("Level buton " + levelButtons[i] + " is activated ");
                levelButtons[i].interactable = true; // Ensure buttons up to the highest level are interactable
            }
        }
    }

    public void LoadLevel(int leveltoload)
    {
        SoundManager.Instance.SFXSounds(SoundTypes.OnClick);
        SceneManager.LoadScene(leveltoload);        
    }

    public void BackButton()
    {
        SoundManager.Instance.SFXSounds(SoundTypes.OnClick);
        gameObject.SetActive(false);
    }
}
