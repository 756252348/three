KindEditor.ready(function (K) {
    var ImgConfig = $("#ImgConfig"),
           PictureCount = ImgConfig.attr("data-pictureCount"),
           Notices = ImgConfig.attr("data-prompt"),
           FileName = ImgConfig.val();


    setTimeout(function () {
        var uploadbutton = K.uploadbutton({
            button: K('#uploadButton')[0],
            fieldName: 'imgFile',
            url: 'AJAX/uploadImg.ashx?filename=' + FileName,
            afterUpload: function (data) {
                if (data.error === 0) {
                    K('#iAlbum').append("<li><em></em><img src='../UploadFile/" + FileName + "/images/JD0_" + data.url + "' alt=\"加载失败\"/><input type='hidden' value='" + data.url + "' name='fuValue'/><s class='prev'>&lt;</s><s class='stick'>TOP</s><s class='next'>&gt;</s></li>");
                } else {
                    alert(data.message);
                }
            },
            afterError: function (str) {
                alert('自定义错误信息: ' + str);
            }
        });
        uploadbutton.fileBox.change(function () {
            if ($("#iAlbum").find("li").size() > parseInt(PictureCount)) {
                alert("图片数量超过限制！");
            } else {
                uploadbutton.submit();
            }
        });
    }, 500);
    setTimeout(function () {
        var uploadbutton = K.uploadbutton({
            button: K('#uploadButton1')[0],
            fieldName: 'imgFile',
            url: 'AJAX/uploadImg.ashx?filename=' + FileName,
            afterUpload: function (data) {
                if (data.error === 0) {
                    K('#iAlbum1').append("<li><em></em><img src='../UploadFile/" + FileName + "/images/JD0_" + data.url + "' alt=\"加载失败\"/><input type='hidden' value='" + data.url + "' name='fuValue1'/><s class='prev'>&lt;</s><s class='stick'>TOP</s><s class='next'>&gt;</s></li>");
                } else {
                    alert(data.message);
                }
            },
            afterError: function (str) {
                alert('自定义错误信息: ' + str);
            }
        });
        uploadbutton.fileBox.change(function () {
            if ($("#iAlbum1").find("li").size() > parseInt(PictureCount)) {
                alert("图片数量超过限制！");
            } else {
                uploadbutton.submit();
            }
        });
    }, 500);//
    //var editor = K.editor({
    //    fileManagerJson: 'AJAX/fileManagerJson.ashx'
    //});
    K('#filemanager').click(function () {

        editor.loadPlugin('filemanager', function () {
            editor.plugin.filemanagerDialog({
                viewType: 'VIEW',
                dirName: FileName,
                clickFn: function (url, title) {
                    url = url.substring(url.lastIndexOf('/') + 1);
                    if ($("#iAlbum").find("li").size() > parseInt(PictureCount)) {
                        alert("图片数量超过限制！");
                        return false;
                    }
                    else {
                        K('#iAlbum').append("<li><em></em><img src='../UploadFile/" + FileName + "/images/JD0_" + url + "' alt=\"加载失败\"/><input type='hidden' value='" + url + "' name='fuValue'/><s class='prev'>&lt;</s><s class='stick'>TOP</s><s class='next'>&gt;</s></li>");
                        editor.hideDialog();
                    }
                }
            });
        });
    });

    bindAlbum();//

    K('#filemanager').click(function () {

        editor.loadPlugin('filemanager', function () {
            editor.plugin.filemanagerDialog({
                viewType: 'VIEW',
                dirName: FileName,
                clickFn: function (url, title) {
                    url = url.substring(url.lastIndexOf('/') + 1);
                    if ($("#iAlbum1").find("li").size() > parseInt(PictureCount)) {
                        alert("图片数量超过限制！");
                        return false;
                    }
                    else {
                        K('#iAlbum1').append("<li><em></em><img src='../UploadFile/" + FileName + "/images/JD0_" + url + "' alt=\"加载失败\"/><input type='hidden' value='" + url + "' name='fuValue1'/><s class='prev'>&lt;</s><s class='stick'>TOP</s><s class='next'>&gt;</s></li>");
                        editor.hideDialog();
                    }
                }
            });
        });
    });

    bindAlbum1();


    $("#iAlbum").delegate("em", "click", function () {
        $(this).parent().remove();
    });
    $("#iAlbum1").delegate("em", "click", function () {
        $(this).parent().remove();
    });
    //
//    $("#iAlbum").delegate(".prev", "click", function () {
//        var a = $(this).parent("li").index();
//        if (a > 0) {
//            $(this).parent("li").prev().before("<li>" + $(this).parent().html() + "</li>");
//            $(this).parent().remove();
//        }
//    });

//    $("#iAlbum").delegate(".next", "click", function () {

//        var a = $(this).parent("li").index();
//        var b = $("#iAlbum li").size();
//        if (a != b - 1) {
//            $(this).parent("li").next().after("<li>" + $(this).parent().html() + "</li>");
//            $(this).parent().remove();
//        }
//    });

//    $("#iAlbum").delegate(".stick", "click", function () {
//        $("#iAlbum").prepend("<li>" + $(this).parent("li").html() + "</li>");
//        $(this).parent().remove();
//    }); 

//    $("#iAlbum").delegate("img", "click", function () {
//        var a = $("#ImgConfig").val(),
//            u = $(this).next().val();
//        if ($("div.yuantu").length > 0) {
//            $("body").find("div.yuantu").remove();
//        }

//        $("body").append("<div style='position:fixed;left:0px; top:0px; cursor:pointer; ' class='yuantu' title='点击图片关闭'><img alt='加载中...'  src='../UploadFile/" + a + "/original/" + u + "' style='width:35%'></div>");
//    });

//    $("body").delegate("div.yuantu", "click", function () {
//        $(this).remove();
//    });

//    K(".ke-form").width(76);

    //    $("#Notice").html(Notices);
    //
});

function bindAlbum() {
    var txtAlbum = $("#txtAlbum").val(),
        filename = $("#ImgConfig").val();
    if (txtAlbum && filename) {
        var arr = txtAlbum.split(','),
            html = [];
        for (var i = 0, len = arr.length ; i < len ; i++) {
            html.push("<li><em></em><img src=\"../UploadFile/" + filename + "/images/JD0_" + arr[i] + "\" alt=\"加载失败\"/><input type='hidden' value='" + arr[i] + "' name='fuValue'/><s class='prev'>&lt;</s><s class='stick'>TOP</s><s class='next'>&gt;</s></li>");
        }
        $("#iAlbum").append(html.join(''));
    }
}
function bindAlbum1() {
    var txtAlbum = $("#txtAlbum1").val(),
        filename = $("#ImgConfig").val();
    if (txtAlbum && filename) {
        var arr = txtAlbum.split(','),
            html = [];
        for (var i = 0, len = arr.length ; i < len ; i++) {
            html.push("<li><em></em><img src=\"../UploadFile/" + filename + "/images/JD0_" + arr[i] + "\" alt=\"加载失败\"/><input type='hidden' value='" + arr[i] + "' name='fuValue1'/><s class='prev'>&lt;</s><s class='stick'>TOP</s><s class='next'>&gt;</s></li>");
        }
        $("#iAlbum1").append(html.join(''));
    }
}