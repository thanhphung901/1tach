<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="vpro.eshop.cpanel.page.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <!-- Bootstrap core CSS -->
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="../Styles/signin.css" rel="stylesheet" type="text/css" />
    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Thông tin đăng nhập</h3>
                    </div>
                    <div class="panel-body">
                            <fieldset>
                                <div class="form-group">
                                    <input type="text" class="form-control" id="txtUN" placeholder="Tên đăng nhập" runat="server">
                                </div>
                                <div class="form-group">
                                    <input id="txtPW" runat="server" class="form-control" placeholder="Mật khẩu" name="password" type="password">
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <input id="chkRemember" runat="server" name="remember" type="checkbox">Nhớ tên đăng nhập
                                    </label>
                                </div>                 
                                <div class="form-group">
                                    <label><asp:Literal ID="lblError" runat="server" Text=""></asp:Literal></label>
                                </div>        
                                <asp:LinkButton ID="lbkLogin" runat="server" 
                                    CssClass="btn btn-lg btn-success btn-block" Text="Đăng nhập" 
                                    onclick="lbtLogin_Click">
                                </asp:LinkButton>
                            </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /container -->
    <footer class="footer">
        <div class="container">
             
        </div>
    </footer>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    </form>
</body>
</html>
