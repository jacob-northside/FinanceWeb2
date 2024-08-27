Public Class Departments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        divAccounting.Visible = False
        divCampusOps.Visible = False
        divFPandA.Visible = False
        divManagedCare.Visible = False
        divProductivityMgt.Visible = False
        divRevenueIntegraty.Visible = False
    End Sub

End Class