<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="category.aspx.cs" Inherits="vpro.eshop.cpanel.page.category" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        input[type="radio"]
        {
            margin: 4px 4px 4px 4px;
        }
        .btn-file
        {
            position: relative;
            overflow: hidden;
        }
        .btn-file input[type=file]
        {
            position: absolute;
            top: 0;
            right: 0;
            min-width: 100%;
            min-height: 100%;
            font-size: 100px;
            text-align: right;
            filter: alpha(opacity=0);
            opacity: 0;
            outline: none;
            background: white;
            cursor: inherit;
            display: block;
        }
    </style>
    <script src="../Jquery/JqueryCollapse/jquery.collapse.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        new jQueryCollapse($("#seo"), {
            query: 'div h2'
        });

        function ParseText(objsent) {
            ParseUrl(objsent, document.getElementById('MainContent_txtSeoUrl'));
            document.getElementById('MainContent_txtSeoTitle').value = objsent.value;
            document.getElementById('MainContent_txtSeoKeyword').value = objsent.value;
            //document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
        }
        function ParseTextEn(objsent) {
            ParseUrl(objsent, document.getElementById('MainContent_txtSeoUrlEn'));
            document.getElementById('MainContent_txtSeoTitleEn').value = objsent.value;
            document.getElementById('MainContent_txtSeoKeywordEn').value = objsent.value;
            //document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
        }
        function ParseDesc(objsent) {
            document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
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
                    <li><a href="#" id="Hyperseo_cate" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Soạn tin chuyên mục </a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Thông tin chuyên mục</h3>
                    <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="form-group" id="trCat" runat="server">
                        <label class="col-sm-2 control-label">
                            Cấp cha</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mã</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtCode" id="txtCode" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Chuyên mục</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtName" id="txtName" runat="server" class="form-control"
                                onkeyup="ParseText(this);" onblur="ParseText(this);" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên chuyên mục"
                                Text="Vui lòng nhập tên nhóm" ControlToValidate="txtName" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mô tả</label>
                        <div class="col-sm-10">
                            <textarea id="txtDesc" runat="server" class="form-control" onkeyup="ParseDesc(this);"
                                onblur="ParseDesc(this);"></textarea>
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
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Số tin / 1 trang</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtpageItem" id="txtpageItem" runat="server" onblur="this.value=formatNumeric(this.value);"
                                maxlength="4" class="form-control" value="20" />
                        </div>
                    </div>
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
                            <div id="trUploadImage1" runat="server">
                                <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                                </span>&nbsp;Chọn ảnh<input id="fileImage1" type="file" name="fileImage1" size="50"
                                    runat="server" /></span>
                            </div>
                            <div id="trImage1" runat="server">
                                <div class="col-sm-3">
                                    <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/delete_icon.gif"
                                        BorderWidth="0" Width="13px" ToolTip="Xóa hình chi tiết này" OnClick="btnDelete1_Click">
                                    </asp:ImageButton>
                                </div>
                                <div class="col-sm-9 thumbnail">
                                    <asp:HyperLink runat="server" ID="hplImage1" Target="_blank"></asp:HyperLink><br />
                                    <img id="Image1" runat="server" alt="" class="img-rounded" />
                                </div>
                            </div>
                        </div>
                    </div>
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
                            <asp:RadioButtonList ID="rblCatPeriod" runat="server" RepeatColumns="3">
                                <asp:ListItem Selected="True" Text="Không" Value="0"></asp:ListItem>
                                <asp:ListItem Value="1">Có</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label>
                                Kiểu hiển thị</label>
                            <asp:RadioButtonList ID="rblCatType" runat="server">
                                <asp:ListItem Text="Tin tức" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1">Đánh giá</asp:ListItem>
                                <asp:ListItem Value="2">Tranh luận</asp:ListItem>
                                <asp:ListItem Text="Khác" Value="99"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label>
                                Vị trí</label>
                            <asp:RadioButtonList ID="rblPos" runat="server" RepeatColumns="2">
                                <asp:ListItem Value="1">Menu</asp:ListItem>
                                <asp:ListItem Value="20" Selected="True">khác</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label>
                                Hiển thị Footer</label>
                            <asp:RadioButtonList ID="rblShowFooter" runat="server" RepeatColumns="52">
                                <asp:ListItem Selected="True" Text="Không" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Có" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label>
                                Hiển thị tức thời</label>
                            <asp:RadioButtonList ID="rblShowItems" runat="server" RepeatColumns="5">
                                <asp:ListItem Selected="True" Text="Không" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Có" Value="1"></asp:ListItem>
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
