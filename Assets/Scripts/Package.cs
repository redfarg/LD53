using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Package : MonoBehaviour
{
    private ModeColor color;
    private ModeShape shape;
    private ModeSymbol symbol;

    public void Init(ModeColor color, ModeShape shape, ModeSymbol symbol)
    {
        this.color = color;
        this.shape = shape;
        this.symbol = symbol;
    }

    public ModeColor getColor()
    {
        return this.color;
    }

    public ModeSymbol getSymbol()
    {
        return this.symbol;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
