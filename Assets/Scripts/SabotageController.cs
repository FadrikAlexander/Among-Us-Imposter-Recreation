using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SabotageController : MonoBehaviour
{
    [SerializeField] List<Button> sabotageButtons;

    //Just for show the button Black Overlay
    [SerializeField] List<Image> sabotageButtonsImages;

    //It's true when all the buttons are Disabled
    bool buttonsSabotaged = false;

    void Awake()
    {
        ActivateSabotages();
    }

    #region SabotageFunctions
    //These functions should be replaced by the real stuff we need to sabotage
    //it just play a sound and print a message
    public void SabotageO2()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("O2 Sabotaged");
        DisableSabotages(30);
    }
    public void SabotageComms()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("Communications Sabotaged");
        DisableSabotages(30);
    }
    public void SabotageReactor()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("Reactor Sabotaged");
        DisableSabotages(30);
    }
    public void SabotageLights()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("Lights Sabotaged");
        DisableSabotages(30);
    }
    #endregion

    //Called when a Sabotage start/Button clicked
    public void DisableSabotages(float coolDownAmount)
    {
        if (!buttonsSabotaged)
        {
            buttonsSabotaged = true;

            //Disable the Doors buttons for 10 sec after the Sabotage
            foreach (DoorSystem doorSystem in FindObjectsOfType<DoorSystem>())
                doorSystem.DisableDoors(10);

            StartCoroutine(SabotagingButtons(coolDownAmount));
        }
    }

    //Called when a Door is closed to diactivate the sabotage buttons for 10sec
    public void DoorsClosed()
    {
        if (!buttonsSabotaged)
        {
            StopAllCoroutines();
            StartCoroutine(SabotagingButtons(10));
        }
    }

    //Called to activate the sabotage buttons
    void ActivateSabotages()
    {
        buttonsSabotaged = false;

        //Activating Buttons
        foreach (Button button in sabotageButtons)
            button.interactable = true;
    }

    //Just a timer for Disabling the Buttons
    IEnumerator SabotagingButtons(float coolDownAmount)
    {
        //Disable all Buttons
        foreach (Button button in sabotageButtons)
            button.interactable = false;

        //Start a timer from the CoolDownAmount
        float TimeLeft = coolDownAmount;
        while (TimeLeft != 0)
        {
            //Change the fill amount of all images
            foreach (Image img in sabotageButtonsImages)
                img.fillAmount = TimeLeft / coolDownAmount;

            //Move the time by one sec
            yield return new WaitForSeconds(1);
            TimeLeft--;
        }

        //Activte all Buttons
        ActivateSabotages();

        foreach (Image img in sabotageButtonsImages)
            img.fillAmount = 0;
    }
}
