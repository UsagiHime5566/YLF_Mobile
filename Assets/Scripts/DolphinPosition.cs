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

    public Transform dolphinModel;

    void Start()
    {
        buttonUp.onClick.AddListener(OnButtonUpClick);
        buttonDown.onClick.AddListener(OnButtonDownClick);
        buttonLeft.onClick.AddListener(OnButtonLeftClick);
        buttonRight.onClick.AddListener(OnButtonRightClick);
        buttonFront.onClick.AddListener(OnButtonFrontClick);
        buttonBack.onClick.AddListener(OnButtonBackClick);
    }

    void OnButtonUpClick()
    {
        dolphinModel.position += new Vector3(0, 1, 0);
    }

    void OnButtonDownClick()
    {
        dolphinModel.position += new Vector3(0, -1, 0);
    }

    void OnButtonLeftClick()
    {
        dolphinModel.position += new Vector3(-1, 0, 0);
    }

    void OnButtonRightClick()
    {
        dolphinModel.position += new Vector3(1, 0, 0);
    }

    void OnButtonFrontClick()
    {
        dolphinModel.position += new Vector3(0, 0, 1);
    }

    void OnButtonBackClick()
    {
        dolphinModel.position += new Vector3(0, 0, -1);
    }
}

