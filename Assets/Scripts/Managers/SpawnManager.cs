using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerPrefab;

    private GameObject SpawnedPlayer;

    public static SpawnManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        SpawingPlayer();
    }
    private void SpawingPlayer()
    {
       Debug.Log("SpawingPlayer");
       SpawnedPlayer = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        GameManager.Instance.SetPlayerController(SpawnedPlayer.GetComponent<playerController>());
        GameManager.Instance.SetUIManager(SpawnedPlayer.GetComponentInChildren<UI_Manager>());
       CameraFollow camerfollow = FindAnyObjectByType<CameraFollow>();

        if(camerfollow != null)
        {
            camerfollow.FollowingPlayer(SpawnedPlayer);
        }
    }
}
