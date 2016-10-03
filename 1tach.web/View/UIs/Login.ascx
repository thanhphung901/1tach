<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="OneTach.UIs.Login" %>
<div class="content">
    <div class="container">
        <!-- InstanceBeginEditable name="content" -->
        <div class="row">
            <div class="col l8 s12">
                <div class="colleft">
                    <p style="font-size: 46px; color: #F27B7B; font-weight: 300; padding-bottom: 21px;">
                        Sign in and do more.
                    </p>
                    <p style="margin-top: 24px; font-size: 24px; font-weight: 300;">
                        Signing into Debate.org will give you access to a variety of features that are for members only.
                    </p>
                    <p style="margin-top: 24px; font-size: 24px; font-weight: 300;">
                        Here’s a preview of what you can do simply by logging in:
                    </p>
                    <p style="font-size: 20px; margin-top: 24px;">Participate in and Vote on Debates</p>
                    <p>Members can start one-on-one debates to challenge each other on specific topics. Debates last anywhere from one to five rounds, and the side with the most votes from the community wins the debate.</p>
                    <p style="font-size: 20px; margin-top: 24px;">Build Your Profile</p>
                    <p>Complete your profile to share what makes you tick. Showcase your activity statistics, including debates won, opinions posted, forum posts, and more.</p>
                    <p style="font-size: 20px; margin-top: 24px;">And Much More…</p>
                    <p>Receive notifications when members reply to your opinions, post in the forums, subscribe to your favorite content, and connect with friends who both share and challenge your belief systems.</p>
                </div>
            </div>
            <!-- end col9 -->
            <div class="col l4 s12">
                <div class="colright">
                    <%--<p class="btn-shadow blue-button"><a href="">SIGN IN USING FACEBOOK</a> </p>
                    <p class="btn-shadow lt-lt-blue-button"><a href="">SIGN IN USING FACEBOOK</a> </p>
                    <p class="btn-shadow lt-lt-red-button"><a href="">SIGN IN USING FACEBOOK</a> </p>
                    <p class="hdr-or">OR</p>--%>
                    <div class="frmlogin">
                        <p class="btn-shadow green-button"><a href="">SIGN IN</a> </p>
                        <div class="frmlogin-ct">
                            <p class="lbl">Username or Email</p>
                            <p>
                                <asp:TextBox CssClass="txt-sm-log" runat="server" ID='txtEmail'></asp:TextBox>
                            </p>
                            <p class="lbl">Password</p>
                            <p>
                                <asp:TextBox TextMode="Password" CssClass="txt-sm-log" runat="server" ID="txtPass"></asp:TextBox>
                            </p>
                            <p>
                                <input type="checkbox" id="remember" class="filled-in">
                                <label for="remember"><small>Keep me logged in.</small></label>
                            </p>
                            <p class="clearfix p-btn-log">
                                <span class="btnsm">
                                    <asp:LinkButton class="btn-sb-sm green-button" runat="server" ID="btnSignIn" OnClick="btnSignIn_OnClick">SIGN IN</asp:LinkButton>
                                </span>
                                <span class="right"><a href="#">Forgot your password?</a></span>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end col 3  -->

        </div>
        <!-- InstanceEndEditable -->
    </div>
</div>

