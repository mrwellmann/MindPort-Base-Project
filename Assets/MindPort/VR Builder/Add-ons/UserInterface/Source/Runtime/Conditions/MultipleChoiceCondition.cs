using System.Linq;
using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using VRBuilder.Core.Conditions;
using VRBuilder.Core;
using VRBuilder.UI.Properties;
using VRBuilder.UI.Utils;
using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace VRBuilder.UI.Conditions
{
    /// <summary>
    /// A condition that creates a Multiple Choice Button and is completed when that Button is Acknowledged
    /// </summary>
    [DataContract(IsReference = true)]
    public class MultipleChoiceCondition : Condition<MultipleChoiceCondition.EntityData>
    {
        [DisplayName("Multiple Choice Button")]
        public class EntityData : IConditionData
        {
            [DataMember(IsRequired = false)]
            [DisplayName("Loca. Table (Optional)")]
            public string LocalizationTable;

            [DataMember(IsRequired = true)]
            [DisplayName("Button Text / Key")]
            public string ButtonText { get; set; }

            [DataMember(IsRequired = true)]
            [DisplayName("Text Key is Sprite Resource Path")]
            public bool IsSprite { get; set; }

            [DataMember]
            [DisplayName("Multiple Choice Box")]
            public SingleScenePropertyReference<IMultipleChoiceProperty> Target { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<IMultipleChoiceProperty> MultipleChoiceBox { get; set; }

            public bool IsCompleted { get; set; }

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"Multiple Choice Box: {Target}";

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
                    multipleChoiceBox.CreateMultipleChoiceButton(Data.LocalizationTable, Data.ButtonText, Data.IsSprite);
                    multipleChoiceBox.ShowMultipleChoiceBox(0f);
                }
                else
                {
                    var sceneBox = FindValidMultipleChoiceBox();
                    sceneBox?.CreateMultipleChoiceButton(Data.LocalizationTable, Data.ButtonText, Data.IsSprite);
                    sceneBox?.ShowMultipleChoiceBox(0f);
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
                var multipleChoiceBox = Data.Target.Value;
                if (multipleChoiceBox != null)
                {
                    return Data.Target.Value.IsButtonPressed(Data.LocalizationTable, Data.ButtonText, Data.IsSprite);
                }
                else
                {
                    var sceneBox = FindValidMultipleChoiceBox(); 
                    if (sceneBox != null)
                    {
                        return sceneBox.IsButtonPressed(Data.LocalizationTable, Data.ButtonText, Data.IsSprite);
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
                var multipleChoiceBox = Data.Target.Value;
                if (multipleChoiceBox != null)
                {
                    multipleChoiceBox.HideMultipleChoiceBox();
                }
                else
                {
                    var sceneBox = FindValidMultipleChoiceBox();
                    sceneBox?.HideMultipleChoiceBox();
                }
            }
        }

        [JsonConstructor, Preserve]
        public MultipleChoiceCondition() : this(Guid.Empty)
        {
        }

        public MultipleChoiceCondition(IMultipleChoiceProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public MultipleChoiceCondition(Guid guid)
        {
            Data.Target = new SingleScenePropertyReference<IMultipleChoiceProperty>(guid);
            Data.LocalizationTable = "";
            Data.ButtonText = "X";
        }

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

        protected static IMultipleChoiceProperty FindValidMultipleChoiceBox()
        {
            return InterfaceExtensions.FindInterfaceOfType<IMultipleChoiceProperty>().FirstOrDefault(mcBox => mcBox.IsValid);
        }

    }
}
