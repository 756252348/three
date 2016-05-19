$(function () {
    var o_upload = $('#file_upload,'),
        o_uploadAlbum = [],
        o_Album = $("#uploadAlbum");
    if (o_Album.val() != "0") o_uploadAlbum = o_Album.val().split(',');
    o_upload.uploadify({
        swf: 'js/uploadify/uploadify.swf',
        uploader: 'AJAX/upload.ashx',
        queueID: 'queue',
        fileObjName: 'file_upload',
        fileTypeExts: '*.jpg;*.gif;*.jpeg;*.png',
        itemTemplate: '<div id="${fileID}" class="uploadify-queue-item">'
                        + '<div class="uploadify-img"><img src="#" width="48" height="60" /></div>'
        				+ '<div class="uploadify-progress">'
        				+ '   <div class="uploadify-progress-bar"></div>'
        				+ '</div>'
                        + '<input class="upload-item" type="hidden" name="upload-item" value="0" data-file="0"  />'
                        + '<a class="cancelBtn" href="javascript:$(\'#${instanceID}\').uploadify(\'cancel\', \'${fileID}\')"><img src="images/cancel.png" /></a>'
        				+ '</div>',
        auto: true,
        removeCompleted: false,
        buttonImage: 'images/submit.png',
        fileSizeLimit: '1024KB',
        queueSizeLimit: 5,
        fileTypeDesc: 'All Files',
        method: 'post',
        cancelImage: 'images/cancel.png',
        width: 85,
        height: 27,
        multi: true,
        onSelect: function (file) {
            o_upload.uploadify('settings', 'formData', { 'fieldID': 0, uType: $("#ModuleCode").attr("data-module"), 'width': $("#ad-width").html(), 'height': $("#ad-height").html(), 'userId': cookie_user_id });
        },
        onCancel: function (file) {
            var o_upload_id = $("#" + file).find(":hidden").val(),
                o_upload_file = $("#" + file).find(":hidden").attr("data-file");
            
            if (parseInt(o_upload_id) > 0) {
                $.post("AJAX/dataRomve.ashx", { nid: o_upload_id, moduleID: 33 },
                    function (data) {
                        if (data == "1") {
                            o_uploadAlbum.remove(o_upload_file);
                            o_Album.val(o_uploadAlbum.join(',') || "0");
                        }
                    },
                "html");
            }
        },
        onUploadSuccess: function (file, data, response) {
            var json = eval("(" + data + ")");
            if (parseInt(json.fileId) < 1) {
                o_upload.uploadify('cancel', file.id);
            }
            else {
                o_uploadAlbum.push(json.fileName);
                o_Album.val(o_uploadAlbum.join(","));

                $("#" + file.id).find("input.upload-item").val(json.fileId).attr("data-file", json.fileName);
                $("#" + file.id).find(".uploadify-img").html("<img src='../Album/" + json.o_Img + "' width='48' height='60' />");

            }
            //alert(json.error);
        },
        onSelectError: function (file, errorCode, errorMsg) {
            var settings = this.settings;
            switch (errorCode) {
                case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
                    alert("一次最多上传" + settings.queueSizeLimit + "张照片！");
                    break;
                case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                    alert('抱歉 "' + file.name + '" 文件查过了大小限制 (' + settings.fileSizeLimit + ').');
                    break;
                case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                    alert('文件 "' + file.name + '" 为空.');
                    break;
                case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                    alert('文件 "' + file.name + '" 类型不支持 (' + settings.fileTypeDesc + ').');
                    break;
            }
        }
    });
})


Array.prototype.remove = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == v) {
            while (i < this.length) {
                if (i == this.length - 1) {
                    this.length -= 1;
                    break;
                }
                this[i] = this[i + 1]
                i++;
            }
            break;
        }
    }
};
