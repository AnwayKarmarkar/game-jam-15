using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlareGun : MonoBehaviour {

    public GameObject FlarePrefab;

    public int FlareDuration = 1; 

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void CreateFlare(GameManager.Compound compound) {
        var flare = Instantiate(FlarePrefab, this.transform.position, this.transform.rotation);
        Destroy(flare, FlareDuration);
        var light = flare.GetComponent<Light2D>();
        light.enabled = true;
        light.color = compound.Color;
    }
}
