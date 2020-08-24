// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// var a = document.getElementById('DisplayImage')
// $(documrnt).ready(function () {
//     $('.myfile-input').on("change", function () {
//         var filename = $(this).val().split("\\").pop();
//         $(this).next('.myfile-input').html(filename);
//     });
// });
// var a = document.getElementById('file');
// a.onchange = function(event) {
//     var reader = new FileReader();
//     reader.readAsDataURL(event.srcElement.files[0]);

//     reader.onload = function() {
//         var fileContent = reader.result;
//         var b = document.getElementById('imagepath');
//         b.src = fileContent;
//         var cancel = document.getElementById('crossicon');
//         b.style.opacity = "1";
//         var i = 0;
//         cancel.addEventListener('click', function() {
//             a.value = "";
//             b.style.opacity = "0";

//         })
//     };
// }
window.onload = function() {
    var a = document.querySelectorAll(".delete");
    a.forEach(btn => {
        btn.addEventListener('click', function(e) {
            let result = window.confirm("Confirm Delete?");
            if (!result) {
                e.preventDefault();
            }
        });
    });
}