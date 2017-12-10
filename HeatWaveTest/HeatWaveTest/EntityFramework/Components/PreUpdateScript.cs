namespace HeatWaveTest.EntityFramework.Components
{
    public class PreUpdateScript : Component
    {
        public string Script { get; set; }
        public string FuncName { get; set; }

        public PreUpdateScript(string script, string funcName)
        {
            Script = script;
            FuncName = funcName;
        }
    }
}
