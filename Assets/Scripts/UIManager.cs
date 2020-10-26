using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// The Script that contain functions to be applied before running the game
public class UIManager : MonoBehaviour
{

#if UNITY_EDITOR
    //to allow the function to appear in the top menu
    [MenuItem("UI System/Auto Anchor")]
    static void PutAnchorAroundUIElement()
    {
		//Get the selected GameObject
        GameObject UIObj = Selection.activeGameObject;

		//Check if any object is selected and the object is a UI gameobject
        if (UIObj != null && UIObj.GetComponent<RectTransform>() != null)
        {
			//get the object and it's parent RectTransform
            RectTransform UIObj_RectTransform = UIObj.GetComponent<RectTransform>();
            RectTransform UIObj_RectTransform_Parent = UIObj.transform.parent.GetComponent<RectTransform>();

			//Calculate the size of the GameObject
            Vector2 UIObj_offsetMin = UIObj_RectTransform.offsetMin;
            Vector2 UIObj_offsetMax = UIObj_RectTransform.offsetMax;

			//Get the Anchor positions
            Vector2 AnchorMin = UIObj_RectTransform.anchorMin;
            Vector2 AnchorMax = UIObj_RectTransform.anchorMax;

			//Get the Parent size
            float Parent_width = UIObj_RectTransform_Parent.rect.width;
            float Parent_height = UIObj_RectTransform_Parent.rect.height;

			//Calculate the new Anchor positions
            Vector2 AnchorMin_new = new Vector2(AnchorMin.x + (UIObj_offsetMin.x / Parent_width), AnchorMin.y + (UIObj_offsetMin.y / Parent_height));
            Vector2 AnchorMax_new = new Vector2(AnchorMax.x + (UIObj_offsetMax.x / Parent_width), AnchorMax.y + (UIObj_offsetMax.y / Parent_height));

			//Set the new Anchor positions to the GameObject Anchors
            UIObj_RectTransform.anchorMin = AnchorMin_new;
            UIObj_RectTransform.anchorMax = AnchorMax_new;

			//Set the GameObject inside the new Anchors
            UIObj_RectTransform.offsetMin = new Vector2(0, 0);
            UIObj_RectTransform.offsetMax = new Vector2(1, 1);
            UIObj_RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
#endif

}
