Imports GameOfLife
Imports SFML.Graphics
Imports SFML.Window

Public Class Menu
    Implements Scene

    Private WithEvents _Window As RenderWindow
    Private _Texte_Titre As New Text("Game Of Life", Font_Sensation)
    Private _Texte_OptionJouer As New Text("1 - Jouer", Font_Sensation)
    Private _Texte_OptionQuitter As New Text("2 - Quitter", Font_Sensation)

    Sub New(ByRef Window As RenderWindow)
        _Window = Window
        SetUpMenu()
    End Sub

    Public Sub ProcessEvent() Implements Scene.ProcessEvent
        _Window.DispatchEvents()
    End Sub

    Public Sub Update() Implements Scene.Update
    End Sub

    Public Sub Affichage() Implements Scene.Affichage
        _Window.Draw(_Texte_Titre)
        _Window.Draw(_Texte_OptionJouer)
        _Window.Draw(_Texte_OptionQuitter)
    End Sub

    Private Sub SetUpMenu()
        _Texte_Titre.FillColor = Color.Black
        _Texte_Titre.Position = New SFML.System.Vector2f(_Window.Size.X / 3, 0)

        _Texte_OptionJouer.FillColor = Color.Black
        _Texte_OptionJouer.Position = New SFML.System.Vector2f(_Window.Size.X / 3, 100)

        _Texte_OptionQuitter.FillColor = Color.Black
        _Texte_OptionQuitter.Position = New SFML.System.Vector2f(_Window.Size.X / 3, 150)
    End Sub

    Sub App_KeyReleased(ByVal sender As Object, ByVal e As KeyEventArgs) Handles _Window.KeyReleased

        If (e.Code = Keyboard.Key.Num1) Then
            Main.ChangerScene(New Jeu(_Window))
        End If

    End Sub

End Class
