<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListDebate.ascx.cs" Inherits="OneTach.UIs.ListDebate" %>

<div class="content">
    <div class="container">
        <h3 class="ttcate2"><span><asp:Literal ID="ltl_CatName" runat="server"></asp:Literal></span></h3>
        <div class="row row_post2">
            <asp:Repeater ID="Re_New" runat="server">
                <ItemTemplate>
                    <div class="col l3 m6 s12">
                        <div class="post_item post_doanh_nghiep">
                            <a href="<%#GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL"))%>" title="<%#Eval("NEWS_TITLE")%>">
                                <p class="img" style="background-image: url(<%#GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3"))%>)"></p>
                                <h2 class="tt-post"><%#Eval("NEWS_TITLE")%></h2>
                                <p class="info_post clearfix"><span class="date"><%#Eval("NEWS_PUBLISHDATE","{0:dd/MM/yyyy}")%></span> <span class="cmm_result">71% chọn YES(doc ra sau)</span> </p>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <p class="pagination clearfix" id="ipage" runat="server">
                <asp:Literal ID="ltrPage" runat="server"></asp:Literal>
            </p>
        </div>
    </div>
</div>

