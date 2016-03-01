var oldFileInput;

function chooseFile() {
    $("#fileInput").click();
    $("#ErrorMessage").empty();
}

function previewFile() {
    var oldImgSrc = $("#Avatar").attr("src");
    if (validateFileUpload()) {
        var preview = document.querySelector('img'); //selects the query named img
        var file = document.querySelector('input[type=file]').files[0]; //sames as here
        var reader = new FileReader();
        reader.onloadend = function () {
            preview.src = reader.result;
        }

        if (file) {
            reader.readAsDataURL(file); //reads the data as a URL
        } else {
            preview.src = oldImgSrc;
        }
    }
    else {
        $("#Avatar").attr("src", oldImgSrc);
    }
}

function validateFileUpload() {
    var fuData = document.getElementById('fileInput');
    if (fuData.value) {
        oldFileInput = fuData.value;
        var FileUploadPath = fuData.value;

        //To check if user upload any file
        if (FileUploadPath == '') {
            return false;
        } else {
            var Extension = FileUploadPath.substring(
                    FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

            //The file uploaded is an image

            if (Extension == "gif" || Extension == "png" || Extension == "bmp"
                                || Extension == "jpeg" || Extension == "jpg") {

                // To Display
                if (fuData.files && fuData.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#blah').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(fuData.files[0]);
                }

            }
            else {
                return false;

            }
        }
    }
    return true;
}



function SaveForm() {
    var form = $('#myForm');
    var formData = new FormData(form[0]);

    $.ajax({
        url: '/Home/MyProfileEdit',
        type: 'Post',
        data: formData,
        contentType: false,
        processData: false,
        failor: function (data) {
        },
        success: function (data) {

            if (data.result == 'Redirect') {
                window.location = data.url;
            }
            else if (data.result == 'Error') {
                $("#ErrorMessage").html(data.message);
            }
            else {
                var div = document.getElementById("AjaxBody");
                var oldScrollTop = div.scrollTop;
                div.innerHTML = data;
                div.scrollTop = oldScrollTop;
                return false;
            }
        }
    });
}


function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
          .toString(16)
          .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4();
}

function takePicture() {
    document.getElementById('cameraBlock').style = "display:initial";
}

function takeFile() {
    document.getElementById('cameraBlock').style = "display:none";
    chooseFile();
}