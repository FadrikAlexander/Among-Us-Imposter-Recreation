using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] Button DoorButton;

    //Just for show the button Black Overlay
    [SerializeField] Image DoorButtonImage;

    //All the doors in the System
    [SerializeField] List<GameObject> Doors;

    //To check if the system is open or closed
    bool isOpen = true;

    void Awake()
    {
        //open all the doors in the start
        OpenDoors();

        //Add a function to the door button to close when clicked
        //Can be done in the Inspector for each Button but this is much cleaner
        DoorButton.onClick.AddListener(() => CloseDoors());
    }

    //Called by pressing the door button
    public void CloseDoors()
    {
        //open all doors and this if you have a door with animation can be swtiched to activate that
        foreach (GameObject door in Doors)
            door.SetActive(true);

        //Disable the Sabotage button for 10sec
        FindObjectOfType<SabotageController>().DoorsClosed();
        isOpen = false;

        GetComponent<AudioSource>().Play();

        //Start the button CoolDown
        StartCoroutine(DoorsCoolDown(30f));
    }

    //Called when the time is done
    void OpenDoors()
    {
        foreach (GameObject door in Doors)
            door.SetActive(false);

        isOpen = true;
    }

    IEnumerator DoorsCoolDown(float coolDown)
    {
        DoorButton.interactable = false;

        //Start the Timer
        float TimeLeft = coolDown;
        while (TimeLeft != 0)
        {
            //Change the Image Fill amount
            DoorButtonImage.fillAmount = TimeLeft / coolDown;

            //When reaching half the timer open the doors
            if (TimeLeft < coolDown / 2 && !isOpen)
                OpenDoors();

            yield return new WaitForSeconds(1);
            TimeLeft--;
        }

        //Activate the Button when the timer is over
        DoorButton.interactable = true;
        DoorButtonImage.fillAmount = 0;
    }

    //Called from the SabotageController to Diable the Door Buttons when a Sabotage is started
    public void DisableDoors(float coolDown)
    {
        DoorButton.interactable = false;

        StopAllCoroutines();
        StartCoroutine(DoorsCoolDown(coolDown));
    }
}
