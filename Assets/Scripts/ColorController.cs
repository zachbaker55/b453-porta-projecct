using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorController : MonoBehaviour
{
    public Volume postProcessingVolume;
    public ColorProfileList colorProfileList;
    //being serialized just for debug viewing purpose. do not override in editor.
    [SerializeField] private ColorAdjustments colorAdjustments;
    [SerializeField] private int currentProfileIndex = 0;

    private void OnEnable() 
    {
        DialogueEvent.DialogueEnd += OnDialogueEnd;
    }

    private void OnDisable() 
    {
        DialogueEvent.DialogueEnd -= OnDialogueEnd;
    }

    private void OnDialogueEnd(object sender, DialogueArgs args) 
    {
        if(args.DialogueContext == "StartingDialogue") StartWorldChanges();
    }

    public void StartWorldChanges()
    {
        Debug.Log("starting world changes");
        if (postProcessingVolume.profile.TryGet(out colorAdjustments))
        {
            StartCoroutine(ChangeColorProfileLoop());
        }
    }

    private IEnumerator ChangeColorProfileLoop()
    {
        while (true)
        {
            ColorProfile targetProfile = colorProfileList.colorProfiles[currentProfileIndex];

            yield return StartCoroutine(ChangeColorProfile(targetProfile));
            
            //if the level doesn't end or something modulo divide so the colorchanges just repeat, instead of out of bounds crash
            currentProfileIndex = (currentProfileIndex + 1) % colorProfileList.colorProfiles.Count;
        }
    }

    private IEnumerator ChangeColorProfile(ColorProfile targetProfile)
    {
        float elapsedTime = 0;
        float initialSaturation = colorAdjustments.saturation.value;
        float initialBrightness = colorAdjustments.postExposure.value;
        float initialContrast = colorAdjustments.contrast.value;

        while (elapsedTime < targetProfile.transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / targetProfile.transitionTime;
            if (targetProfile.saturation != -101)
                colorAdjustments.saturation.value = Mathf.Lerp(initialSaturation, targetProfile.saturation, t);

            if (targetProfile.brightness != -101)
                colorAdjustments.postExposure.value = Mathf.Lerp(initialBrightness, targetProfile.brightness, t);

            if (targetProfile.contrast  != -101)
                colorAdjustments.contrast.value = Mathf.Lerp(initialContrast, targetProfile.contrast, t);
                yield return null;
        }
        if(targetProfile.errorMode == true) //if enabled the colors revert back to base value of 0 after transitiontime
        {
            
            elapsedTime = 0;
            while (elapsedTime < targetProfile.transitionTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / targetProfile.transitionTime;

                if (targetProfile.saturation != -101)
                    colorAdjustments.saturation.value = Mathf.Lerp(initialSaturation, 0, t);

                if (targetProfile.brightness != -101)
                    colorAdjustments.postExposure.value = Mathf.Lerp(initialBrightness, 0, t);

                if (targetProfile.contrast  != -101)
                    colorAdjustments.contrast.value = Mathf.Lerp(initialContrast, 0, t);
                yield return null;
            }
        }
        //Debug.Log("starting next color profile");
        
    }
}