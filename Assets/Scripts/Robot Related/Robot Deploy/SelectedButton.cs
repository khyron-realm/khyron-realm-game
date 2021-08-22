using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SelectRobot);
    }

    // Scale up button
    private void SelectRobot()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
        ManageButtonsTouched.DisableOtherButtone(gameObject);
    }

    // Scale down button
    public void DeselectRobot()
    {
        transform.localScale = new Vector3(1f, 1f, 1);
    }
}