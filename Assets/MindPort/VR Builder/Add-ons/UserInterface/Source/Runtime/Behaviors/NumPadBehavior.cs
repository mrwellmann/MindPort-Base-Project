using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine.Scripting;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using VRBuilder.UI.Properties;
using VRBuilder.UI.Utils;

namespace VRBuilder.UI.Behaviors
{
    /// <summary>
    /// Setup Num Pad Visibility
    /// </summary>
    [DataContract(IsReference = true)]
    public class NumPadBehavior : Behavior<NumPadBehavior.EntityData>
    {
        [DisplayName("Num Pad Visibility")]
        [DataContract(IsReference = true)]
        public class EntityData : IBehaviorData
        {

            [DataMember(IsRequired = false)]
            [DisplayName("Num Pad")]
            public SingleScenePropertyReference<INumPadProperty> Target { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<INumPadProperty> Numpad { get; set; }

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"Show Num Pad: {Target}";

            [DataMember(IsRequired = false)]
            [DisplayName("Visibility")]
            public bool Visible { get; set; }

            public Metadata Metadata { get; set; }
        }


        private class ActivatingProcess : InstantProcess<EntityData>
        {
            public ActivatingProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                var numPad = Data.Target.Value;
                if (numPad != null)
                {
                    numPad.SetNumPadVisibility(Data.Visible);
                }
                else
                {
                    var sceneBox = FindValidNumPad();
                    sceneBox?.SetNumPadVisibility(Data.Visible);
                }
            }
        }

        [JsonConstructor, Preserve]
        public NumPadBehavior() : this(Guid.Empty)
        {
        }

        public NumPadBehavior(INumPadProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public NumPadBehavior(Guid guid)
        {
            Data.Target = new SingleScenePropertyReference<INumPadProperty>(guid);
        }

        public override IStageProcess GetActivatingProcess()
        {
            return new ActivatingProcess(Data);
        }

        protected static INumPadProperty FindValidNumPad()
        {
            return InterfaceExtensions.FindInterfaceOfType<INumPadProperty>().FirstOrDefault(numPad => numPad.IsValid);
        }
    }
}