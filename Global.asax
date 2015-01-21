<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Код, выполняемый при запуске приложения
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Код, выполняемый при завершении работы приложения
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Код, выполняемый при возникновении необрабатываемой ошибки
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Код, выполняемый при запуске нового сеанса
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Код, выполняемый при запуске приложения. 
        ' Примечание: Событие Session_End вызывается только в том случае, если для режима sessionstate
        ' задано значение InProc в файле Web.config. Если для режима сеанса задано значение StateServer 
        ' или SQLServer, событие не порождается.
    End Sub
       
</script>