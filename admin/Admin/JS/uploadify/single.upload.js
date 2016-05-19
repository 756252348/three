$(function () {
    var o_upload = $('#file_upload'),
        o_upload_id = $("#fieldID");
    o_upload.uploadify({
        swf: 'js/uploadify/uploadify.swf',
        uploader: 'AJAX/upload.ashx',
        queueID: 'queue',
        fileObjName: 'file_upload',
        fileTypeExts: '*.jpg;*.gif;*.jpeg;*.png',
        itemTemplate: false,
        auto: true,
        fileSizeLimit: '1024KB',
        queueSizeLimit: 1,
        fileTypeDesc: 'All Files',
        buttonText: '点击上传图片',
        method: 'post',
        cancelImage: 'images/cancel.png',
        width: 180,
        height: 180,
        multi: true,
        onUploadSuccess: function (file, data, response) {
            var json = eval("(" + data + ")");
            if (parseInt(json.fileId) < 1) {
                o_upload.uploadify('cancel', file.id);
            } else {
                o_upload_id.val(json.fileName).attr("data-upload-id", json.fileId);
                $("#file_upload-button").slideDown(1000).html("<img src='../album/" + o_upload_id.attr("data-file") + "/" + o_upload_id.attr("data-size") + "_" + o_upload_id.val() + "?r=" + Math.random() + "' />");
            }
        },
        onSelect: function (file) {
            $("#file_upload-button").slideUp();
            o_upload.uploadify('settings', 'formData', { 'fieldID': o_upload_id.attr("data-upload-id"), uType: $("#ModuleCode").attr("data-module"), uSpecial: o_upload_id.attr("data-file"), userId: cookie_user_id });
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

    if (o_upload_id.val().indexOf('.') > 0) {
        $("#file_upload-button").html("<img src='../album/" + o_upload_id.attr("data-file") + "/" + o_upload_id.attr("data-size") + "_" + o_upload_id.val() + "?r=" + Math.random() + "' />");
    }
})

