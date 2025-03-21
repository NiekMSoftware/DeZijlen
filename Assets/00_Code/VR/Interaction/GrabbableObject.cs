using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

namespace DeZijlen.VR.Interaction
{
    [RequireComponent(typeof(XRGeneralGrabTransformer))]
    public abstract class GrabbableObject : XRGrabInteractable { }
}
