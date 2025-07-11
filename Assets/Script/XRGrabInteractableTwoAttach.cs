using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactableObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if(args.interactableObject.transform.CompareTag("Reft Hand"))
        {
            attachTransform = rightAttachTransform;
        }

        base.OnSelectEntered(args);
    }

}
