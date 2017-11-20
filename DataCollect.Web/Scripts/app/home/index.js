

$(function () {
    var btnPreview = $("#btnPreview");
    var input1 = document.querySelector("#fileUpload");
    var previewBook = $("#book-preview");
    var uploadUrl = "/Home/Preview";

    //方法
    function getColumn(cols) {
        var str = "";
        for (var i = 0; i < cols.length; i++) {
            var col = cols[i];
            str += "<li>"+col.Name+"</li>";
        }
        return str;
    }
     
    function previewClick() {
        let form1 = new FormData();
        form1.append("file", input1.files[0]);

        axios.post(uploadUrl, form1, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        }).then(function (response) {
            console.log(response.data);
            var data = response.data;
            var text = "<h5>工作簿 " + data.Name + "</h5>";
            for (var i = 0; i < data.Sheets.length; i++) {
                var sheet = data.Sheets[i];
                text += "<div class='sheet-item'>" +
                    "<h5>" + sheet.Name + "</h5>"
                    +"<ul>"+getColumn(sheet.Columns)+"</ul>"
                    + "</div>";
            }
            previewBook.html(text);
        });
    }

    //事件注册
    btnPreview.click(previewClick);
});
//doc:https://blueimp.github.io/jQuery-File-Upload/basic.html