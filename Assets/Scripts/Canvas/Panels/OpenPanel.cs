using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenPanel : MonoBehaviour, IPointerClickHandler
{
    #region "Input Fields"

    [SerializeField]
    [Header("Panel that will be opened")]
    [Space(10f)]
    private GameObject _panel;

    [SerializeField]
    [Header("BackGround Panel")]
    [Space(30f)]
    private GameObject _bgPanel;

    [SerializeField]
    [Header("The value for the popup scale [normal --> 1 + value --> normal]")]
    [Space(30f)]
    private float _popUpScaleValue;

    [SerializeField]
    [Range(0,1)]
    [Header("BackGround trasparency [alpha value]")]
    [Space(30f)]
    private float _bgTransparency;

    #endregion

    #region "Private members used in script"
    private Image _bgImage;

    #endregion


    private void Awake()
    {
        _bgImage = _bgPanel.GetComponent<Image>();

        ClosePanelUsingBackround.OnExit += SetFalse;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        SetActive();
    }

    // Set Panel and Bg to active
    public void SetActive()
    {
        _panel.SetActive(true);
        _bgPanel.SetActive(true);

        StartCoroutine("ChangeBgTransparency");
        StartCoroutine("PopUpScale");
    }


    // Set Panel and Bg to inactive
    public void SetFalse()
    {
        _panel.SetActive(false);
        _panel.transform.localScale = new Vector3(1,1,0);

        _bgPanel.SetActive(false);
        _bgImage.color = new Color(0,0,0,0);
    }


    // Change transparency
    private IEnumerator ChangeBgTransparency()
    {
        float temp = 0;
        while (temp < _bgTransparency)
        {
            temp += Time.deltaTime * 1.2f;
            _bgImage.color = new Color(0, 0, 0, temp);
            yield return null;
        } 
    }


    // Pop Up Scale animation
    private IEnumerator PopUpScale()
    {
        float temp = 0;
        while (temp < _popUpScaleValue)
        {
            temp += Time.deltaTime;
            _panel.transform.localScale = new Vector3(1 + temp, 1 + temp, 0);

            yield return null;
        }

        temp = 0;
        while (temp < _popUpScaleValue)
        {
            temp += Time.deltaTime;
            _panel.transform.localScale = new Vector3(1 + _popUpScaleValue - temp, 1 + _popUpScaleValue - temp, 0);

            yield return null;
        }
    }

    private void OnDestroy()
    {
        ClosePanelUsingBackround.OnExit -= SetFalse;
    }
}