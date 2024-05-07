using System.Collections;
using UnityEngine;

public class GameComplete : MonoBehaviour
{
    [SerializeField]
    private float holdCameraTimer;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Sprite[] doorSprite;
    [SerializeField]
    private Animator animator;


    private int doorOpeningStages = 0;

    public void UpdateDoorOpeningStage() 
    {
        doorOpeningStages++;
        Debug.Log("Door opening stage updated " + doorOpeningStages);
        DoorOpeningStage();
        ShowDoor();
    }

    private void ShakingAnim() 
    {
        animator.SetTrigger("DoorShaking");
    }
    private void DoorOpeningStage() 
    {
        Debug.Log("Door Sprite chnaging");
        Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        switch (doorOpeningStages) 
        {
            case 1:
                Debug.Log("Door Sprite chnaged");
                sprite = doorSprite[1];
                break;
            case 2: sprite = doorSprite[2];
                break;
            case 3: sprite = doorSprite[3];
                break;
            default: sprite = doorSprite[0];
                break;
        }
       gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void DisablePlayerCanvas(playerController playerController) 
    {
        playerController.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
    }
    private void ShowDoor() 
    {
        cam.gameObject.SetActive(true);
        Debug.Log("Showing camera" + cam.gameObject.activeInHierarchy);
        ShakingAnim();
        StartCoroutine(WaitForCameraToReturn());
    }

    IEnumerator WaitForCameraToReturn() 
    {
        Debug.Log("Waiting for camera to deactiavte " + holdCameraTimer);
        yield return new WaitForSeconds(holdCameraTimer);
        cam.gameObject.SetActive(false);
        Debug.Log("Showing camera" + cam.gameObject.activeInHierarchy);
    }
}
