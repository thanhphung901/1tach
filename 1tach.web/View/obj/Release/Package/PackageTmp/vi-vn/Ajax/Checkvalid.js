function checkval() {
    alert("zxc");
    var name = document.getElementById("Txtname");
    if (name.value == "")
        name.setCustomValidity("Bạn chưa nhập tên ");
    else
        name.setCustomValidity("");
}