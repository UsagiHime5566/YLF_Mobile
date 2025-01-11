using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DolphinPosition : MonoBehaviour
{
    public Button buttonUp;
    public Button buttonDown;
    public Button buttonLeft;
    public Button buttonRight;
    public Button buttonFront;
    public Button buttonBack;
    public Button buttonRotateUp;
    public Button buttonRotateDown;
    public Button buttonReset;

    public Transform dolphinModel;

    void Start()
    {
        buttonUp.onClick.AddListener(OnButtonUpClick);
        buttonDown.onClick.AddListener(OnButtonDownClick);
        buttonLeft.onClick.AddListener(OnButtonLeftClick);
        buttonRight.onClick.AddListener(OnButtonRightClick);
        buttonFront.onClick.AddListener(OnButtonFrontClick);
        buttonBack.onClick.AddListener(OnButtonBackClick);
        buttonRotateUp.onClick.AddListener(OnButtonRotateUpClick);
        buttonRotateDown.onClick.AddListener(OnButtonRotateDownClick);
        buttonReset.onClick.AddListener(OnButtonResetClick);
    }

    void OnButtonUpClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.position += new Vector3(0, 1, 0);
    }

    void OnButtonDownClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.position += new Vector3(0, -1, 0);
    }

    void OnButtonLeftClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.position += new Vector3(-1, 0, 0);
    }

    void OnButtonRightClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.position += new Vector3(1, 0, 0);
    }

    void OnButtonFrontClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.position += new Vector3(0, 0, 1);
    }

    void OnButtonBackClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.position += new Vector3(0, 0, -1);
    }

    void OnButtonRotateUpClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.rotation *= Quaternion.Euler(1, 0, 0);
    }

    void OnButtonRotateDownClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.rotation *= Quaternion.Euler(-1, 0, 0);
    }

    void OnButtonResetClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.position = new Vector3(0, -2, 10);
        dolphinModel.rotation = Quaternion.Euler(0, 0, 0);
    }
}

