using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject toggleObject = null;
    private bool isPressed = false;

    public void ToggleObject()
    {
        if (!isPressed)
        {
            toggleObject.gameObject.active = true;
            isPressed = true;
        }
        else
        {
            toggleObject.gameObject.active = false;
            isPressed = false;
        }
    }
}