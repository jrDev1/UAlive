using Bolt;
using Ludiq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lasm.UAlive
{
    [InitializeAfterPlugins]
    public static class ClassMemberExtensions
    {
        static ClassMemberExtensions()
        {
            UnitBase.dynamicUnitsExtensions.Add(GetDynamicOptions);
        }

        private static IEnumerable<IUnitOption> GetDynamicOptions()
        {
            var customClasses = UnityAPI.Await(() => AssetUtility.GetAllAssetsOfType<CustomClass>().ToArray());

            foreach (CustomClass @class in customClasses)
            {
                foreach (var invokeUnit in @class.methods.custom.Select(method => new InvokeUnit(@class, method)))
                {
                    if (string.IsNullOrEmpty(invokeUnit.method.name)) yield return null;
                    yield return invokeUnit.Option();
                }

                foreach (var getUnit in @class.variables.variables.Select(variable => new GetClassVariableUnit(variable, @class)))
                {
                    if (string.IsNullOrEmpty(getUnit.variable.name)) yield return null;
                    yield return getUnit.Option();
                }

                foreach (var setUnit in @class.variables.variables.Select(variable => new SetClassVariableUnit(variable, @class)))
                {
                    if (string.IsNullOrEmpty(setUnit.variable?.name)) yield return null;
                    yield return setUnit.Option();
                }
            }

            foreach (CustomClass @class in customClasses)
            {
            }
        }
    }
}