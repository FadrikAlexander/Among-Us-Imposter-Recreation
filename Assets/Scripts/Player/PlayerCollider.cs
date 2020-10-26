using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    //This Class is the collider controller class for the player

    PlayerMovement playerMovement;
    PlayerKillController playerKillController;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerKillController = GetComponent<PlayerKillController>();
    }

    #region Triggers
    void OnTriggerEnter2D(Collider2D other)
    {
        //When the Imposter Enters the Vent Area
        if (other.gameObject.tag == "Vent")
        {
            //If the Imposter is activated skip
            //This is used to move the imposter around without activating the triggers
            if (playerMovement.IsInVent())
            {
                other.gameObject.GetComponent<Vent>().EnableVent(playerMovement);
            }
        }

        //When the Imposter Enters a crewmember Trigger
        if (other.gameObject.tag == "Crewmember")
        {
            //If the Imposter is activated skip
            //to not allow killing from the vent
            if (playerMovement.IsInVent())
            {
                playerKillController.EnableKilling(other.GetComponent<PlayerKillDetector>());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //When the Imposter Exits the Vent Area
        if (other.gameObject.tag == "Vent")
        {
            //If the Imposter is activated skip
            //to not allow killing from the vent
            if (playerMovement.IsInVent())
                other.gameObject.GetComponent<Vent>().DisableVent();
        }

        //When the Imposter Exit a crewmember Trigger
        if (other.gameObject.tag == "Crewmember")
        {
            //If the Imposter is activated skip
            //This is used to move the imposter around without activating the triggers
            if (playerMovement.IsInVent())
            {
                playerKillController.DisableKilling();
            }
        }
    }
    #endregion 
}
