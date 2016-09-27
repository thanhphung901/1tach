<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Popup-Sendemail.ascx.cs"
    Inherits="vpro.eshop.cpanel.UIs.Popup_Sendemail" %>
<script src="../tinymce/js/tinymce/tinymce.min.js"></script>
<script src="../Scripts/TinymiceEditor.js" type="text/javascript"></script>
<div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">
                    Gửi mail thông điệp</h4>
            </div>
            <div class="modal-body">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <h3 class="panel-title" id="H1">
                                        Nội dung email</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <input type="text" name="txttitle" id="txttitle" runat="server" class="form-control"
                                            placeholder='Tiêu đề' />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập tiêu đề !"
                                            Text="Vui lòng nhập tiêu đề !" ControlToValidate="txttitle" CssClass="label label-danger"
                                            ValidationGroup="G1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Nội dung</label>
                                        <textarea id="mrk" cols="20" rows="15" class="form-control" runat="server"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"
                    ValidationGroup="G1"><span class="glyphicon glyphicon-envelope" aria-hidden="true"></span>&nbsp;Send</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
