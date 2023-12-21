using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ShowKeyboard : MonoBehaviour
{

    private TMP_InputField inputField;

    public float distance = 1.0f;
    public float verticalOffset = -0.1f;

    public Transform positionSource;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        Vector3 direction = positionSource.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPosition = positionSource.position + direction * distance + Vector3.up * verticalOffset;

        NonNativeKeyboard.Instance.RepositionKeyboard(targetPosition);
        Debug.Log("Keyboard ON");

        SetCaretColorAlpha(1);

        NonNativeKeyboard.Instance.OnClosed += Instance_onClosed;

    }

    private void Instance_onClosed(object sender, System.EventArgs e)
    {
        SetCaretColorAlpha(0);
        NonNativeKeyboard.Instance.OnClosed -= Instance_onClosed;
        Debug.Log("Keyboard ON");
    }

    public void SetCaretColorAlpha(float value)
    {
        inputField.customCaretColor = true;
        Color caretColor = inputField.caretColor;
        caretColor.a = value;
        inputField.caretColor = caretColor;
    }
}

