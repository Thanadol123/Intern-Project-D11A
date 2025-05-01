using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CarDoorTrigger : MonoBehaviour
{
    public GameObject xrRig;                // Reference to the XR Rig (player)
    public Transform seatPosition;          // Seat position
    public Transform exitPosition;          // Exit position

    private bool isInside = false;

    // Movement-related components
    private ActionBasedContinuousMoveProvider moveProvider;
    private ActionBasedSnapTurnProvider turnProvider;
    private TeleportationProvider teleportProvider;

    void Start()
    {
        moveProvider = xrRig.GetComponent<ActionBasedContinuousMoveProvider>();
        turnProvider = xrRig.GetComponent<ActionBasedSnapTurnProvider>();
        teleportProvider = xrRig.GetComponent<TeleportationProvider>();
    }

    public void TriggerTeleport()
    {
        if (exitPosition == null || seatPosition == null || xrRig == null)
        {
            Debug.LogError("Missing references!");
            return;
        }

        if (isInside)
        {
            xrRig.transform.position = exitPosition.position;
            SetMovementEnabled(true);
            isInside = false;
        }
        else
        {
            xrRig.transform.position = seatPosition.position;
            SetMovementEnabled(false);
            isInside = true;
        }
    }

    void SetMovementEnabled(bool enable)
    {
        if (moveProvider != null)
            moveProvider.enabled = enable;

        if (turnProvider != null)
            turnProvider.enabled = enable;

        if (teleportProvider != null)
            teleportProvider.enabled = enable;
    }
}
