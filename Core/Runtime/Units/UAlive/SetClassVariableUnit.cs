using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("Set Class Variable")]
    [UnitCategory("Variables")]
    public sealed class SetClassVariableUnit : ClassVariableUnit
    {
        public Variable variable;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput value;

        private bool justDefined;
         
        protected override void Definition()
        {
            base.Definition();

            enter = ControlInput("enter", (flow) => 
            {
                IUAClass _target;
                
                if (target.hasValidConnection)
                {
                    _target = flow.GetValue<IUAClass>(target);
                }
                else
                {
                    _target = (IUAClass)flow.variables.Get("#secret_uaclass_instance");
                }

                _target.Class.Set(memberName, flow.GetValue(value, variable == null ? typeof(object) : variable.type));
                return exit;
            });
            exit = ControlOutput("exit");

            if (variable != null) value = ValueInput(variable.type, "value");

            if (variable != null)
            {
                if (!variable.setUnits.Contains(this)) variable.setUnits.Add(this);
            }

            Requirement(target, enter);
            Succession(enter, exit);
        }
    }
}