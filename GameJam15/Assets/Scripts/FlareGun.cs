using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlareGun : MonoBehaviour {

    public GameObject FlarePrefab;

    public int FlareDuration = 1;

  

    void Update() {

    }

    public void CreateFlare(GameManager.Compound compound) {
        string tagName = "";
        switch (compound.Name) {
            case "NaCl":
                tagName = "YellowFlare";
                break;
            case "Sr(NO3)2":
                tagName = "RedFlare";
                break;
        }
        var flare = Instantiate(FlarePrefab, this.transform.position, this.transform.rotation, transform);
        tagName = compound.Name switch {
            "NaCl" => "YellowFlare",
            "Sr(NO3)2" => "RedFlare",
            "CuSO4" => "GreenFlare",
            "CuCl2" => "BlueFlare",
            "KNO3KSO4" => "VioletFlare",
            _ => ""
        };
        flare = Instantiate(FlarePrefab, this.transform.position, this.transform.rotation);
        flare.tag = tagName;
        Destroy(flare, FlareDuration);
        var light = flare.GetComponent<Light2D>();
        light.enabled = true;
        light.color = compound.Color;
    }

}
