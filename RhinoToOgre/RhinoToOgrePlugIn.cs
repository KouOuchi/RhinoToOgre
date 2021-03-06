﻿namespace RhinoToOgre
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class OpenCAMPlugIn : Rhino.PlugIns.PlugIn
    {
        public OpenCAMPlugIn()
        {
            Instance = this;
        }


        ///<summary>Gets the only instance of the OpenCAMPlugIn plug-in.</summary>
        public static OpenCAMPlugIn Instance
        {
            get;
            private set;
        }

        public override Rhino.PlugIns.PlugInLoadTime LoadTime
        {
            get
            {
                return Rhino.PlugIns.PlugInLoadTime.AtStartup;
            }
        }

        //public Icon Resource { get; private set; }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and mantain plug-in wide options in a document.
        protected override Rhino.PlugIns.LoadReturnCode OnLoad(ref string errorMessage)
        {
            return base.OnLoad(ref errorMessage);
        }
        protected string Toolbar()
        {
            return string.Empty;
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
    }
}