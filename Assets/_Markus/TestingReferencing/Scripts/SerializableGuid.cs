using System;
using UnityEngine;

[Serializable]
public class SerializableGuid
{
    [SerializeField]
    private byte[] serializedGuid = Guid.NewGuid().ToByteArray();

    public Guid Guid
    {
        get => new Guid(serializedGuid);
        set => serializedGuid = value.ToByteArray();
    }

    public override string ToString() => Guid.ToString();
}

