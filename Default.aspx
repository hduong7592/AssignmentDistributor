<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
        //***********************************
        //Show Waiting to Load Popup Function
        //***********************************

        function ShowCover() {
            $("#page-cover").css({ "display": "block", opacity: 0.7, "width": $(document).width(), "height": $(document).height() });
            $("#page-cover").show();   
        }

        function HideCover() {
            $("#page-cover").hide();   
        }
        function WaitToLoad() {
            $("#DivWaitingForm").show();                            
        }

        //***********************************
        //Hide Waiting to Load Popup Function
        //***********************************

        function HideWaitToLoad() {
            $("#DivWaitingForm").hide();        
        }

        //********************************
        //Show Popup with Content Function
        //********************************

        function ShowContent() {

            //center lightbox
            var w = $(window).width();
            var h = $(window).height();

            var lw = $("#DivForm").width();
            var lh = $("#DivForm").height();

            $("#DivForm").css({ 'margin-top': ((-lh / 2) - 50) + 'px', 'margin-left': (-lw / 2) + 'px' });           
            $("#DivForm").fadeIn('fast');
        }  

        function Login() {
           // alert("Login");          
            var t = new Date().getTime();
            ShowCover();
            WaitToLoad();
            $("#result").load("Forms/Login.aspx #LoginDiv",
                {                  
                    time: t
                },
                function (xhr, textStatus, req) {
                    if (textStatus == "error") {
                        alert("Error");                       
                    }
                    else {
                        ShowContent();
                        HideWaitToLoad();
                    }
                }
            );
        }

        function Close() {
            $("#DivForm").hide();
            $("#page-cover").hide();   
        }

        function Register(WhichUser) {
            var t = new Date().getTime();
            ShowCover();
            WaitToLoad();
            $("#result").load("Forms/Register.aspx #RegisterDiv",
                {
                    WhichUser: WhichUser,
                    time: t
                },
                function (xhr, textStatus, req) {
                    if (textStatus == "error") {
                        alert("Error");
                    }
                    else {
                        ShowContent();
                        HideWaitToLoad();
                    }
                }
            );
        }
    </script>
    <table align="center" cellpadding="2" style="height: 800px; width: 800px;">
        <tr>
            <td style="text-align: center; vertical-align: middle; height: 800px;">
                <input id="LoginBtn" style="width: 100px; height: 50px" type="button" value="Login" runat="server" />
                <input id="TeacherRegisterBtn" style="width: 150px; height: 50px" type="button" value="Teacher Register" runat="server" />
                <input id="StudentRegisterBtn" style="width: 150px; height: 50px" type="button" value="Student Register" runat="server" />
            </td>
        </tr>
    </table>
    
</asp:Content>

