﻿using Bolt;
using Ludiq;
using UnityEngine;

namespace Lasm.BoltExtensions
{
    [Widget(typeof(ConvertUnit))]
    public class ConvertUnitWidget : UnitWidget<ConvertUnit>
    {
        public ConvertUnitWidget(FlowCanvas canvas, ConvertUnit unit) : base(canvas, unit)
        {
        }

        protected override bool showHeaderAddon => true;
        public override bool foregroundRequiresInput => true;
        private ConversionType lastConversionType;

        protected override void DrawHeaderAddon()
        {
            if (unit.conversion == ConversionType.ToArrayOfObject || unit.conversion == ConversionType.ToListOfObject)
            {
                LudiqGUI.Inspector(metadata["conversion"], new Rect(headerAddonPosition.x, headerAddonPosition.y, GetHeaderAddonWidth(), 18), GUIContent.none);
            }
            else
            {
                LudiqGUI.Inspector(metadata["conversion"], new Rect(headerAddonPosition.x, headerAddonPosition.y, GetHeaderAddonWidth(), 18), GUIContent.none);
                LudiqGUI.Inspector(metadata["type"], new Rect(headerAddonPosition.x, headerAddonPosition.y + 20, GetHeaderAddonWidth(), 18), GUIContent.none);
            }

            if (lastConversionType != unit.conversion)
            {
                lastConversionType = unit.conversion;
                Reposition();
            }
        }

        protected override float GetHeaderAddonWidth()
        {
            return 130;
        }

        protected override float GetHeaderAddonHeight(float width)
        {
            if (unit.conversion == ConversionType.ToArrayOfObject || unit.conversion == ConversionType.ToListOfObject) return 20;
            return 40;
        }
    }
} 