using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairUI : SceneUI
{
    //TODO: Should react to the weapon switch. 
    RectTransform primary;
    RectTransform secondary;
    RectTransform current;
    [SerializeField] float scaleSize = 1.3f;
    [SerializeField] float totalscaletime = 0.1f;
    protected override void Awake()
    {

        base.Awake();
        Initialize();
    }
    private void OnEnable()
    {
        GameManager.CombatManager.WeaponSwitch += SwitchActiveWeapon;
    }
    private void OnDisable()
    {
        GameManager.CombatManager.WeaponSwitch -= SwitchActiveWeapon;
    }

    public void Initialize()
    {
        primary = transforms["CrossHair_Shotgun"];
        primary.gameObject.SetActive(false);
        secondary = transforms["CrossHair_Crossbow"];
        secondary.gameObject.SetActive(false);
        current = primary; 
    }


    public void SwitchActiveWeapon()
    {
        if (current.name == "Shotgun")
        {
            current.gameObject.SetActive(false);
            current = secondary;
            current.gameObject.SetActive(true); 
        }
        else
        {
            current.gameObject.SetActive(false);
            current = primary;
            current.gameObject.SetActive(true);
        }
        //upScale = StartCoroutine(RescaleButton(currentCH));
        //downScale = StartCoroutine(DownscaleButton(otherCH));
    }
    //Coroutine upScale;
    //Coroutine downScale;

    //float timestep_up;
    //float timestep_down;

    //Vector3 currentScale;
    //Vector3 upTargetScale;

    //Vector3 otherScale;
    //Vector3 downTargetScale;
    //Vector3 defaultScale = Vector3.one;
    //IEnumerator RescaleButton(RectTransform cH)
    //{
    //    timestep_up = 0f;
    //    currentScale = cH.localScale;
    //    upTargetScale = cH.localScale * scaleSize;
    //    while (timestep_up < totalscaletime)
    //    {
    //        timestep_up += Time.deltaTime;
    //        cH.localScale = Vector3.Lerp(currentScale, upTargetScale, timestep_up / totalscaletime);
    //        yield return null;
    //    }
    //}

    //IEnumerator DownscaleButton(RectTransform cH)
    //{
    //    timestep_down = 0f;
    //    otherScale = cH.localScale;
    //    downTargetScale = defaultScale;
    //    while (timestep_down < totalscaletime)
    //    {
    //        timestep_down += Time.deltaTime;
    //        cH.localScale = Vector3.Lerp(otherScale, Vector3.one, timestep_down / totalscaletime);
    //        yield return null;
    //    }
    //}
}
