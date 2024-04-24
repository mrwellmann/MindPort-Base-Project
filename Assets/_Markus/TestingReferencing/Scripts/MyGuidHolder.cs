using System;
using UnityEngine;

public class MyGuidHolder : MonoBehaviour
{
    [SerializeField]
    private SerializableGuid serializedGuidWithDrawer = new SerializableGuid();
    [SerializeField]
    private byte[] serializedGuid = Guid.NewGuid().ToByteArray();
}
