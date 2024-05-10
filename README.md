Code for check Entitlement of a user from AutoDesk store
Using:

      [CommandMethod("CheckEntitlement", CommandFlags.Modal)]
      public void MyCommand_CheckEntitlement()
      {
          Document doc = Application.DocumentManager.MdiActiveDocument;            
          if (doc == null) return;
			Editor ed = doc.Editor;
			
			var HasEntitlement = AJS_Lics.CheckOnline();
   
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

Source: https://www.lisp.vn/2024/05/kiem-tra-ban-quyen-tren-autodesk-store.html
