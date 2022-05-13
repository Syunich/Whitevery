using UnityEngine;
using UnityEngine.UI;

public class SESlider : MonoBehaviour
{
    private float passedtime;
    private void Start()
    {
        Slider SEslider = GetComponent<Slider>();
        SEslider.value = AudioManager.Instance.SEvolume;
        SEslider.onValueChanged.AddListener(value => AudioManager.Instance.SEvolume = value);
        SEslider.onValueChanged.AddListener( x => CheckSEvolume());
    }

    private void Update()
    {
        passedtime += Time.deltaTime;
    }

    private void CheckSEvolume()
    {
        if (passedtime < 0.5f)
        {
            return;
        }

        passedtime = 0;
        AudioManager.Instance.PlaySE(0);
    }
}