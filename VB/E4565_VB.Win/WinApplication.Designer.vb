Imports Microsoft.VisualBasic
Imports System

Partial Public Class E4565_VBWindowsFormsApplication
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary> 
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (Not components Is Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
        Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
		Me.module3 = New Global.E4565_VB.Module.E4565_VBModule()
        Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()

		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        ' 
		' sqlConnection1
		' 
        Me.sqlConnection1.ConnectionString = "Integrated Security=SSPI;Pooling=false;Data Source=.\SQLEXPRESS;Initial Catalog=E4565_VB"
		Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
		' 
		' E4565_VBWindowsFormsApplication
		' 
		Me.ApplicationName = "E4565_VB"
		Me.Connection = Me.sqlConnection1
		Me.Modules.Add(Me.module1)
		Me.Modules.Add(Me.module2)
		Me.Modules.Add(Me.module3)

        AddHandler Me.DatabaseVersionMismatch, AddressOf E4565_VBWindowsFormsApplication_DatabaseVersionMismatch
        AddHandler Me.CustomizeLanguagesList, AddressOf E4565_VBWindowsFormsApplication_CustomizeLanguagesList

		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub

#End Region

	Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
    Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
	Private module3 As Global.E4565_VB.Module.E4565_VBModule
    Private sqlConnection1 As System.Data.SqlClient.SqlConnection
End Class
