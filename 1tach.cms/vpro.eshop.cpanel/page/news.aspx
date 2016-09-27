﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="news.aspx.cs" Inherits="vpro.eshop.cpanel.page.news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Jquery/JqueryCollapse/jquery.collapse.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function ParseText(objsent) {
            ParseUrl(objsent, document.getElementById('MainContent_txtSeoUrl'));
            document.getElementById('MainContent_txtSeoTitle').value = objsent.value;
            document.getElementById('MainContent_txtSeoKeyword').value = objsent.value;
        }
        function ParseDesc(objsent) {
            document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
        }
        function ParseTextEn(objsent) {
            ParseUrl(objsent, document.getElementById('MainContent_txtSeoUrlEn'));
            document.getElementById('MainContent_txtSeoTitleEn').value = objsent.value;
            document.getElementById('MainContent_txtSeoKeywordEn').value = objsent.value;
        }
        function ParseDescEn(objsent) {
            document.getElementById('MainContent_txtSeoDescriptionEn').value = objsent.value;
        }
    </script>
  
    <div class="row page-header">
        <div class="col-sm-5">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"
                ValidationGroup="G1"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
            <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click" class="btn btn-default btn-sm"
                ValidationGroup="G1">
				<span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;Lưu &
            Thêm mới
            </asp:LinkButton>
            <asp:LinkButton ID="LbsaveClose" runat="server" class="btn btn-default btn-sm" OnClick="LbsaveClose_Click"
                ValidationGroup="G1">
				<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Lưu & Đóng
            </asp:LinkButton>
            <asp:HyperLink ID="Hyperback" runat="server" class="btn btn-default btn-sm"> 
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Đóng</asp:HyperLink>
        </div>
        <div class="col-sm-7 navbar-right">
            <div id="trNewsFunction" runat="server">
                <ul class="nav nav-pills">
                    <li><a href="#" id="hplCatNews" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Chuyên mục </a></li>
                    <li><a href="#" id="hplEditorHTMl" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Soạn tin </a></li>
                    <li><a href="#" id="hplNewsAtt" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;File đính kèm </a></li>
                    <li><a href="#" id="hplAlbum" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Album hình </a></li>
                    <li><a href="#" id="hplComment" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Phản hồi</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Thông tin chi tiết</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group" id="trCat" runat="server">
                        <label class="col-sm-2 control-label">
                            Chuyên mục</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Tiêu đề</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtTitle" id="txtTitle" runat="server" class="form-control"
                                onkeyup="ParseText(this);" onblur="ParseText(this);" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên tiêu đề"
                                Text="Vui lòng nhập tiêu đề" ControlToValidate="txtTitle" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblError" runat="server" CssClass="label label-danger"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mô tả ngắn</label>
                        <div class="col-sm-10">
                            <textarea id="txtDesc" runat="server" class="form-control" onkeyup="ParseDesc(this);"
                                onblur="ParseDesc(this);"></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Link Youtube</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtVideo" id="txtVideo" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div id="div_productinfo" runat="server">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Mã sản phẩm</label>
                            <div class="col-sm-10">
                                <input type="text" name="txtcode" id="txtcode" runat="server" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <div id="hangsx" runat="server" style="display:none">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Hãng Sản Xuất</label>
                            <div class="col-sm-10">
                              <asp:DropDownList ID="Drhangsx" runat="server" Width="500px">
                        </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                     <div id="tinhtrang" runat="server" style="display: none;">
                      <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Tình trạng</label>
                        <div class="col-sm-10">
                            <asp:RadioButtonList ID="Rdstatus" runat="server" RepeatColumns="3">
                                <asp:ListItem Value="0" Selected="True">Thường</asp:ListItem>
                                <asp:ListItem Value="1">Mới</asp:ListItem>
                                <asp:ListItem Value="2">Khuyến mãi</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Liên kết</label>
                        <div class="col-sm-7">
                            <input type="text" name="txtUrl" id="txtUrl" runat="server" class="form-control" />
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlTarget" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary" id="div_price" style="display: none;" runat="server">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin giá</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Giá</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtPrice" id="txtPrice" runat="server" onkeyup="this.value=formatNumeric(this.value);"
                                onblur="this.value=formatNumeric(this.value);" maxlength="20" value="0" class="form-control" />
                        </div>
                    </div>
<%--                     <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Giá khuyến mãi</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtPrice" id="Txtprice_promos" runat="server" onkeyup="this.value=formatNumeric(this.value);"
                                onblur="this.value=formatNumeric(this.value);" maxlength="20" value="0" class="form-control" />
                        </div>
                    </div>--%>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Thông tin hình ảnh</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Ảnh đại diện</label>
                        <div class="col-sm-10">
                            <div id="trUploadImage3" runat="server">
                                <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                                </span>&nbsp;Chọn ảnh
                                    <input id="fileImage3" type="file" name="fileImage3" size="50" runat="server" /></span>
                            </div>
                            <div id="trImage3" runat="server">
                                <div class="col-sm-3">
                                    <asp:ImageButton ID="btnDelete3" runat="server" ImageUrl="../images/delete_icon.gif"
                                        BorderWidth="0" Width="13px" ToolTip="Xóa hình chi tiết này" OnClick="btnDelete3_Click">
                                    </asp:ImageButton>
                                </div>
                                <div class="col-sm-9 thumbnail">
                                    <asp:HyperLink runat="server" ID="hplImage3" Target="_blank"></asp:HyperLink><br />
                                    <img id="Image3" runat="server" alt="" class="img-rounded" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="form-group">
                        <label class="col-sm-2 control-label">
                            Ảnh lớn</label>
                        <div class="col-sm-10">
                            <div id="trUploadImage2" runat="server">
                                <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                                </span>&nbsp;Chọn ảnh
                                    <input id="fileImage2" type="file" name="fileImage2" size="50" runat="server" /></span>
                            </div>
                            <div id="trImage2" runat="server">
                                <div class="col-sm-3">
                                    <asp:ImageButton ID="btnDelete2" runat="server" ImageUrl="../images/delete_icon.gif"
                                        BorderWidth="0" Width="13px" ToolTip="Xóa hình chi tiết này" OnClick="btnDelete2_Click">
                                    </asp:ImageButton>
                                </div>
                                <div class="col-sm-9 thumbnail">
                                    <asp:HyperLink runat="server" ID="hplImage2" Target="_blank"></asp:HyperLink><br />
                                    <img id="Image2" runat="server" alt="" class="img-rounded" />
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H3">
                        Thứ tự</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Thứ tự</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtOrder" id="txtOrder" runat="server" onkeyup="this.value=formatNumeric(this.value);"
                                onblur="this.value=formatNumeric(this.value);" maxlength="4" value="1" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Thứ tự trang chủ</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtOrderPeriod" id="txtOrderPeriod" runat="server" onkeyup="this.value=formatNumeric(this.value);"
                                onblur="this.value=formatNumeric(this.value);" maxlength="4" value="1" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4" id="accordion" role="tablist" aria-multiselectable="true">
            <div class="panel panel-primary">
                <div class="panel-heading" role="tab" id="headingOne">
                    <h3 class="panel-title" id="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true"
                            aria-controls="collapseOne">Thông tin hiển thị <span class="glyphicon glyphicon-download navbar-right"
                                aria-hidden="true" style="margin: 0"></span></a>
                    </h3>
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>
                                Hiển thị</label>
                            <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label>
                                Hiển thị trang chủ</label>
                            <asp:RadioButtonList ID="rblNewsPeriod" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="1" Text="Sản phẩm"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Tin tức"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Testimonials"></asp:ListItem>
                                <asp:ListItem Text="Không" Value="20" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label>
                                Hiển thị trong chi tiết</label>
                            <asp:RadioButtonList ID="rblShowDetail" runat="server" RepeatColumns="2">
                                <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label>
                                Cho phép phản hồi</label>
                            <asp:RadioButtonList ID="rblFeefback" runat="server" RepeatColumns="5">
                                <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Có" Selected="True" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                          <div class="form-group">
                            <label>
                                Ngôn ngữ</label>
                            <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="5">
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading" role="tab" id="headingTwo">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"
                            aria-expanded="false" aria-controls="collapseTwo">SEO PARAMETER <span class="glyphicon glyphicon-download navbar-right"
                                aria-hidden="true" style="margin: 0"></span></a>
                    </h4>
                </div>
                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>
                                Seo title</label>
                            <input type="text" name="txtSeoTitle" id="txtSeoTitle" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập seo title"
                                Text="Vui lòng nhập Seo Title" ControlToValidate="txtSeoTitle" CssClass="form-control"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label>
                                Seo url</label>
                            <input type="text" name="txtSeoUrl" id="txtSeoUrl" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập Seo Url"
                                Text="Vui lòng nhập Seo Url" ControlToValidate="txtSeoUrl" CssClass="form-control"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label>
                                Seo Keyword</label>
                            <textarea id="txtSeoKeyword" runat="server" class="form-control"></textarea>
                        </div>
                        <div class="form-group">
                            <label>
                                Seo Description</label>
                            <textarea id="txtSeoDescription" runat="server" class="form-control"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
