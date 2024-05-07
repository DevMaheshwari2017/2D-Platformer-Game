
using UnityEngine;
public class GameManager : GenericMonoSingelton<GameManager>
{
    [SerializeField]
    private SpawnManager spawnManager;
    [SerializeField]
    private SoundManager soundManager;
    [SerializeField]
    private playerController playerController;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private UI_Manager uiManager;


    public void SetUIManager( UI_Manager uI_Manager) 
    {
        uiManager = uI_Manager;
    }
    public void SetPlayerController(playerController _playerController) 
    {
        playerController = _playerController;
    }
    public playerController GetPlayerController() => playerController;
    public SpawnManager GetSpawnManager() => spawnManager;
    public SoundManager GetSoundManager() => soundManager;
    public ScoreManager GetScoreManager() => scoreManager;

    public UI_Manager GetUIManager() => uiManager;
}
