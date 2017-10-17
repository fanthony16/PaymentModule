Imports Microsoft.VisualBasic

Public Class CreateItemTemplate
     Implements ITemplate
     Private myListItemType As ListItemType
     Private _listItemType As ListItemType

     Sub New(listItemType As ListItemType)
          ' TODO: Complete member initialization 
          _listItemType = listItemType
     End Sub

     Public Sub InstantiateIn(container As Control) Implements ITemplate.InstantiateIn

          If myListItemType = ListItemType.Item Then

               Dim txtCashCheque As New TextBox
               container.Controls.Add(txtCashCheque)

          End If
            
     End Sub

     Public Sub CreateItemTemplate(Item As ListItemType)
          myListItemType = Item
     End Sub

End Class
