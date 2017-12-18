using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using HeatWaveTest.EntityFramework.Components;

namespace HeatWaveTest.EntityFramework.Systems
{
    class ScriptingSystem : System
    {
        public override ComponentSelector ComponentSelector { get; } = 
            new ComponentSelector(typeof(Velocity), typeof(Position), typeof(PreUpdateScript));

        private Lua LuaState { get; set; }
        private string lastScript = "";
        private LuaFunction lastFunc = null;

        public ScriptingSystem()
        {
            LuaState = new Lua();
        }

        public override void Begin()
        {
            
        }

        public override void End()
        {
            
        }

        public override void Process(Entity entity)
        {
            Velocity velocity = entity.GetComponent<Velocity>();
            Position position = entity.GetComponent<Position>();
            PreUpdateScript script = entity.GetComponent<PreUpdateScript>();

            if (!(lastScript == script.Script))
            {
                LuaState.DoString(script.Script);
                lastFunc = (LuaFunction)LuaState[script.FuncName];
                lastScript = script.Script;
            }

            lastFunc.Call(position, velocity);
        }
    }
}
