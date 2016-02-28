Public Class RichTextBoxEx

    Private Sub RichTextBoxEx_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Load control        
        rtb.Focus()
    End Sub

    ' Spell checking thanks to: http://www.codeproject.com/KB/string/netspell.aspx

    ' Handles when user chooses to delete in spell cehck
    Private Sub SpellChecker_DeletedWord(ByVal sender As Object, ByVal e As NetSpell.SpellChecker.SpellingEventArgs) Handles SpellChecker.DeletedWord
        'save existing selecting
        Dim start As Integer = rtb.SelectionStart
        Dim length As Integer = rtb.SelectionLength

        'select word for this event
        rtb.Select(e.TextIndex, e.Word.Length)

        'delete word
        rtb.SelectedText = ""

        If ((start + length) > rtb.Text.Length) Then
            length = 0
        End If

        'restore selection
        rtb.Select(start, length)

    End Sub

    ' Handles replacing a word from spell checking
    Private Sub SpellChecker_ReplacedWord(ByVal sender As Object, ByVal e As NetSpell.SpellChecker.ReplaceWordEventArgs) Handles SpellChecker.ReplacedWord
        'save existing selecting
        Dim start As Integer = rtb.SelectionStart
        Dim length As Integer = rtb.SelectionLength

        'select word for this event
        rtb.Select(e.TextIndex, e.Word.Length)

        'replace word
        rtb.SelectedText = e.ReplacementWord

        If ((start + length) > rtb.Text.Length) Then
            length = 0
        End If

        'restore selection
        rtb.Select(start, length)
    End Sub

    ' Update buttons when text is selected
    Private Sub rtb_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb.SelectionChanged
        ' see which buttons should be checked or unchecked
        BoldToolStripButton.Checked = rtb.SelectionFont.Bold
        UnderlineToolStripButton.Checked = rtb.SelectionFont.Underline
        LeftToolStripButton.Checked = IIf(rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left, True, False)
        CenterToolStripButton.Checked = IIf(rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center, True, False)
        RightToolStripButton.Checked = IIf(rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right, True, False)
        BulletsToolStripButton.Checked = rtb.SelectionBullet

        'cmbFontName.Text = rtb.SelectionFont.Name
        'cmbFontSize.Text = rtb.SelectionFont.SizeInPoints

    End Sub

    Private Sub checkBullets()
        If rtb.SelectionBullet = True Then
            BulletsToolStripButton.Checked = True
        Else
            BulletsToolStripButton.Checked = False
        End If
    End Sub

    Private Sub FontToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontToolStripButton.Click
        If FontDlg.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
            rtb.SelectionFont = FontDlg.Font
        End If
    End Sub

    Private Sub FontColorToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontColorToolStripButton.Click
        If ColorDlg.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
            rtb.SelectionColor = ColorDlg.Color
        End If
    End Sub

    Private Sub BoldToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BoldToolStripButton.Click
        ' Switch Bold
        Dim currentFont As System.Drawing.Font = rtb.SelectionFont
        Dim newFontStyle As System.Drawing.FontStyle
        If rtb.SelectionFont.Bold = True Then
            newFontStyle = currentFont.Style - Drawing.FontStyle.Bold
        Else
            newFontStyle = currentFont.Style + Drawing.FontStyle.Bold
        End If
        rtb.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        ' Check/Uncheck Bold button
        BoldToolStripButton.Checked = IIf(rtb.SelectionFont.Bold, True, False)
    End Sub

    Private Sub SpellcheckToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpellcheckToolStripButton.Click
        SpellChecker.Text = rtb.Text
        SpellChecker.SpellCheck()
    End Sub

    Private Sub UnderlineToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnderlineToolStripButton.Click
        ' Switch Underline
        Dim currentFont As System.Drawing.Font = rtb.SelectionFont
        Dim newFontStyle As System.Drawing.FontStyle
        If rtb.SelectionFont.Underline = True Then
            newFontStyle = currentFont.Style - Drawing.FontStyle.Underline
        Else
            newFontStyle = currentFont.Style + Drawing.FontStyle.Underline
        End If
        rtb.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        ' Check/Uncheck Underline button
        UnderlineToolStripButton.Checked = IIf(rtb.SelectionFont.Underline, True, False)
    End Sub

    Private Sub LeftToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftToolStripButton.Click
        rtb.SelectionAlignment = HorizontalAlignment.Left
    End Sub

    Private Sub CenterToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CenterToolStripButton.Click
        rtb.SelectionAlignment = HorizontalAlignment.Center
    End Sub

    Private Sub RightToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightToolStripButton.Click
        rtb.SelectionAlignment = HorizontalAlignment.Right
    End Sub

    Private Sub BulletsToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BulletsToolStripButton.Click
        rtb.SelectionBullet = Not rtb.SelectionBullet
        BulletsToolStripButton.Checked = rtb.SelectionBullet
    End Sub
End Class
