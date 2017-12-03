using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Script that allows the ship icons to be dragged around the screen
/// </summary>
public class DragShips : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Public Variables
    public RectTransform draggableArea;

    public List<RectTransform> possibleTargets;
    #endregion

    #region Class Variables
    private Transform shipyard;
    private int goldAmount;
    private int listIndex;
    #endregion

    public int GoldAmount
    {
        get { return goldAmount; }
        set { goldAmount = value; }
    }

    public int ListIndex
    {
        get { return listIndex; }
        set { listIndex = value; }
    }

    /// <summary>
    /// Function called when the user starts to drag an object
    /// </summary>
    /// <param name="eventData">The data for the drag event</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Set parent to be the draggable area and move it to the front of the other UI elements
        transform.SetParent(draggableArea, false);
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// Function called during every frame that the object is being dragged
    /// </summary>
    /// <param name="eventData">The data for the drag event</param>
    public void OnDrag(PointerEventData eventData)
    {
        //Set the position to the position of the mouse
        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// Function called when the user stops dragging the object
    /// </summary>
    /// <param name="eventData">The data for the drag event</param>
    public void OnEndDrag(PointerEventData eventData)
    {

        //Loop through each of the possible targets and see if the object is overlapping
        foreach (RectTransform r in possibleTargets)
        {
            //If the object overlaps, set its parent and snap it in place
            if (RectOverlaps((RectTransform)transform, r))
            {
                if(r.GetComponent<MinigameSelect>().IsSet)
                {
                    break;
                }
                else
                {
                    transform.SetParent(r);
                    transform.localPosition = new Vector3(0, 0, transform.parent.localPosition.z);
                    r.GetComponentInChildren<Text>().enabled = false;
                    r.GetComponent<MinigameSelect>().GoldOnShip = goldAmount;
                    r.GetComponent<MinigameSelect>().IsSet = true;
                    r.GetComponent<MinigameSelect>().ListIndex = listIndex;

                    if(shipyard.gameObject.tag.Equals("Player1"))
                    {
                        r.GetComponent<MinigameSelect>().WhichPlayer = PlayerAdvantage.Player1;
                    }
                    else
                    {
                        r.GetComponent<MinigameSelect>().WhichPlayer = PlayerAdvantage.Player2;
                    }
                    break;
                }

            }
        }

        //If the object is not overlapping anything, put it back in the shipyard
        if (transform.parent == draggableArea)
        {
            transform.SetParent(shipyard);
        }
    }

    // Use this for initialization
    void Start ()
    {
        shipyard = transform.parent;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Helper method that checks of two rectangle transforms are overlapping
    /// </summary>
    /// <param name="rTrans1">The first rectangle transform</param>
    /// <param name="rTrans2">The second rectangle transform</param>
    /// <returns>Are they overlapping?</returns>
    private bool RectOverlaps(RectTransform rTrans1, RectTransform rTrans2)
    {
        //Create a rectangle out of each rect transform
        Rect r1 = new Rect(rTrans1.position.x, rTrans1.position.y, rTrans1.rect.width, rTrans1.rect.height);
        Rect r2 = new Rect(rTrans2.position.x, rTrans2.position.y, rTrans2.rect.width, rTrans2.rect.height);

        return r1.Overlaps(r2);
    }


}
