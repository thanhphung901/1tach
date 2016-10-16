<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="OneTach.UIs.Register" %>
<%@ Import Namespace="System.ServiceModel.Security.Tokens" %>
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
                        <p class="btn-shadow green-button"><a href="">CREATE NEW ACCOUNT</a> </p>
                        <div class="frmlogin-ct">
                            <p class="lbl">Username</p>
                            <p>
                                <asp:TextBox CssClass="txt-sm-log" runat="server" ID="txtUserName"></asp:TextBox>
                            </p>
                            <p class="lbl">Email</p>
                            <p>
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="txt-sm-log"></asp:TextBox>
                            </p>
                            <p class="lbl">Password</p>
                            <p>
                                <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" class="txt-sm-log"></asp:TextBox>
                            </p>
                            <p class="lbl">Confirm Password</p>
                            <p>
                                <asp:TextBox ID="txtRePassword" TextMode="Password" runat="server" class="txt-sm-log"></asp:TextBox>
                            </p>
                            <%--<p>
                                <asp:CheckBox runat="server" ID="ckRemember" class="filled-in" />
                                <label for="<%=ckRemember.ClientID %>"><small>Keep me logged in.</small></label>
                            </p>--%>
                            <p style="color: red">
                                <asp:Label runat="server" ID="lblError"></asp:Label>
                            </p>
                            <p class="clearfix p-btn-log">
                                <span class="btnsm">
                                    <asp:LinkButton class="btn-sb-sm green-button" OnClick="btnSignUp_OnClick" runat="server" ID="btnSignUp">SIGN UP</asp:LinkButton>
                                </span>
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
