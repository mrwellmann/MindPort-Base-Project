using System;
using UnityEngine;

[ExecuteAlways]
public class PrefabExecuteAlways : MonoBehaviour
{
    [SerializeField]
    private string guid = string.Empty;

    public Guid Guid
    {
        get
        {
            Guid realGuid;
            if (!Guid.TryParse(guid, out realGuid))
            {
                realGuid = Guid.NewGuid();
                guid = realGuid.ToString();
            }
            return realGuid;
        }
        set
        {
            guid = value.ToString();
        }
    }

    protected void Awake()
    {
        var oldGuid = Guid;
        Guid = Guid.NewGuid();
        Debug.Log($"ExecuteAlways Awake {this.name}, instanceID {gameObject.GetInstanceID()}, oldGuid {oldGuid}, newGuid {Guid}");
    }

    protected void OnValidate()
    {
        Debug.Log($"ExecuteAlways OnValidate {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }

    protected void Start()
    {
        Debug.Log($"ExecuteAlways Start {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }

    protected void OnEnable()
    {
        Debug.Log($"ExecuteAlways OnEnable {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }

    protected void OnDisable()
    {
        Debug.Log($"ExecuteAlways OnDisable {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }

    protected void OnDestroy()
    {
        Debug.Log($"ExecuteAlways OnDestroy {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }
}
