using EasyButtons;
using UnityEngine;

public class EasyInstance : MonoBehaviour
{
    public GameObject ObjectToInstance; // The GameObject you want to instance if null will be this gameobject
    public GameObject NewParent;        // The new parent of the instance if null will be the same as the original object
    public Vector3 NewRelativePosition; // The position relative to the original object where the new instance should be created

    [Button]
    public void CreateInstance()
    {
        if (ObjectToInstance == null)
            ObjectToInstance = gameObject;

        if (NewParent == null)
            NewParent = transform.parent.gameObject;

        // Calculate the new position based on the original object's position and the relative position
        Vector3 newPosition = transform.position + NewRelativePosition;

        // Instantiate the object at the new position
        GameObject newInstance = Instantiate(ObjectToInstance, newPosition, Quaternion.identity, NewParent.transform);

    }
}
