using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class AruncaLaButon : MonoBehaviour
{
    public float putereAruncare = 6f;      
    public float pauzaDupaAruncare = 0.5f; 

    XRGrabInteractable obiectXR;
    Rigidbody rb;
    bool arunca;

    void Start()
    {
        obiectXR = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        obiectXR.activated.AddListener(OnActivated);
    }

    void OnActivated(ActivateEventArgs args)
    {
        if (arunca) return;
        var interactor = args.interactorObject as IXRSelectInteractor;
        if (interactor == null) return;
        Vector3 directie = args.interactorObject.transform.forward;
        StartCoroutine(Arunca(interactor, directie));
    }

    IEnumerator Arunca(IXRSelectInteractor interactor, Vector3 directie)
    {
        arunca = true;
        obiectXR.interactionManager.SelectExit(interactor, obiectXR);
        yield return null;
        yield return new WaitForFixedUpdate();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.linearVelocity = directie * putereAruncare;
        obiectXR.enabled = false;
        yield return new WaitForSeconds(pauzaDupaAruncare);
        obiectXR.enabled = true;
        arunca = false;
    }
}