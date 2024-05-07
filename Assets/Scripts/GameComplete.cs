using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameComplete : MonoBehaviour
{
    [SerializeField]
    private Sprite[] doorSprite;
    private int doorOpeningStages = 0;

    public void UpdateDoorOpeningStage() 
    {
        doorOpeningStages++;
        Debug.Log("Door opening stage updated " + doorOpeningStages);
        DoorOpeningStage();
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
}
