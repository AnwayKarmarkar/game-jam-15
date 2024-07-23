using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public class Compound {
        public Color32 Color { get; set; }
        public string Name { get; set; }
    }

    public static Dictionary<string, Compound> ViableCompounds = new() {
        {"1123", new Compound() {
            Name = "NaCl",
            Color = Color.yellow
        }},
        {"1221", new Compound() {
            Name = "Sr(NO3)2",
            Color = Color.red
        }},
        {"1422", new Compound() {
            Name = "CuSO4",
            Color = Color.green
        }},
        {"1423", new Compound() {
            Name = "CuCl2",
            Color = Color.blue
        }},
        {"13211322", new Compound() {
            Name = "KNO3KSO4",
            Color = new Color(0.5f,0f,1f)
        }},
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
