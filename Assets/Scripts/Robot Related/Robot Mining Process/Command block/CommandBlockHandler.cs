using System;
using UnityEngine;

// Hanlde input from the command block
public class CommandBlockHandler : MonoBehaviour
{
    public event Action OnPreviewCommand;
    public event Action OnExecuteCommand;
    public event Action OnDeleteCommand;

    // Restrict the PanPinch while user gives a command
    public static event Action OnGivingCommand;

    // Purpose of bool --> OnMouseUpAsButton() also calls OnMouseUp() because finger is lifted in both scenarios
    private bool _once = true;
    private float _time = 0;

    // Detect if finger has been drag around the screen
    private void OnMouseDrag()
    {
        OnGivingCommand?.Invoke();

        _time += Time.deltaTime;
        _once = true;

        OnPreviewCommand?.Invoke();
    }

    // Detect if finger has been lifted up after a movement[drag] on the screen 
    private void OnMouseUp()
    {
        _time = 0;
        if (_once == true)
        {
            OnExecuteCommand?.Invoke();
        }
    }

    // Detect if gameObject has been tapped and invoke only the OnDeleteCommand event
    private void OnMouseUpAsButton()
    {
        if (_time < 0.3f)
        {
            _once = false;
            OnDeleteCommand?.Invoke();
        }
    }
}