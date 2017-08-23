Imports System.IO
'Imports IBRioRestService.RestKlasi

Public Class UnvFunkcii

   
    Public Shared Function zapisiGreskaVoFileLog(ByVal text As String)
        Dim patekaFileLog As String = AppDomain.CurrentDomain.BaseDirectory + "Logs" + Path.DirectorySeparatorChar + "ErrorLog.txt"

        If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs") Then
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Logs")
        End If

        If Not System.IO.File.Exists(patekaFileLog) = True Then
            Dim file As System.IO.FileStream
            file = System.IO.File.Create(patekaFileLog)

            file.Close()
        End If

        Try
            'My.Computer.FileSystem.WriteAllText(patekaFileLog, text, True)
            'My.Computer.FileSystem.Write(patekaFileLog, text, True)
            Dim infoReader As System.IO.FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(patekaFileLog)

            Dim difference As TimeSpan = Date.Now.Subtract(infoReader.LastWriteTime)
            ''logot ke stoi edna nedela
            If difference.TotalDays > 7 Then
                My.Computer.FileSystem.WriteAllText(patekaFileLog, "", True)
            End If

            Using sw As StreamWriter = File.AppendText(patekaFileLog)
                sw.WriteLine(Date.Now + " : " + text)
            End Using
        Catch ex As Exception

        End Try


    End Function
    Public Shared Function zapisiInfoVoFileLog(ByVal text As String)
        Dim patekaFileLog As String = AppDomain.CurrentDomain.BaseDirectory + "Logs" + Path.DirectorySeparatorChar + "InfoLog.txt"

        Dim enableInfoLog As Boolean = Konv.ObjVoBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("enableInfoLog"))

        If Not enableInfoLog Then
            Exit Function

        End If
        If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs") Then
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Logs")
        End If

        If Not System.IO.File.Exists(patekaFileLog) = True Then
            Dim file As System.IO.FileStream
            file = System.IO.File.Create(patekaFileLog)

            file.Close()
        End If

        Try
            'My.Computer.FileSystem.WriteAllText(patekaFileLog, text, True)
            'My.Computer.FileSystem.Write(patekaFileLog, text, True)
            Dim infoReader As System.IO.FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(patekaFileLog)

            Dim difference As TimeSpan = Date.Now.Subtract(infoReader.LastWriteTime)
            ''logot ke stoi edna nedela
            If difference.TotalDays > 7 Then
                My.Computer.FileSystem.WriteAllText(patekaFileLog, "", True)
            End If

            Using sw As StreamWriter = File.AppendText(patekaFileLog)
                sw.WriteLine(Date.Now + " : " + text)
            End Using
        Catch ex As Exception

        End Try


    End Function
   
   
End Class
