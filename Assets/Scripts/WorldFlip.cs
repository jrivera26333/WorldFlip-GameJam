using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldFlip : MonoBehaviour
{
    [SerializeField] Transform gridTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            RotateGrid();
    }

    public void RotateGrid()
    {
        float zGridRotation = gridTransform.localEulerAngles.z;
        zGridRotation += 180;
        gridTransform.DORotate(new Vector3(gridTransform.localEulerAngles.x, gridTransform.localEulerAngles.y, zGridRotation), 1.5f).SetEase(Ease.OutSine);
    }
}
