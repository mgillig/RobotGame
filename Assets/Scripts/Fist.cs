using UnityEngine;

public class Fist
{
    public bool grab;
    private bool isRight;
    public Transform fistLocation;

    public Fist(bool isRightIn, Transform fistLocationIn)
    {
        isRight = isRightIn;
        grab = false;
        fistLocation = fistLocationIn;
    }

    public string fistAction()
    {
        if(grab)
        {
            grab = false;
            if(isRight)
                return "TrRightThrow";
            else
                return "TrLeftThrow";
        }
        else
        {
            if(isRight)
                return "TrRightPunch";
            else
                return "TrLeftPunch";
        }
    }
}
