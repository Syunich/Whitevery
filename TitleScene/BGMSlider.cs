using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    private void Start()
    {
        Slider BGMslider = GetComponent<Slider>();
        BGMslider.value = AudioManager.Instance.BGMvolume;
        BGMslider.value = AudioManager.Instance.BGMvolume;
        BGMslider.onValueChanged.AddListener(value => AudioManager.Instance.BGMvolume = value);
    }
}
