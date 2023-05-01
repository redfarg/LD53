using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dropoff : MonoBehaviour
{
    [SerializeField] GameObject sign;

    [SerializeField] ModeMappings modeMappings;

    private Distraction distraction = null;

    private Mode mode;

    private ModeColor color;
    private ModeShape shape;
    private ModeSymbol symbol;

    public UnityEvent PackageDeliveredCorrectEvent;
    public UnityEvent PackageDeliveredIncorrectEvent;

    void Start()
    {
        
    }

    public void UpdateMode(Mode newMode, ModeColor newColor, ModeShape newShape, ModeSymbol newSymbol)
    {
        this.mode = newMode;
        this.color = newColor;
        this.shape = newShape;
        this.symbol = newSymbol;

        sign.transform.Find("SignColor").GetComponent<SpriteRenderer>().color = modeMappings.colorToColor[newColor];
        sign.transform.Find("SignSymbol").GetComponent<SpriteRenderer>().sprite = modeMappings.symbolToSprite[newSymbol];
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (distraction == null && collider.gameObject.tag == "Package")
        {
            if (this.mode == Mode.Color)
            {
                if (this.color == collider.gameObject.GetComponent<Package>().getColor())
                {
                    Debug.Log("Right color");
                    PackageDeliveredCorrectEvent.Invoke();
                }
                else
                {
                    Debug.Log("Wrong color");
                    PackageDeliveredIncorrectEvent.Invoke();
                }
            }
            else if (this.mode == Mode.Symbol)
            {
                if (this.symbol == collider.gameObject.GetComponent<Package>().getSymbol())
                {
                    Debug.Log("Right symbol");
                    PackageDeliveredCorrectEvent.Invoke();
                }
                else
                {
                    Debug.Log("Wrong symbol");
                    PackageDeliveredIncorrectEvent.Invoke();
                }
            }
            Destroy(collider.gameObject);
        }
    }

    public void Distract(Distraction distraction)
    {
        this.distraction = distraction;
    }

}
