using UnityEngine;   // use Unity basic tool and stuff 
using UnityEngine.UI;   // use UI tools like Slider and CanvasSca

public class UISettings : MonoBehaviour    // this script controls the UI size scaleusing a sl
{
    public CanvasScaler scaler;   // this is the CanvasScaler on the Canvas controls UI siz
    public Slider uiScaleSlider;    // this is the slider that the player moves to change UI 

    void Start()     // Start runs once when the scene begn
    {
        float s = PlayerPrefs.GetFloat("ui_scale", 1f);   // read saved UI scale from PlayerPrefs default is 1 if nothing sav
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;    // make sure the scaler uses ConstantPixelSize mo
        scaler.scaleFactor = s;      // set the current scale of the UI to the saved 
        uiScaleSlider.minValue = 0.8f;     // set the minimum value of the slider smallest UI si
        uiScaleSlider.maxValue = 1.4f;    // set the maximum value of the slider biggest UI 
        uiScaleSlider.value = s;     // set the slider position to the saved valu
        uiScaleSlider.onValueChanged.AddListener(SetScale);     // when the slider value changes, call SetSca
    }

    public void SetScale(float s)     // this function runs when we move the sli
    {
        scaler.scaleFactor = s;     // change the UI scale to the new value from the sl
        PlayerPrefs.SetFloat("ui_scale", s);    // save the new value so it stays next time we open the ga
    }
}
