Imports SFML.Window
Imports SFML.Graphics

Module Main

    Private WithEvents Window As New RenderWindow(New SFML.Window.VideoMode(600, 600), "Game Of Life")
    Private Scene As Scene

    Sub Main()

        Console.Title = "Game Of Life - Debug"

        Window.SetFramerateLimit(30)
        Window.SetKeyRepeatEnabled(False)
        Window.SetTitle("Game Of Life")

        Scene = New Menu(Window)
        Console.WriteLine("[Debug] Starting menu")

        While (Window.IsOpen)
            Scene.ProcessEvent()
            Scene.Update()
            Scene.Affichage()
            Window.Display()
            Window.Clear(Color.White)
        End While

        Console.ReadLine()

    End Sub

    Sub App_Closed(ByVal sender As Object, ByVal e As EventArgs) Handles Window.Closed
        Environment.Exit(0)
    End Sub

    Sub ChangerScene(ByVal NewScene As Scene)
        Scene = NewScene
    End Sub

End Module
