function uploadImage(fileId,imageId) {
    let input = document.getElementById(fileId);

    var fileToUpload = input.files.item(0)

    var filereader = new FileReader();
    filereader.onload = (event) => {
        document.getElementById(imageId).src = event.target.result;

    }
    filereader.readAsDataURL(fileToUpload)




  /*  if (input.files && input.files[0]) {

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                console.log(xhttp.responseText);
                document.getElementById('img').src = xhttp.responseText;
            }
        };
       
        xhttp.open("POST", "/Image/upload", true);
        var formData = new FormData(document.getElementById('imageForm'));
       
        xhttp.send(formData);
    }*/
}


