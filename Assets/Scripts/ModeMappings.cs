using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModeMappings : MonoBehaviour
{

    [SerializeField] Sprite spriteBrown;
    [SerializeField] Sprite spriteRed;
    [SerializeField] Sprite spriteGreen;
    [SerializeField] Sprite spriteBlue;

    [SerializeField] Sprite spriteSymSquare;
    [SerializeField] Sprite spriteSymCircle;
    [SerializeField] Sprite spriteSymL;
    [SerializeField] Sprite spriteSymI;

    public Dictionary<ModeColor, Sprite> colorToSprite;
    public Dictionary<ModeColor, Color> colorToColor;
    public Dictionary<ModeSymbol, Sprite> symbolToSprite;

    void Awake()
    {
        colorToSprite = new Dictionary<ModeColor, Sprite>()
            {
                { ModeColor.Brown, spriteBrown },
                { ModeColor.Red, spriteRed },
                { ModeColor.Green, spriteGreen },
                { ModeColor.Blue, spriteBlue }
            };

        colorToColor = new Dictionary<ModeColor, Color>()
            {
                { ModeColor.Brown, new Color(102.0f/255f, 57.0f/255f, 49.0f/255f, 1.0f) },
                { ModeColor.Red, new Color(172.0f/255f, 50.0f/255f, 50.0f/255f, 1.0f) },
                { ModeColor.Green,  new Color(75.0f/255f, 105.0f/255f, 47.0f/255f, 1.0f) },
                { ModeColor.Blue, new Color(48.0f/255f, 96.0f/255f, 130.0f/255f, 1.0f) }
            };

        symbolToSprite = new Dictionary<ModeSymbol, Sprite>()
            {
                { ModeSymbol.Square, spriteSymSquare },
                { ModeSymbol.Circle, spriteSymCircle },
                { ModeSymbol.ISymbol, spriteSymI },
                { ModeSymbol.LSymbol, spriteSymL }
            };
    }
}
