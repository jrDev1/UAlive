﻿using Bolt;
using Ludiq;
using System.Linq;

namespace Lasm.UAlive
{
    public static class GraphExtensions
    {
        public static void DefineAllGraphs()
        {
            var assets = HUMAssets.Find().Assets().OfType<ClassMacro>();

            for (int i = 0; i < assets.Count; i++)
            {
                for (int j = 0; j < assets[i].methods.custom.Count; j++)
                {
                    var units = assets[i].methods.custom[j].macro.graph.units.ToArrayPooled();

                    for (int unit = 0; unit < units.Length; unit++)
                    {
                        units[unit].Define();
                    }
                }
            }
        }

        public static void DefineAllGraphsOfType<T>() where T : IUnit
        {
            var assets = HUMAssets.Find().Assets().OfType<ClassMacro>();
            for (int i = 0; i < assets.Count; i++)
            {
                for (int j = 0; j < assets[i].methods.custom.Count; j++)
                {
                    var units = assets[i].methods.custom[j].macro.graph.units.Where((u) => { return u.GetType() == typeof(T); }).ToArrayPooled();
                    for (int unit = 0; unit < units.Length; unit++)
                    {
                        units[unit].Define();
                    }
                }
            }
        }
    }
}