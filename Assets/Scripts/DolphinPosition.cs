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
    public Button buttonScaleUp;
    public Button buttonScaleDown;
    public Button buttonReset;
    public float moveSpeed = 0.5f;

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
        buttonScaleUp.onClick.AddListener(OnButtonScaleUpClick);
        buttonScaleDown.onClick.AddListener(OnButtonScaleDownClick);
        buttonReset.onClick.AddListener(OnButtonResetClick);
    }

    void OnButtonUpClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localPosition += new Vector3(0, moveSpeed, 0);
    }

    void OnButtonDownClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localPosition += new Vector3(0, -moveSpeed, 0);
    }

    void OnButtonLeftClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localPosition += new Vector3(-moveSpeed, 0, 0);
    }

    void OnButtonRightClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localPosition += new Vector3(moveSpeed, 0, 0);
    }

    void OnButtonFrontClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localPosition += new Vector3(0, 0, moveSpeed);
    }

    void OnButtonBackClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localPosition += new Vector3(0, 0, -moveSpeed);
    }

    void OnButtonRotateUpClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localRotation *= Quaternion.Euler(moveSpeed, 0, 0);
    }

    void OnButtonRotateDownClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localRotation *= Quaternion.Euler(-moveSpeed, 0, 0);
    }

    void OnButtonScaleUpClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    void OnButtonScaleDownClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

    void OnButtonResetClick()
    {
        if (!dolphinModel.gameObject.activeSelf) return;
        dolphinModel.localPosition = new Vector3(0, -2, 10);
        dolphinModel.localRotation = Quaternion.Euler(0, 0, 0);
        dolphinModel.localScale = new Vector3(1, 1, 1);
    }
}

