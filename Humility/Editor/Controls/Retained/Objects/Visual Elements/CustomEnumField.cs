﻿using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Lasm.UAlive
{
    public class CustomEnumField : AdvancedField<EnumField, Enum>
    {
        public CustomEnumField(string label, Enum @default, LabelPosition position = LabelPosition.Left, int minFieldWidth = 120, VisualElement prependLabel = null, VisualElement appendLabel = null, Action<EnumField, Enum> onValueChanged = null) : base(label, @default, position, minFieldWidth, prependLabel, appendLabel, onValueChanged)
        {
        }
    }
}
