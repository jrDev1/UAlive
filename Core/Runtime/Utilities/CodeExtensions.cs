using System;

namespace Lasm.UAlive
{
    public static class CodeExtensions
    {
        public static string Invoke(string key, Type returnType, bool isOverride, string parameters)
        {
            var output = string.Empty;
            var bodyOpen = "data.Invoke(";
            var instance = "this" + CodeBuilder.Comma();
            var _key = CodeBuilder.Qoute() + key + CodeBuilder.Qoute() + CodeBuilder.Comma();
            var callback = HUMType.Is(returnType).NullOrVoid() ? "null" : CodeBuilder.SingleLineLambda("obj", CodeBuilder.Assign("val", "obj", returnType));
            var _parameters = CodeBuilder.NullAsEmptyOr(parameters, CodeBuilder.Comma() + parameters);
            var bodyClose = CodeBuilder.End();

            output += bodyOpen + instance + _key + callback + ", " + isOverride.As().Code(false) + _parameters + bodyClose;
            return output;
        }
    }
}