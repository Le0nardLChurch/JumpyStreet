using UnityEngine;

[System.Serializable]
public class InputData
{
#pragma warning disable 0649
    [SerializeField] KeyCode forward;
    [SerializeField] KeyCode back;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
#pragma warning restore 0649 
    KeyCode[] inputKeys;

    public KeyCode Forward { get { return forward; } }
    public KeyCode Back { get { return back; } }
    public KeyCode Left { get { return left; } }
    public KeyCode Right { get { return right; } }


    KeyCode[] InputKeys()
    {
        if (inputKeys == null)
        {
            inputKeys = new KeyCode[] { forward, back, left, right }; 
        }
        return inputKeys;
    }

    public bool IsInputKeyDown()
    {
        foreach (var key in InputKeys())
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }
}





