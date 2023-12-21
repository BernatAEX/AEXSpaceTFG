using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SetOptionFromUI : MonoBehaviour
{
    public Scrollbar volumeSlider;
    public SetTurnTypeFromPlayerPref turnTypeFromPlayerPref;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

}
