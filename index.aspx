<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TaskTracker.index" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Task Tracker</title>
    <link href="styles.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/fontawesome.min.css" rel="stylesheet" />
</head>
<script>
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }
</script>
<body>
    <form id="form1" runat="server">
        <main class="container w-50">
            <div class="d-flex align-items-center  overflow-auto">
                <h1 class="fw-bold float-start">Task Tracker</h1>
                <asp:Button runat="server" ID="btnClose" Text="Close" class=" text-uppercase fw-bold btn btn-danger flex-end" />
            </div>
            <div>

                <hr />
                <span><asp:Label runat="server" ID="lblMessages"></asp:Label></span>
                <span>Task</span><br />
                <asp:TextBox runat="server" class="textInput" ID="tbTaskName" />
                <br />
                <br />
                <span>Day and Time</span><br />
                <asp:TextBox runat="server" class="textInput" TextMode="DateTime" ID="tbTimeInput" />
                <br />
                <br />

                <div class="d-flex align-items-center">
                    <label>Set Reminder</label>
                    <asp:CheckBox class="px-3" runat="server" ID="chkSetReminder" />
                </div>
                <asp:Button class="btn btn-dark align-content-center fw-bold m-auto w-100 mt-2" runat="server" Text="Save Task" ID="btnSaveTask" OnClick="btnSaveTask_Click" />
            </div>
            <div id="tasks">
                <%if (values.count <= 0)
                    { %>
                        <div class="text-center p-3 rounded text-black" style="background-color:#eee">
                            <asp:Label runat="server" ID="taskMessage">No tasks found, please add some first to view them.</asp:Label>
                        </div>
                  <%}
                      else
                          foreach (var task in tasks)
                          {%>
                            <div class="taskSingle my-2 p-0 d-flex">
                            <%if (!task.SetReminder)
                                {%>
                                <div class="rounded" id="selection" style="background-color: transparent !important;"></div>
                            <%}
                                else
                                { %>
                                <div class="rounded" id="selection" style="background-color: green;"></div>
                            <%} %>

                            <div id="details" class="d-flex col bg-info px-1 rounded-1">
                                <div class="flex-fill">
                                   <span class="fw-bold"><%=task.TaskName%></span><br />
                                   <span style="font-size: 0.8rem" ><%=task.TaskDate.ToString("ddd-MM-yyyy HH:mm")%></span>
                                </div>
                                <div class="flex-end float-end">
                                    <asp:Button runat="server" class="px-2 text-danger border-0 fw-bold" id="iDeleteTask" Text="x" onclick="iDeleteTask_Click" />
                                </div>
                            </div>
                        </div>
                 <%} %>
            </div>
        </main>
    </form>
</body>
</html>
