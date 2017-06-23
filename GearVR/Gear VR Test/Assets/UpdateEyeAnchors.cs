using UnityEngine;
using UnityEngine.VR;

public class UpdateEyeAnchors : MonoBehaviour
{
    GameObject[] eyes = new GameObject[2];
    string[] eyeAnchorNames = { "LeftEyeAnchor", "RightEyeAnchor" };

    void Update()
    {
        for (int i = 0; i < 2; ++i)
        {
            // If the eye anchor is no longer a child of us, don't use it
            if (eyes[i] != null && eyes[i].transform.parent != transform)
            {
                eyes[i] = null;
            }

            // If we don't have an eye anchor, try to find one or create one
            if (eyes[i] == null)
            {
                Transform t = transform.Find(eyeAnchorNames[i]);
                if (t)
                    eyes[i] = t.gameObject;

                if (eyes[i] == null)
                {
                    eyes[i] = new GameObject(eyeAnchorNames[i]);
                    eyes[i].transform.parent = gameObject.transform;
                }
            }

            // Update the eye transform
            eyes[i].transform.localPosition = InputTracking.GetLocalPosition((VRNode)i);
            eyes[i].transform.localRotation = InputTracking.GetLocalRotation((VRNode)i);
        }
    }
}