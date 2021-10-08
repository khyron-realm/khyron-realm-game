using UnityEngine;
using System.Collections;
using System;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Threading;

namespace DarkRift.Server.Unity
{
    [AddComponentMenu("DarkRift/Server")]
    public class XmlUnityServer : MonoBehaviour
    {
        /// <summary>
        ///     The actual server.
        /// </summary>
        public DarkRiftServer Server { get; private set; }

#pragma warning disable IDE0044 // Add readonly modifier, Unity can't serialize readonly fields
        [SerializeField]
        [Tooltip("The configuration file to use.")]
        private TextAsset configuration;

        [SerializeField]
        [Tooltip("Indicates whether the server will be created in the OnEnable method.")]
        private bool createOnEnable = true;

        [SerializeField]
        [Tooltip("Indicates whether the server events will be routed through the dispatcher or just invoked.")]
        private bool eventsFromDispatcher = true;
#pragma warning restore IDE0044 // Add readonly modifier, Unity can't serialize readonly fields

        private void OnEnable()
        {
            //If createOnEnable is selected create a server
            if (createOnEnable)
                Create();
        }

        private void Update()
        {
            //Execute all queued dispatcher tasks
            if (Server != null)
                Server.ExecuteDispatcherTasks();
        }

        /// <summary>
        ///     Creates the server.
        /// </summary>
        public void Create()
        {
            Create(new NameValueCollection());
        }

        /// <summary>
        ///     Creates the server.
        /// </summary>
        public void Create(NameValueCollection variables)
        {
            if (Server != null)
                throw new InvalidOperationException("The server has already been created! (Is CreateOnEnable enabled?)");
            
            if (configuration != null)
            {
                // Create spawn data from config
                ServerSpawnData spawnData = ServerSpawnData.CreateFromXml(XDocument.Parse(configuration.text), variables);

                // Allow only this thread to execute dispatcher tasks to enable deadlock protection
                spawnData.DispatcherExecutorThreadID = Thread.CurrentThread.ManagedThreadId;

                // Inaccessible from XML, set from inspector
                spawnData.EventsFromDispatcher = eventsFromDispatcher;

                // Unity is broken, work around it...
                // This is an obsolete property but is still used if the user is using obsolete <server> tag properties
#pragma warning disable 0618
                spawnData.Server.UseFallbackNetworking = true;
#pragma warning restore 0618

                // Add types
                spawnData.PluginSearch.PluginTypes.AddRange(UnityServerHelper.SearchForPlugins());
                spawnData.PluginSearch.PluginTypes.Add(typeof(UnityConsoleWriter));

                // Create server
                Server = new DarkRiftServer(spawnData);
                Server.Start();
            }
            else
                Debug.LogError("No configuration file specified!");
        }

        private void OnDisable()
        {
            Close();
        }

        private void OnApplicationQuit()
        {
            Close();
        }

        /// <summary>
        ///     Closes the server.
        /// </summary>
        public void Close()
        {
            if (Server != null)
                Server.Dispose();
        }
    }
}
