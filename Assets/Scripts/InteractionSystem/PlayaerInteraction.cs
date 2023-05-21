using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayaerInteraction : MonoBehaviour
{
    public Camera mainCam;
    public float interactionDistance = 2f;
    public GameObject interactionUI;
    public Text InteractionText;

    private void Update()
    {
        interactionRay();
    }

    void interactionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hit;

        bool hitSomething = false;

        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                hitSomething = true;
                InteractionText.text = interactable.GetDescription();

                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.interact();
                }
            }
        }

        interactionUI.SetActive(hitSomething);
    }
}