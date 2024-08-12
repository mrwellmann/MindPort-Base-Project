using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine.Scripting;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using VRBuilder.UI.Properties;
using VRBuilder.UI.Utils;

namespace VRBuilder.UI.Conditions
{
    /// <summary>
    /// A condition that completes when an Mandatory InfoBox is Acknowledged
    /// </summary>
    [DataContract(IsReference = true)]
    public class MandatoryInfoBoxCondition : Condition<MandatoryInfoBoxCondition.EntityData>
    {
        [DisplayName("Mandatory Infobox")]
        public class EntityData : IConditionData
        {
            [DataMember]
            [DisplayName("Infobox")]
            public SingleScenePropertyReference<IInfoBoxProperty> Target { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<IInfoBoxProperty> InfoBox { get; set; }

            public bool IsCompleted { get; set; }

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"Mandatory Infobox: {Target}";

            public Metadata Metadata { get; set; }
        }

        private class ActivatingProcess : InstantProcess<EntityData>
        {
            public ActivatingProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                var infoBox = Data.Target.Value;
                if (infoBox != null)
                {
                    infoBox.SetInfoBoxType(InfoBoxType.Mandatory);
                    infoBox.ShowInfoBox(true, 0f);
                }
                else
                {
                    var sceneBox = FindValidInfobox();
                    sceneBox?.SetInfoBoxType(InfoBoxType.Mandatory);
                    sceneBox?.ShowInfoBox(true, 0f);
                }
            }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            public ActiveProcess(EntityData data) : base(data)
            {
            }

            protected override bool CheckIfCompleted()
            {
                var infoBox = Data.Target.Value;
                if (infoBox != null)
                {
                    return infoBox.IsCompleted;
                }
                else
                {
                    var sceneBox = FindValidInfobox();
                    if (sceneBox!=null)
                    {
                        return sceneBox.IsCompleted;
                    }
                }
                return false;
            }
        }

        private class DeactivatingProcess : InstantProcess<EntityData>
        {
            public DeactivatingProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                var infoBox = Data.Target.Value;
                if (infoBox != null)
                {
                    infoBox.ShowInfoBox(false);
                }
                else
                {
                    var sceneBox = FindValidInfobox();
                    sceneBox?.ShowInfoBox(false);
                }
            }
        }

        [JsonConstructor, Preserve]
        public MandatoryInfoBoxCondition() : this(Guid.Empty)
        {
        }

        public MandatoryInfoBoxCondition(IInfoBoxProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public MandatoryInfoBoxCondition(Guid guid)
        {
            Data.Target = new SingleScenePropertyReference<IInfoBoxProperty>(guid);
        }

        /// <inheritdoc />
        public override IStageProcess GetActiveProcess()
        {
            return new ActiveProcess(Data);
        }

        public override IStageProcess GetActivatingProcess()
        {
            return new ActivatingProcess(Data);
        }

        public override IStageProcess GetDeactivatingProcess()
        {
            return new DeactivatingProcess(Data);
        }

        protected static IInfoBoxProperty FindValidInfobox()
        {
            return InterfaceExtensions.FindInterfaceOfType<IInfoBoxProperty>().FirstOrDefault(infoBox => infoBox.IsValid && infoBox.InfoBoxType == InfoBoxType.Mandatory);
        }

    }
}
