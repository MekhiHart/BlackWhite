using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    [SerializeField] Image backgroundLayer;
    public static ColorController objContoller;

    public string backgroundColor;

    // Start is called before the first frame update

    private void Start() {
        objContoller = this;
        backgroundColor = backgroundLayer.color == Color.black ? "black" : "white";
    } // *Start



    void ChangeState(){
        backgroundColor = backgroundColor == "black" ? "white" : "black"; // * Assignes the opposite color
        backgroundLayer.color =  backgroundLayer.color == Color.black ? Color.white : Color.black;
    }


    private void OnEnable() {
        PlayerController.shift_States += ChangeState;
    }

    private void OnDisable() {
        PlayerController.shift_States -= ChangeState;
    }

    public void ResetColor(){
        backgroundLayer.color = Color.black;
    }
}
