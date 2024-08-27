Imports System.IO
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.HSSF.Util
Imports NPOI.POIFS.FileSystem
Imports NPOI.HPSF

Public Class NpoiExport
    Implements IDisposable
    Const MaximumNumberOfRowsPerSheet As Integer = 65500
    Const MaximumSheetNameLength As Integer = 25
    Protected Property Workbook() As HSSFWorkbook
        Get
            Return m_Workbook
        End Get
        Set(value As HSSFWorkbook)
            m_Workbook = value
        End Set
    End Property
    Private m_Workbook As HSSFWorkbook

    Public Sub New()
        Me.Workbook = New HSSFWorkbook()
    End Sub

    Protected Function EscapeSheetName(sheetName As String) As String
        Dim escapedSheetName = sheetName.Replace("/", "-").Replace("\", " ").Replace("?", String.Empty).Replace("*", String.Empty).Replace("[", String.Empty).Replace("]", String.Empty).Replace(":", String.Empty)

        If escapedSheetName.Length > MaximumSheetNameLength Then
            escapedSheetName = escapedSheetName.Substring(0, MaximumSheetNameLength)
        End If

        Return escapedSheetName
    End Function

    Protected Function CreateExportDataTableSheetAndHeaderRow(exportData As DataTable, sheetName As String, headerRowStyle As HSSFCellStyle) As HSSFSheet
        Dim sheet = Me.Workbook.CreateSheet(EscapeSheetName(sheetName))

        ' Create the header row
        Dim row = sheet.CreateRow(0)

        For colIndex As Integer = 0 To exportData.Columns.Count - 1
            Dim cell = row.CreateCell(colIndex)
            cell.SetCellValue(exportData.Columns(colIndex).ColumnName)

            If headerRowStyle IsNot Nothing Then
                cell.CellStyle = headerRowStyle
            End If
        Next

        Return sheet
    End Function

    Public Sub ExportDataTableToWorkbook(exportData As DataTable, sheetName As String)
        ' Create the header row cell style
        Dim headerLabelCellStyle = Me.Workbook.CreateCellStyle()
        'headerLabelCellStyle.BorderBottom = BorderStyle.Thin ' CellBorderType.THIN
        Dim headerLabelFont = Me.Workbook.CreateFont()
        headerLabelFont.Boldweight = CShort(FontBoldWeight.Bold)
        headerLabelCellStyle.SetFont(headerLabelFont)

        Dim sheet = CreateExportDataTableSheetAndHeaderRow(exportData, sheetName, headerLabelCellStyle)
        Dim currentNPOIRowIndex = 0
        Dim sheetCount = 1

        For rowIndex As Integer = 0 To exportData.Rows.Count - 1
            If currentNPOIRowIndex >= MaximumNumberOfRowsPerSheet Then
                sheetCount += 1
                currentNPOIRowIndex = 1

                sheet = CreateExportDataTableSheetAndHeaderRow(exportData, sheetName & " - " & sheetCount, headerLabelCellStyle)
            End If

            Dim row = sheet.CreateRow(System.Math.Max(System.Threading.Interlocked.Increment(currentNPOIRowIndex), currentNPOIRowIndex - 1))

            For colIndex As Integer = 0 To exportData.Columns.Count - 1
                Dim cell = row.CreateCell(colIndex)
                cell.SetCellValue(exportData.Rows(rowIndex)(colIndex).ToString())
            Next
        Next
    End Sub

    Public Function GetBytes() As Byte()
        Using buffer = New MemoryStream()
            Me.Workbook.Write(buffer)
            Return buffer.GetBuffer()
        End Using
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        If Me.Workbook IsNot Nothing Then
            'Me.Workbook.Dispose()
            Me.Workbook.Clear()
        End If
    End Sub
End Class
