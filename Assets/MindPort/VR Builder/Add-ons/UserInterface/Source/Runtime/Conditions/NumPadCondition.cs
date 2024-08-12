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
    /// A condition that completes when the 'Enter' Button on the Num Pad is pressed
    /// </summary>
    [DataContract(IsReference = true)]
    public class NumPadCondition : Condition<NumPadCondition.EntityData>
    {
        [DisplayName("Num Pad Enter Button")]
        public class EntityData : IConditionData
        {
            [DataMember]
            [DisplayName("Num Pad")]
            public SingleScenePropertyReference<INumPadProperty> Target { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<INumPadProperty> NumPad { get; set; }

            [DataMember(IsRequired = false)]
            [DisplayName("Reset Entered Value after Step")]
            public bool ResetEnteredValue { get; set; } = true;

            public bool IsCompleted { get; set; }

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"Num Pad Enter: {Target}";

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
                    numPad?.ResetNumPad(true);
                    numPad?.SetNumPadVisibility(true);
                    numPad.InitNumPad();
                }
                else
                {
                    var sceneNumPad = FindValidNumPad();
                    sceneNumPad?.ResetNumPad(true);
                    sceneNumPad?.SetNumPadVisibility(true);
                    sceneNumPad?.InitNumPad();
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
                var numPad = Data.Target.Value;
                if (numPad != null)
                {
                    return Data.Target.Value.IsDataAccepted();
                }
                else
                {
                    var sceneNumPad = FindValidNumPad();
                    if (sceneNumPad != null)
                    {
                        return sceneNumPad.IsDataAccepted();
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
                var numPad = Data.Target.Value;
                if (numPad != null)
                {
                    numPad?.SetNumPadVisibility(false);
                    numPad?.ResetNumPad(Data.ResetEnteredValue);
                }
                else
                {
                    var sceneNumPad = FindValidNumPad();
                    sceneNumPad?.SetNumPadVisibility(false);
                    sceneNumPad?.ResetNumPad(Data.ResetEnteredValue);
                }
            }
        }

        [JsonConstructor, Preserve]
        public NumPadCondition() : this(Guid.Empty)
        {
        }

        public NumPadCondition(INumPadProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public NumPadCondition(Guid guid)
        {
            Data.Target = new SingleScenePropertyReference<INumPadProperty>(guid);
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

        protected static INumPadProperty FindValidNumPad()
        {
            return InterfaceExtensions.FindInterfaceOfType<INumPadProperty>().FirstOrDefault(numPad => numPad.IsValid);
        }
    }
}