using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private GameComplete gameComplete;
    [SerializeField]
    private GameObject intreactPanel;
    [SerializeField]
    private Sprite switchTurnedOnSprite;

    private bool intereacted = false;

    private void Start()
    {
        intreactPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController playerController = collision.gameObject.GetComponent<playerController>();
        if (playerController != null)
        {
            intreactPanel.SetActive(true);
        }
    }

    private void TurnOn() 
    {
        if (!intreactPanel.activeInHierarchy)
            return;

        if (intereacted == true)
            return;

            if (Input.GetKeyDown(KeyCode.E)) 
            {
               gameObject.GetComponent<SpriteRenderer>().sprite = switchTurnedOnSprite;
               gameComplete.UpdateDoorOpeningStage();
               intereacted = true;
            }               
      
    }
    private void Update()
    {
        TurnOn();
    }
}
