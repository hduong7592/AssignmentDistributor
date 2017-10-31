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

        function ValidateLogIn(SessionID) {
            var Username = $("#UsernameTxt").val();
            var Password = $("#PasswordTxt").val();
            if (Username == "") {
                alert("Username is missing!");
                $("#UsernameTxt").focus();
                return false;
            }
            else if (Password == "") {
                alert("Password is missing!");
                $("#PasswordTxt").focus();
                return false;
            }
            else {
                LogUserIn(Username, Password, SessionID);
            }

        }

        function LogUserIn(Username, Password, SessionID) {
            var t = new Date().getTime();

            var obj = '{"Username": "' + Username +
                '","Password": "' + Password +
                '","SessionID": "' + SessionID +
                '","time": "' + t +
                '" }';
            //alert(obj);
            $.ajax({
                type: "POST",
                url: "WebService.asmx/WebLogIn",
                data: obj,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var data = JSON.parse(response.d);
                    alert(data.Status);
                    if (data.length > 0) {
                        
                    }
                   
                    /*
                    //alert(data);
                    if (data == "Error") {
                        alert("Error");
                        return false;
                    }
                    else if (data == "Not Exist") {
                        alert("User not exist");
                        return false;
                    }
                    else if (data == "Password Incorrect") {
                        alert("Password is incorrect!");
                        return false;
                    }
                    else {
                        LoggedIn(data);
                        HideWaitToLoad();
                        HideCover();
                        Close();
                    }
                    */
                },
                error: function (response) {
                    //alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                    alert("Error "+ response.d);
                    ShowError();
                }
            });   
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

        function ValidateRegisterForm(SessionID) {
            //alert(SessionID);
            var Code = $("#CodeTxt").val();
            var FirstName = $("#FirstNameTxt").val();
            var LastName = $("#LastNameTxt").val();
            var Email = $("#EmailTxt").val();
            var Username = $("#UsernameTxt").val();
            var Password = $("#PasswordTxt").val();
            var RePassword = $("#RePasswordTxt").val();

            if (Code == "") {
                alert("Code is required!");
                $("#CodeTxt").focus();
                return false;
            }
            else if (FirstName == "") {
                alert("First Name is required!");
                $("#FirstNameTxt").focus();
                return false();
            }
            else if (LastName == "") {
                alert("Last Name is required!");
                $("#LastNameTxt").focus();
                return false();
            }
            else if (Email  == "") {
                alert("Email  is required!");
                $("#EmailTxt").focus();
                return false();
            }
            else if (Username == "") {
                alert("Username is required!");
                $("#UsernameTxt").focus();
                return false();
            }
            else if (Password == "") {
                alert("Password is required!");
                $("#PasswordTxt").focus();
                return false();
            }
            else if (RePassword == "") {
                alert("Confirm password is required!");
                $("#RePasswordTxt").focus();
                return false();
            }
            else if (Password != RePassword) {
                alert("Password does not match!");
                return false;
            }
            else {
                var t = new Date().getTime();

                var obj = '{"Code": "' + Code +
                            '","FirstName": "' + FirstName +
                            '","LastName": "' + LastName +
                            '","Email": "' + Email +
                            '","Username": "' + Username +
                            '","Password": "' + Password +                  
                            '","time": "' + t +
                            '" }';

                $.ajax({
                    type: "POST",
                    url: "WebService.asmx/Register",
                    data: obj,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                       
                        var data = JSON.parse(response.d);
                        alert(data.Status);                      

                        //alert(data);
                        /*
                        if (data == "Error") {
                            alert("Error");
                        }
                        else if (data == "Exist") {
                            alert("Username already exist!");
                        }
                        else {
                            LogUserIn(Username, Password, SessionID);
                            HideWaitToLoad();
                            HideCover();
                            Close();
                        }
                        */
                    },
                    error: function (response) {
                        //alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                        //alert(response);
                        ShowError();
                    }
                });     
            }
        }

        function LoggedIn(SessionID) {
            alert("Logged in: "+SessionID);
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

