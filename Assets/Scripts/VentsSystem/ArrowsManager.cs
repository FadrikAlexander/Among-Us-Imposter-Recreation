using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsManager : MonoBehaviour
{
    //Make sure the number of arrows is greater than the biggest vents system
    [SerializeField] List<GameObject> arrows;
    int index = 0;

    //Other Classes
    VentsSystem ventsSystem;

    void Awake()
    {
        //Reset and Remove Arrows from the Start
        ResetArrows();
    }

    //Called When the Vents System is Entered or moved to a new Vent
    public void VentEntered(VentsSystem ventsSystem, int CurrentVentID, List<Vent> connectedVents)
    {
        this.ventsSystem = ventsSystem;

        //Go through all the vents and set the new arrows
        foreach (Vent vent in connectedVents)
        {
            if (vent.ID != CurrentVentID)
                SetArrow(connectedVents[CurrentVentID].GetPos(), vent.GetPos(), vent.ID);
        }
    }

    //This Class to set a new Arrow for the new Vent
    void SetArrow(Vector3 ventPosition, Vector3 nextVentPosition, int VentID)
    {
        //Activate the Arrow
        arrows[index].SetActive(true);

        //Get the angle between the current Vent and the next one
        Vector3 arrowRotation = Vector3.zero;
        arrowRotation.z = -AngleDegrees(ventPosition - nextVentPosition);
        arrows[index].GetComponent<RectTransform>().localEulerAngles = arrowRotation;

        //Set the Arrow Button for the function in the VentsSystem to move to a new vent when clicked by the provided ID
        arrows[index].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        arrows[index].GetComponentInChildren<Button>().onClick.AddListener(() => ventsSystem.MoveToVent(VentID));

        //Move to the Next Arrow in the List
        index++;
    }

    //Reset All Arrows called from the VentsSystem when a new Vent is moved to
    public void ResetArrows()
    {
        index = 0;
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
            arrow.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
    }

    //Math function to calculate the Angle between vents 
    public float AngleDegrees(Vector3 p_vector2)
    {
        return (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg) + 180;
    }
}
