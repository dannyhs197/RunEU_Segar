using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class TeleportController : MonoBehaviour
{
    [Header("Teleport Anchors")]
    [Tooltip("List of Teleportation Anchors to cycle through")]
    public TeleportationAnchor[] teleportationAnchors;

    [Tooltip("Teleportation Provider")]
    public TeleportationProvider teleportationProvider;

    [Tooltip("Input Action Reference for triggering teleport")]
    public InputActionReference teleportAction;

    private int currentAnchorIndex = 0;

    private void Start()
    {
        //Teleport to the first anchor on Start
        if (teleportationAnchors.Length > 0 && teleportationProvider != null)
        {
            TeleportationAnchor anchor = teleportationAnchors[0];
            if (anchor != null && anchor.teleportAnchorTransform != null)
            {
                TeleportRequest initialRequest = new TeleportRequest
                {
                    destinationPosition = anchor.teleportAnchorTransform.position,
                    destinationRotation = anchor.teleportAnchorTransform.rotation,
                    matchOrientation = MatchOrientation.TargetUpAndForward
                };

                teleportationProvider.QueueTeleportRequest(initialRequest);
            }
        }
    }

    private void OnEnable()
    {        
        if (teleportAction != null)
        {
            teleportAction.action.performed += OnTeleport;
        }
    }

    private void OnDisable()
    {
        if (teleportAction != null)
        {
            teleportAction.action.performed -= OnTeleport;
        }
    }

    private void OnTeleport(InputAction.CallbackContext context)
    {
        currentAnchorIndex++;
        try
        {
            TryTeleportToAnchor(currentAnchorIndex);
        }
        catch
        {
            Debug.Log("Reached end of teleport anchors. Looping back to start.");
            currentAnchorIndex = 0;
            TryTeleportToAnchor(currentAnchorIndex);
        }
    }

    private void TryTeleportToAnchor(int index)
    {
        if (teleportationAnchors == null || teleportationAnchors.Length == 0)
        {
            throw new System.Exception("Teleport anchor list is empty.");
        }

        if (index < 0 || index >= teleportationAnchors.Length)
        {
            throw new System.IndexOutOfRangeException("Teleport anchor index out of range.");
        }

        var anchor = teleportationAnchors[index];
        if (anchor == null || anchor.teleportAnchorTransform == null)
        {
            throw new System.Exception($"Teleport anchor at index '{index}' is null or missing its transform.");
        }

        var request = new TeleportRequest
        {
            destinationPosition = anchor.teleportAnchorTransform.position,
            destinationRotation = anchor.teleportAnchorTransform.rotation,
            matchOrientation = MatchOrientation.TargetUpAndForward
        };

        teleportationProvider.QueueTeleportRequest(request);
        Debug.Log($"Teleported to anchor '{index}'");
    }
}
