using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCanvasOnInteract : MonoBehaviour
{
    public GameObject canvas; // Assign your canvas in the Inspector

    private void Start()
    {
        if (canvas != null)
            canvas.SetActive(false); // Hide canvas initially
    }

    public void ShowCanvas()
    {
        if (canvas != null)
            canvas.SetActive(true);
    }

    public void HideCanvas()
    {
        if (canvas != null)
            canvas.SetActive(false);
    }
}
