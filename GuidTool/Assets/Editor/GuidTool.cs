using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GuidTool : ScriptableWizard
{
    public RectTransform target;
    public Camera uiCamera;
    public Shader shader;

    private Material material;
    private void OnWizardUpdate()
    {
        helpString = "图片为可选项，用于展示引导";
        isValid = (target != null) && (uiCamera != null) &&(shader!=null);
    }
    void OnWizardCreate()
    {

        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Vector3[] corners = new Vector3[4];

        target.GetWorldCorners(corners);

        for (int i = 0; i < corners.Length; i++) {
            corners[i] = uiCamera.WorldToScreenPoint(corners[i]);
        }

        float x = corners[0].x + (corners[3].x - corners[0].x) / 2f;
        float y = corners[0].y + (corners[1].y - corners[0].y) / 2f;

        Vector3 center = new Vector3(x, y, corners[0].z+10);

        target.GetWorldCorners(corners);
        float diameter = Vector3.Distance(corners[0],corners[2]);

        GameObject mask = new GameObject("mask");  
        RawImage rawImage = mask.AddComponent <RawImage>();
        Material material = new Material(shader);
        material.SetFloat("_Slider", diameter/2f);
        center = canvas.worldCamera.ScreenToWorldPoint(center);
        material.SetVector("_Center", center);
        material.SetColor("_Color", new Color(0, 0, 0, 0.5f));
        rawImage.material = material;
        mask.transform.SetParent(canvas.transform);
        mask.transform.position=center;
        Mask help = mask.AddComponent<Mask>();
        help.radius = diameter / 2f;

        GameObject click = new GameObject("click");
        RawImage image = click.AddComponent<RawImage>();
        image.color = new Color(0, 0, 0, 0);
        click.transform.position = center;
        (click.transform as RectTransform).sizeDelta = target.sizeDelta;
        Debug.Log(click.transform.localScale);
        click.transform.SetParent(canvas.transform);
        click.transform.localScale = target.transform.localScale;
        click.AddComponent<ButtonMask>();

        foreach (Transform tran in canvas.GetComponentsInChildren<Transform>())
            tran.gameObject.layer = LayerMask.NameToLayer("UI");
        
    }

    [MenuItem("Tool/GuidTool")]
    public static void Guid()
    {
        ScriptableWizard.DisplayWizard<GuidTool>("Guid Create Tools", "Create");
    }

    private Vector2 WorldToScreen(Canvas canvas, Vector3 pos) {

        Vector2 screenPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pos, uiCamera, out screenPos);
        return screenPos;
            
    }
}
