
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

    public playerController GetPlayerController() => playerController;
    public SpawnManager GetSpawnManager() => spawnManager;
    public SoundManager GetSoundManager() => soundManager;
    public ScoreManager GetScoreManager() => scoreManager;
}
