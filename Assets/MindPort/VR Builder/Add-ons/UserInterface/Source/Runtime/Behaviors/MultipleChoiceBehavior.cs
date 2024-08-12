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
    /// Setup a Multiple Choice Box 
    /// </summary>
    [DataContract(IsReference = true)]
    public class MultipleChoiceBehavior : Behavior<MultipleChoiceBehavior.EntityData>
    {
        [DisplayName("Multiple Choice Box")]
        [DataContract(IsReference = true)]
        public class EntityData : IBehaviorData
        {
            [DataMember(IsRequired = false)]
            [DisplayName("Loca. Table (Optional)")]
            public string LocalizationTable;

            [DataMember(IsRequired = true)]
            [DisplayName("Title / Key")]
            public string Title { get; set; }

            [DataMember(IsRequired = false)]
            [DisplayName("Hide Delay (Seconds)")]
            public int HideDelay { get; set; }

            [DataMember(IsRequired = false)]
            [DisplayName("Vertical Button Layout")]
            public bool VerticalButtonLayout { get; set; }

            [DataMember(IsRequired = false)]
            [DisplayName("Multiple Choice Box")]
            public SingleScenePropertyReference<IMultipleChoiceProperty> Target { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<IMultipleChoiceProperty> MultipleChoiceBox { get; set; }

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"Setup Multiple Choice Box: {Target}";

            public Metadata Metadata { get; set; }
        }

        private class ActivatingProcess : InstantProcess<EntityData>
        {
            public ActivatingProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                var multipleChoiceBox = Data.Target.Value;
                if (multipleChoiceBox != null)
                {
                    if(Data.HideDelay > 0) multipleChoiceBox.ResetMultipleChoiceBox(); 
                    multipleChoiceBox.SetHideDelay(Data.HideDelay);
                    multipleChoiceBox.SetVerticalButtonLayout(Data.VerticalButtonLayout);
                    multipleChoiceBox.SetMultipleChoiceBoxTitle(Data.LocalizationTable, Data.Title);
                }
                else
                {
                    var sceneBox = FindValidMultipleChoiceBox();
                    if (Data.HideDelay > 0) sceneBox?.ResetMultipleChoiceBox();
                    sceneBox?.SetHideDelay(Data.HideDelay);
                    sceneBox?.SetVerticalButtonLayout(Data.VerticalButtonLayout);
                    sceneBox?.SetMultipleChoiceBoxTitle(Data.LocalizationTable, Data.Title);
                }
            }
        }

        [JsonConstructor, Preserve]
        public MultipleChoiceBehavior() : this(Guid.Empty)
        {
        }

        public MultipleChoiceBehavior(IMultipleChoiceProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public MultipleChoiceBehavior(Guid guid)
        {
            Data.Target = new SingleScenePropertyReference<IMultipleChoiceProperty>(guid);
            Data.LocalizationTable = "";
            Data.Title = "";
        }

        public override IStageProcess GetActivatingProcess()
        {
            return new ActivatingProcess(Data);
        }

        protected static IMultipleChoiceProperty FindValidMultipleChoiceBox()
        {
            return InterfaceExtensions.FindInterfaceOfType<IMultipleChoiceProperty>().FirstOrDefault(mcBox => mcBox.IsValid);
        }

    }
}
