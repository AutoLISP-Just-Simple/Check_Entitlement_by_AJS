Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Runtime

<Assembly: CommandClass(GetType(Check_Entitlement_by_AJS.MyCommands))>
Namespace Check_Entitlement_by_AJS
    Public Class MyCommands
        <CommandMethod("CheckEntitlement", CommandFlags.Modal)>
        Public Sub MyCommand_CheckEntitlement()
            Dim doc As Document = Application.DocumentManager.MdiActiveDocument
            If doc Is Nothing Then Return
            Dim ed As Editor = doc.Editor
            Dim HasEntitlement = AJS_Lics.CheckOnline(True)

            If Not HasEntitlement Then
                ed.WriteMessage("You are not entitled to use this plug-in!")
                Return
            End If

            ed.WriteMessage("You are entitled to use this plug-in! Continue")
        End Sub
    End Class
End Namespace
