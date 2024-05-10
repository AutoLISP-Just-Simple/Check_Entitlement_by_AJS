// (C) Copyright 2024 by
//
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(Check_Entitlement_by_AJS.MyCommands))]

namespace Check_Entitlement_by_AJS
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class MyCommands
    {
        [CommandMethod("CheckEntitlement", CommandFlags.Modal)]
        public void MyCommand_CheckEntitlement()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null) return;
            Editor ed = doc.Editor;

            var HasEntitlement = AJS_Lics.CheckOnline(true);
            if (!HasEntitlement)
            {
                ed.WriteMessage("You are not entitled to use this plug-in!");
                return;
            }

            ed.WriteMessage("You are entitled to use this plug-in! Continue");
            // Put your command code here
            // Put your command code here
            // Put your command code here

            //
            //Example by www.lisp.vn
        }
    }
}