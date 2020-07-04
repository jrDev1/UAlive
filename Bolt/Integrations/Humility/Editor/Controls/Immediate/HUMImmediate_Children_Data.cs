﻿using UnityEditor;
using UnityEngine;

namespace Lasm.BoltExtensions.Humility
{
    public static partial class HUMEditor_Immediate_Children
    {
        public static partial class Data
        {
            public struct Selection
            {
                public HUMEditor.Data.Immediate immediate;

                public Selection(HUMEditor.Data.Immediate immediate)
                {
                    this.immediate = immediate;
                }
            }

            public struct Image
            {
                public HUMEditor.Data.Immediate immediate;

                public Image(HUMEditor.Data.Immediate immediate)
                {
                    this.immediate = immediate;
                }
            }

            public struct Button
            {
                public Image image;

                public Button(Image image)
                {
                    this.image = image;
                }
            }

            public struct Toggle
            {
                public Image image;

                public Toggle(Image image)
                {
                    this.image = image;
                }
            }
        }
    }
}
