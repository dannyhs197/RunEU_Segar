using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
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

    [Header("Fade UI")]
    public Image fadeImage;

    private float fadeDuration = 1.0f;
    private int currentAnchorIndex = 0;
    private bool isTeleporting = false;

    private void Start()
    {
        TryTeleportToAnchor(0);
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
        if (isTeleporting) return;

        currentAnchorIndex++;
        try
        {
            if (currentAnchorIndex >= teleportationAnchors.Length)
            {
                throw new System.IndexOutOfRangeException();
            }

            StartCoroutine(SmoothTeleport(currentAnchorIndex));
        }
        catch
        {
            Debug.Log("Reached end of teleport anchors. Looping back to start.");
            currentAnchorIndex = 0;
            StartCoroutine(SmoothTeleport(currentAnchorIndex));
        }
    }

    private IEnumerator SmoothTeleport(int index)
    {
        isTeleporting = true;

        yield return StartCoroutine(Fade(1f));

        TryTeleportToAnchor(index);

        yield return new WaitForSeconds(0.05f);

        yield return StartCoroutine(Fade(0f));

        isTeleporting = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("Fade image not assigned!");
            yield break;
        }

        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        Color final = fadeImage.color;
        final.a = targetAlpha;
        fadeImage.color = final;
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
