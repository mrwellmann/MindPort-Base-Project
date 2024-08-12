using VRBuilder.Core.Attributes;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using System.Runtime.Serialization;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core;
using VRBuilder.UI.Properties;
using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace VRBuilder.UI.Behaviors
{
    /// <summary>
    /// Localize a string for TextMesh, UI Text, or TextMeshPro (TMP) Components.
    /// </summary>
    [DataContract(IsReference = true)]
    public class SetLocalizedTextBehavior : Behavior<SetLocalizedTextBehavior.EntityData>
    {
        [DisplayName("Set Localized Text")]
        [DataContract(IsReference = true)]
        public class EntityData : IBehaviorData
        {
            [DataMember(IsRequired = false)]
            [DisplayName("Text Component")]
            public SingleScenePropertyReference<ILocalizedTextProperty> Target { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<ILocalizedTextProperty> TextComponent { get; set; }

            [DataMember]
            [DisplayName("Loca. Table (Optional)")]
            public string LocalizationTable;

            [DataMember]
            [DisplayName("Text/Key to Set")]
            public string Text;

            [DataMember]
            [DisplayName("Append Text")]
            public bool AppendText = false;

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"Set Localized Text for: {Target}";

            public Metadata Metadata { get; set; }
        }

        private class ActivatingProcess : InstantProcess<EntityData>
        {
            public ActivatingProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                if(Data.AppendText)
                    Data.Target.Value.AppendText(Data.LocalizationTable, Data.Text);
                else
                    Data.Target.Value.SetText(Data.LocalizationTable, Data.Text);
            }
        }

        [JsonConstructor, Preserve]
        public SetLocalizedTextBehavior() : this(Guid.Empty)
        {
        }

        public SetLocalizedTextBehavior(ILocalizedTextProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public SetLocalizedTextBehavior(Guid guid)
        {
            Data.Target = new SingleScenePropertyReference<ILocalizedTextProperty>(guid);
        }

        public override IStageProcess GetActivatingProcess()
        {
            return new ActivatingProcess(Data);
        }


    }
}
