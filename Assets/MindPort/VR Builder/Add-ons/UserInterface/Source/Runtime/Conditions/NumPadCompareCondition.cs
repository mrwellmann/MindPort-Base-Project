using VRBuilder.Core.Attributes;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using VRBuilder.Core.Properties;
using VRBuilder.Core.ProcessUtils;
using System.Runtime.Serialization;
using System.Linq;
using VRBuilder.Core.Conditions;
using VRBuilder.Core;
using VRBuilder.UI.Properties;
using VRBuilder.UI.Utils;
using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using VRBuilder.BasicInteraction.Properties;

namespace VRBuilder.UI.Conditions
{
    /// <summary>
    /// A condition that compares two <see cref="IDataProperty{T}"/>s and completes when the comparison returns true.
    /// </summary>
    [DataContract(IsReference = true)]
    public class NumPadCompareCondition : Condition<NumPadCompareCondition.EntityData>
    {
        /// <summary>
        /// The data for a <see cref="NumPadCompareCondition{T}"/>
        /// </summary>
        [DisplayName("Compare Num Pad Numbers")]
        public class EntityData : IConditionData
        {
            [DataMember]
            [HideInProcessInspector]
            [DisplayName("Left Value")]
            public SingleScenePropertyReference<INumPadProperty> LeftTarget { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Left Target instead.")]
            public ScenePropertyReference<INumPadProperty> LeftValueProperty { get; set; }

            [DataMember]
            [HideInProcessInspector]
            public IOperationCommand<int, bool> Operation { get; set; }

            [DataMember]
            [HideInProcessInspector]
            public int CompareValue { get; set; }

            public bool IsCompleted { get; set; }

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name
            {
                get
                {
                    return $"Compare ({LeftTarget} {Operation} {CompareValue})";
                }
            }

            public Metadata Metadata { get; set; }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            public ActiveProcess(EntityData data) : base(data)
            {
            }

            /// <inheritdoc />
            protected override bool CheckIfCompleted()
            {
                var leftValuePropertyNumPad = Data.LeftTarget.Value;
                if (leftValuePropertyNumPad == null)
                {
                    leftValuePropertyNumPad = FindValidNumPad();
                }

                int left = leftValuePropertyNumPad.GetValue();
                int right = Data.CompareValue;

                return Data.Operation.Execute(left, right);
            }
        }

        [JsonConstructor, Preserve]
        public NumPadCompareCondition() : this(Guid.Empty, default, new EqualToOperation<int>())
        {
        }

        public NumPadCompareCondition(INumPadProperty leftTarget, int rightValue, IOperationCommand<int, bool> operation) : this(ProcessReferenceUtils.GetUniqueIdFrom(leftTarget), rightValue, operation)
        {
        }

        public NumPadCompareCondition(Guid leftTarget, int rightValue, IOperationCommand<int, bool> operation)
        {
            Data.LeftTarget = new SingleScenePropertyReference<INumPadProperty>(leftTarget);
            Data.CompareValue = rightValue;
            Data.Operation = operation;
        }

        /// <inheritdoc />
        public override IStageProcess GetActiveProcess()
        {
            return new ActiveProcess(Data);
        }

        protected static INumPadProperty FindValidNumPad()
        {
            return InterfaceExtensions.FindInterfaceOfType<INumPadProperty>().FirstOrDefault(numPad => numPad.IsValid);
        }
    }
}