using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject intreactPanel;

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

            if (Input.GetKeyDown(KeyCode.E)) 
            {
                animator.SetTrigger("SwitchTurnOn");
            }
        }
    }
}
