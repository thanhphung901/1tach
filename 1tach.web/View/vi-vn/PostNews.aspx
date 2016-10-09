<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="PostNews.aspx.cs" Inherits="OneTach.vi_vn.PostNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .btnXoa {
            float: right;
            display: block;
            top: 0;
            border-radius: 100%;
            padding: 5px;
            border: 1px solid #ff0000;
            color: #ff0000;
            background: #fff;
            height: 30px;
            width: 30px;
            text-align: center;
        }

            .btnXoa:hover {
                border: 1px solid #ff0000;
                color: #fff;
                background: #ff0000;
                text-decoration: none;
            }
    </style>

    <div class="content">
        <div class="container">
            <!-- InstanceBeginEditable name="content" -->
            <div class="post" style="padding: 2em 0">
                <div class="row">
                    <div class="col s8 main-col">
                        <div class="row">
                            <div class="input-field çol s12">
                                <label>Chọn danh mục</label><br />
                                <br />
                                <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s12">
                                <input type="text" name="txtTitle" id="txtTitle" runat="server" class="form-control"
                                    onkeyup="ParseText(this);" onblur="ParseText(this);" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên tiêu đề"
                                    Text="Vui lòng nhập tiêu đề" ControlToValidate="txtTitle" CssClass="label label-danger"
                                    ValidationGroup="G1"></asp:RequiredFieldValidator>
                                <label for='<%=txtSeoTitle.ClientID %>'>Tiêu đề</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s12">
                                <textarea id="txtDesc" runat="server" class="validate materialize-textarea" onkeyup="ParseDesc(this);"
                                    onblur="ParseDesc(this);"></textarea>
                                <label for='<%=txtDesc.ClientID %>'>Mô tả ngắn</label>
                            </div>
                        </div>
                        <asp:ScriptManager runat="server" ID="scrpt1"></asp:ScriptManager>
                        <asp:UpdatePanel runat="server" ID="pnContentUpdate">
                            <ContentTemplate>
                                <%--<div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title" id="H2">Thông tin hình ảnh</h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Ảnh đại diện</label>
                                                <div class="col-sm-10">
                                                    <div id="trUploadImage3" runat="server">
                                                        <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true"></span>&nbsp;Chọn ảnh
                                    <input id="fileImage3" type="file" name="fileImage3" size="50" runat="server" visible="True" /></span>
                                                    </div>
                                                    <div id="trImage3" runat="server">
                                                        <div class="col-sm-3">
                                                            <asp:ImageButton ID="btnDelete3" runat="server" ImageUrl="../images/delete_icon.gif"
                                                                BorderWidth="0" Width="13px" ToolTip="Xóa hình chi tiết này" OnClick="btnDelete3_OnClick"></asp:ImageButton>
                                                        </div>
                                                        <div class="col-sm-9 thumbnail">
                                                            <asp:HyperLink runat="server" ID="hplImage3" Target="_blank"></asp:HyperLink><br />
                                                            <img id="Image3" runat="server" alt="" class="img-rounded" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Ảnh lớn</label>
                                                <div class="col-sm-10">
                                                    <div id="trUploadImage2" runat="server">
                                                        <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true"></span>&nbsp;Chọn ảnh
                                    <input id="fileImage2" type="file" name="fileImage2" size="50" runat="server" visible="True" /></span>
                                                    </div>
                                                    <div id="trImage2" runat="server">
                                                        <div class="col-sm-3">
                                                            <asp:ImageButton ID="btnDelete2" runat="server" ImageUrl="../images/delete_icon.gif"
                                                                BorderWidth="0" Width="13px" ToolTip="Xóa hình chi tiết này" OnClick="btnDelete2_OnClick"></asp:ImageButton>
                                                        </div>
                                                        <div class="col-sm-9 thumbnail">
                                                            <asp:HyperLink runat="server" ID="hplImage2" Target="_blank"></asp:HyperLink><br />
                                                            <img id="Image2" runat="server" alt="" class="img-rounded" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="row" id="pnNoiDung">
                                    <div class="col s12">
                                        <h2><b>NỘI DUNG CHÍNH</b></h2>
                                    </div>
                                    <div class="col s12">
                                        <asp:ListView runat="server" ID="lstContentNews" OnItemCommand="lstContentNews_OnItemCommand" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <LayoutTemplate>
                                                <ul class="col-md-12 col-lg-12 col-sm-12" style="list-style: none">
                                                    <li runat="server" id="itemPlaceholder"></li>
                                                </ul>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li style="border-bottom: 1px dashed #ccc; margin: 5px; padding: 20px 0px">
                                                    <asp:LinkButton runat="server" CssClass="btnXoa"  OnClientClick="scrollToBottom();"
                                                        ID="btnXoa" CommandName="XoaDong" CommandArgument='<%# Eval("Id") %>'> x </asp:LinkButton>
                                                    <asp:HiddenField runat="server" ID="hdType" Value='<%# Eval("Type") %>' />
                                                    <asp:Panel runat="server" CssClass="row" ID="pnBoxChuDe" Visible='<%#int.Parse(Eval("Type").ToString()) == 0 %>'>
                                                        <label>Chủ đề</label>
                                                        <asp:TextBox runat="server" ID="txtBoxChuDe" CssClass="form-control" Text='<%#Eval("Paragraph") %>'></asp:TextBox>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server" CssClass="row" ID="pnPara" Visible='<%#int.Parse(Eval("Type").ToString()) == 1 %>'>
                                                        <label>Tiêu đề</label>
                                                        <asp:TextBox runat="server" ID="txtTitlePara" CssClass="form-control" Text='<%# Eval("Title") %>'></asp:TextBox>
                                                        <label>Nội dung</label>
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" ID="txtNoiDung" CssClass="materialize-textarea" Text='<%#Eval("Paragraph") %>'></asp:TextBox>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server" CssClass="row" ID="pnImage" Visible='<%#int.Parse(Eval("Type").ToString()) == 2 %>'>
                                                        <label>Chọn ảnh</label>

                                                        <input id="avatarUpload" type="file" name="file" accept="image/gif, image/jpeg, image/png" onchange="previewFile(this)" runat="server" />
                                                        <%--<asp:FileUpload ID="avatarUpload" runat="server" />--%>
                                                        <img id="imgUpload" alt="" runat="server" class="displayImg" src='<%#Eval("Image") %>' style="width: 200px" />
                                                        <asp:TextBox ID="hdImage" Style="display: none" runat="server" CssClass="valueImg" Text='<%#Eval("Image") %>' />
                                                    </asp:Panel>
                                                    <asp:Panel runat="server" CssClass="row" ID="pnBox" Visible='<%#int.Parse(Eval("Type").ToString()) == 3 %>'>
                                                        <label>Nội dung trong hộp</label>
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Text='<%#Eval("Box") %>' Rows="3" ID="txtBox" CssClass="materialize-textarea"></asp:TextBox>
                                                    </asp:Panel>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <asp:ListView runat="server" ID="lstContentVote" OnItemCommand="lstContentVote_OnItemCommand" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <LayoutTemplate>
                                                <ul class="col-md-12 col-lg-12 col-sm-12">
                                                    <li runat="server" id="itemPlaceholder"></li>
                                                </ul>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li style="border-bottom: 1px dashed #ccc; margin: 5px; padding: 20px 0px">
                                                    <asp:LinkButton runat="server" CssClass="btnXoa"
                                                        ID="btnXoa" OnClientClick="scrollToBottom();" CommandName="XoaDong" CommandArgument='<%# Eval("ID") %>'> x </asp:LinkButton>

                                                    <asp:HiddenField runat="server" ID="hdID" Value='<%#Eval("ID") %>' />
                                                    <asp:HiddenField runat="server" ID="hdVoteCount" Value='<%#Eval("VotedCount") %>' />

                                                    <label>Chọn ảnh</label>
                                                    <input id="avatarUpload" type="file" name="file" accept="image/gif, image/jpeg, image/png" onchange="previewFile(this)" runat="server" />
                                                    <%--<asp:FileUpload ID="avatarUpload" runat="server" />--%>
                                                    <img id="imgUpload" alt="" runat="server" class="displayImg" src='<%#Eval("Image") %>' style="width: 200px" /><br />
                                                    <asp:TextBox ID="hdImage" Style="display: none" runat="server" CssClass="valueImg" Text='<%#Eval("Image") %>' />

                                                    <label>Nội dung</label>
                                                    <asp:TextBox runat="server" Text='<%#Eval("Content") %>' ID="txtContent" CssClass="form-control"></asp:TextBox>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="col s12">
                                        <asp:LinkButton ID="btnboxChuDe" OnClientClick="scrollToBottom();" CssClass="waves-effect waves-light btn" OnClick="btnboxChuDe_OnClick" runat="server">Box chủ đề</asp:LinkButton>
                                        <asp:LinkButton ID="btnThemDoanVan" OnClientClick="scrollToBottom();" CssClass="waves-effect waves-light btn" OnClick="btnThemDoanVan_OnClick" runat="server">Đoạn văn</asp:LinkButton>
                                        <asp:LinkButton ID="btnThemHinhAnh" OnClientClick="scrollToBottom();" CssClass="waves-effect waves-light btn" OnClick="btnThemHinhAnh_OnClick" runat="server">Hình ảnh</asp:LinkButton>
                                        <asp:LinkButton ID="btnBox" OnClientClick="scrollToBottom();" CssClass="waves-effect waves-light btn" OnClick="btnBox_OnClick" runat="server">Box nội dung</asp:LinkButton>
                                        <asp:LinkButton ID="btnThemLuaChon" Visible="False" OnClientClick="scrollToBottom();" CssClass="waves-effect waves-light btn" OnClick="btnThemLuaChon_OnClick" runat="server">Lựa chọn</asp:LinkButton>
                                    </div>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col s4">
                        <div class="sub-col row">
                            <div class="header">
                                Thông tin bài viết
                            </div>
                            <div>
                                <div class="col s12">
                                    <h3>Loại bài viết</h3>
                                    <asp:RadioButtonList ID="radType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radType_OnSelectedIndexChanged" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="Tin tức" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Bầu chọn" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Tranh luận" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="sub-col row">
                            <div class="header">
                                Thông tin hiển thị
                            </div>
                            <div>
                                <div class="col s12">
                                    <h3>Hiển thị</h3>
                                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                        <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col s12">
                                    <h3>Hiển thị trang chủ</h3>
                                    <asp:RadioButtonList ID="rblNewsPeriod" runat="server" RepeatDirection="Vertical">
                                        <asp:ListItem Value="1" Text="Tin tức"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Tin chuyên gia"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Tin doanh nghiệp"></asp:ListItem>
                                        <asp:ListItem Text="Không" Value="20" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col s12">
                                    <h3>Hiển thị trong chi tiết</h3>
                                    <asp:RadioButtonList ID="rblShowDetail" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                        <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col s12">
                                    <h3>Cho phép phản hồi</h3>
                                    <asp:RadioButtonList ID="rblFeefback" runat="server" RepeatColumns="5">
                                        <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Có" Selected="True" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col s12" style="display: none">
                                    <h3>Ngôn ngữ</h3>
                                    <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="5">
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="sub-col row" style="display: none">
                            <div class="header">
                                Seo parameter
                            </div>
                            <div class="">
                                <div class="col s12">
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
                </div>
            </div>
            <style>
                select {
                    display: block;
                    margin: 3px;
                    width: 95%;
                }

                label {
                    padding-left: 10px;
                }

                .post .h3, .post h3 {
                    font-size: 1.5em;
                }

                .post .h2, .post h2 {
                    font-size: 1.5em;
                }

                .post h3 i, .post .h3 i {
                    font-size: 1em;
                    position: relative;
                    top: 5px;
                }

                .post .mg0 {
                    margin-bottom: 0;
                }

                .coltrol-btn {
                    margin-top: 1em;
                }

                .frm_input {
                    margin-bottom: 2em;
                    background: #F3F3F3;
                    padding: 1em;
                    margin-top: 1em;
                }

                .coltrol-right i {
                    background: #f00;
                    color: #fff;
                    padding: 5px;
                    border-radius: 5px;
                    border: 1px solid #ccc;
                }

                .header {
                    background-color: #404D56;
                    color: #fff;
                    font-size: 12pt;
                    padding: 5px 0px;
                    text-align: center;
                }

                .sub-col {
                    border: 1px solid #404D56;
                }

                    .sub-col h3 {
                        font-size: 16px;
                        padding-top: 10px;
                        font-weight: bold;
                    }

                input[type="radio"]:not(:checked), input[type="radio"]:checked {
                    opacity: 1;
                    position: inherit;
                }

                [type="radio"]:not(:checked) + label, [type="radio"]:checked + label {
                    padding-left: 4px;
                }
            </style>
            <!-- InstanceEndEditable -->
        </div>
    </div>

    <script type="text/javascript">
        function ParseText(objsent) {
            ParseUrl(objsent, document.getElementById('<%=txtSeoUrl.ClientID%>'));
            document.getElementById('<%=txtSeoTitle.ClientID%>').value = objsent.value;
            document.getElementById('<%=txtSeoKeyword.ClientID%>').value = objsent.value;
        }
        function ParseDesc(objsent) {
            document.getElementById('<%=txtSeoDescription.ClientID%>').value = objsent.value;
        }
        function ParseTextEn(objsent) {
            ParseUrl(objsent, document.getElementById('MainContent_txtSeoUrlEn'));
            document.getElementById('MainContent_txtSeoTitleEn').value = objsent.value;
            document.getElementById('MainContent_txtSeoKeywordEn').value = objsent.value;
        }
        function ParseDescEn(objsent) {
            document.getElementById('MainContent_txtSeoDescriptionEn').value = objsent.value;
        }
        function ParseUrl(objsent, objreceive) {
            objreceive.value = RemoveUnicode(objsent);
        }
        function RemoveUnicode(obj) {
            var str;
            if (eval(obj))
                str = eval(obj).value;
            else
                str = obj;
            str = str.toLowerCase();
            str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
            str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
            str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
            str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
            str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
            str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
            str = str.replace(/đ/g, "d");
            /*thay the 1 so ky tu dat biet*/
            str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|-+|–|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\“|\”|\&|\#|\[|\]|~|$|_/g, " ");
            /**/
            //str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
            str = str.replace(/\s+/g, "-");
            /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */
            str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-  
            str = str.replace(/^\-+|\-+$/g, "");
            //cắt bỏ ký tự - ở đầu và cuối chuỗi 
            //eval(obj).value = str.toUpperCase();
            return str;
        }

        function previewFile(input) {
            debugger;
            var preview = $(input).parent().find('.displayImg')[0];
            var value = $(input).parent().find('.valueImg')[0];

            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $(preview)
                        .attr('Src', e.target.result)
                        .width(200);
                    $(value).val(e.target.result);
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

        $(document)
            .ready(function () {
                scrollToBottom();
            });

        function scrollToBottom() {
            var t = $('#pnNoiDung');
            $('html,body').animate({
                scrollTop: $(t).scrollHeight
            },
            'slow');
            $(t).scrollTop($(t).scrollHeight);
        }
    </script>
</asp:Content>
