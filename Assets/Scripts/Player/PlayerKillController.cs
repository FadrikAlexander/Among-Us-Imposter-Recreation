using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKillController : MonoBehaviour
{
    //The UI button with the Overlay color
    [SerializeField] Button KillButton;
    [SerializeField] Image KillButtonImage;
    [SerializeField] Text KillButtonCoolDownText;

    [SerializeField] float KillCoolDown = 10;
    bool canKill = true;

    PlayerKillDetector playerKillDetector;

    void Awake()
    {
        KillButtonCoolDownText.text = "";
        KillButtonImage.fillAmount = 1;
    }

    IEnumerator ResetKill()
    {
        //Reset the Variables
        canKill = false;
        KillButton.interactable = false;
        KillButtonCoolDownText.text = "" + KillCoolDown;
        KillButtonImage.fillAmount = 0;

        //Start the Timer
        float TimeLeft = KillCoolDown;
        while (TimeLeft != 0)
        {
            yield return new WaitForSeconds(1);
            TimeLeft--;

            //Change the Time Text and the Image Fill Amount
            KillButtonCoolDownText.text = "" + TimeLeft;
            KillButtonImage.fillAmount = 1 - TimeLeft / KillCoolDown;
        }

        //if the current Player in range is Alive
        if (playerKillDetector != null && playerKillDetector.isAlive)
            KillButton.interactable = true;

        //Set variables for the next kill
        KillButtonCoolDownText.text = "";
        KillButtonImage.fillAmount = 1;
        canKill = true;
    }

    //Called when the kill button is pressed
    public void Kill()
    {
        //Send notification for the player to be killed
        playerKillDetector.Killed();

        //Start the CoolDown
        StartCoroutine(ResetKill());
    }

    #region Triggers

    //Called when a player enter the player collider
    public void EnableKilling(PlayerKillDetector playerKillDetector)
    {
        this.playerKillDetector = playerKillDetector;

        //if the cooldown is 0
        if (canKill)
            KillButton.interactable = true;
    }

    //Called when a player exit the player collider
    public void DisableKilling()
    {
        playerKillDetector = null;
        KillButton.interactable = false;
    }

    #endregion
}
