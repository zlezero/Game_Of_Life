Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window

Public Class Jeu
    Implements Scene

    'Variables de jeu
    Private WithEvents _Window As RenderWindow
    Private _TabCases(0, 0) As Boolean
    Private _NbrCases As Integer
    Private _SimulationDemaree As Boolean = False
    Private _TailleCases As Integer = 20
    Private _NbrTours As Integer = 0

    'Variables autres
    Private Ligne As RectangleShape
    Private CaseShape As RectangleShape

    Public Sub New(ByRef Window As RenderWindow)
        _Window = Window
        _NbrCases = CalculerNbrCase()
        ReDim _TabCases(_NbrCases, _NbrCases)
    End Sub

    Public Sub ProcessEvent() Implements Scene.ProcessEvent
        _Window.DispatchEvents()
    End Sub

    Public Sub Update() Implements Scene.Update

        If _SimulationDemaree Then
            NextTurn()
        End If

    End Sub

    Public Sub Affichage() Implements Scene.Affichage
        ShowQuadrillage()
        ShowCases()
    End Sub

    Sub ShowQuadrillage()

        Ligne = New RectangleShape(New SFML.System.Vector2f(_Window.Size.X, 5))
        Ligne.FillColor = Color.Black

        For I As Integer = 0 To _Window.Size.X Step _TailleCases 'Lignes horizontales
            Ligne.Position = New SFML.System.Vector2f(I, 0)
            Ligne.Rotation = 90
            _Window.Draw(Ligne)
        Next

        Ligne.Rotation = 0

        For I As Integer = 0 To _Window.Size.Y Step _TailleCases 'Lignes verticales
            Ligne.Position = New SFML.System.Vector2f(0, I)
            _Window.Draw(Ligne)
        Next

    End Sub

    Sub ShowCases()

        CaseShape = New RectangleShape(New Vector2f(_TailleCases, _TailleCases))

        For I As Integer = 0 To _NbrCases
            For J As Integer = 0 To _NbrCases
                If _TabCases(I, J) = True Then
                    CaseShape.FillColor = Color.Black
                    CaseShape.Position = New Vector2f((I - 1) * _TailleCases, (J - 1) * _TailleCases)
                    _Window.Draw(CaseShape)
                End If
            Next
        Next

    End Sub

    Function CalculerNbrCase()
        Return _Window.Size.X / _TailleCases
    End Function

    Function CalculerNbrVoisins(ByVal X As Integer, ByVal Y As Integer)

        Dim NbrVoisins As Integer = 0

        If (X + 1 < _NbrCases) Then

            If (_TabCases(X + 1, Y)) Then
                NbrVoisins += 1
            End If

            If ((Y + 1) < _NbrCases AndAlso _TabCases(X + 1, Y + 1)) Then
                NbrVoisins += 1
            End If

            If ((Y - 1) > 0 AndAlso _TabCases(X + 1, Y - 1)) Then
                NbrVoisins += 1
            End If

        End If

        If (X - 1 > 0) Then

            If (_TabCases(X - 1, Y)) Then
                NbrVoisins += 1
            End If

            If ((Y + 1) < _NbrCases AndAlso _TabCases(X - 1, Y + 1)) Then
                NbrVoisins += 1
            End If

            If ((Y - 1) > 0 AndAlso _TabCases(X - 1, Y - 1)) Then
                NbrVoisins += 1
            End If

        End If


        If ((Y + 1) < _NbrCases AndAlso _TabCases(X, Y + 1)) Then
            NbrVoisins += 1
        End If

        If ((Y - 1) > 0 AndAlso _TabCases(X, Y - 1)) Then
            NbrVoisins += 1
        End If

        Return NbrVoisins

    End Function

    Sub NextTurn()

        _NbrTours += 1

        Console.WriteLine("[Debug] Tour n°" & _NbrTours)

        Dim NbrVoisins As Integer = 0
        Dim TabTemp(_NbrCases, _NbrCases) As Boolean

        For I As Integer = 0 To _NbrCases
            For J As Integer = 0 To _NbrCases

                NbrVoisins = CalculerNbrVoisins(I, J)

                If (_TabCases(I, J) = False And NbrVoisins = 3) Then
                    TabTemp(I, J) = True
                ElseIf (_TabCases(I, J) = True And (NbrVoisins = 2 Or NbrVoisins = 3)) Then
                    TabTemp(I, J) = True
                Else
                    TabTemp(I, J) = False
                End If

            Next
        Next

        _TabCases = TabTemp

        GC.Collect()

    End Sub
    Sub App_KeyReleased(ByVal sender As Object, ByVal e As KeyEventArgs) Handles _Window.KeyReleased

        If (e.Code = Keyboard.Key.Space) Then
            _SimulationDemaree = Not _SimulationDemaree
            Console.WriteLine("[Debug] Statut de la simulation : " & _SimulationDemaree)

            If _SimulationDemaree Then
                _Window.SetFramerateLimit(2)
            Else
                _Window.SetFramerateLimit(30)
            End If

        ElseIf (e.Code = Keyboard.Key.Enter And Not _SimulationDemaree) Then
            NextTurn()
        End If

    End Sub

    Sub App_MouseButtonReleased(ByVal sender As Object, ByVal e As MouseButtonEventArgs) Handles _Window.MouseButtonReleased

        If (e.Button = Mouse.Button.Left And Not _SimulationDemaree) Then

            Console.WriteLine(Mouse.GetPosition(_Window))

            Dim MousePosition As Vector2i = Mouse.GetPosition(_Window)

            If ((MousePosition.X < _Window.Size.X And MousePosition.X > 0) And (MousePosition.Y < _Window.Size.Y And MousePosition.Y > 0)) Then
                Dim Coordonnee As New Vector2i(Math.Ceiling(MousePosition.X / _TailleCases), Math.Ceiling(MousePosition.Y / _TailleCases))
                _TabCases(Coordonnee.X, Coordonnee.Y) = Not _TabCases(Coordonnee.X, Coordonnee.Y)
            End If

        End If

    End Sub

    Sub App_WindowResize() Handles _Window.Resized
        Console.WriteLine("[Debug] Window resized : X = " & _Window.Size.X & "/ Y = " & _Window.Size.Y)
        _NbrCases = CalculerNbrCase()
        ReDim _TabCases(_NbrCases, _NbrCases)
        _Window.SetView(New View(New Vector2f(_Window.Size.X / 2, _Window.Size.Y / 2), New Vector2f(_Window.Size.X, _Window.Size.Y)))
    End Sub

End Class
