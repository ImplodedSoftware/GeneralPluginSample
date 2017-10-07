using System;
using System.Diagnostics;
using ImGeneralPluginEngine;
using NeonScripting;

namespace GeneralPluginSample
{
    public class TestPlugin : IImGeneralPlugin
    {

        private SampleForm _form;
        public void InitializePlugin(INeonScriptHost host, Action<IImGeneralPlugin> pluginCloseAction)
        {
            _form = new SampleForm {ScriptHost = host};
            _form.Populate();
            _form.ShowDialog();
            pluginCloseAction?.Invoke(this);
        }

        public void OnEvent(NeonEventTypes eventType)
        {
            Debug.WriteLine("*** ON PLUGIN EVENT");
        }

        public void ClosePlugin()
        {
            _form.Close();
        }

        public string Name => "Sample c# plugin";
        public string Author => "Mikael Stalvik";
    }
}
