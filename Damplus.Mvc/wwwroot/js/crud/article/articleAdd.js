$(document).ready(function () {

    //Text Editor Starts From Here
    $('#text-editor').trumbowyg(
        {
            
            btns: [
                ['viewHTML'],
                ['undo', 'redo'], // Only supported in Blink browsers
                ['formatting'],
                ['strong', 'em', 'del'],
                ['superscript', 'subscript'],
                ['link'],
                ['insertImage'],
                ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
                ['unorderedList', 'orderedList'],
                ['horizontalRule'],
                ['removeformat'],
                ['fullscreen'],
                ['foreColor', 'backColor'],
                ['preformatted'],
                ['emoji'],
                ['fontfamily'],
                ['fontsize'],
                ['lineheight']

            ],
            plugins: {
                resizeimg: {
                    minSize: 64,
                    step: 16,
                }
            }
        }
    );
    //Text Editor Ends  Here
    $('#categoryList').select2({
        theme: 'bootstrap4',
        placeholder: "Kateqoriya Seçin",
        allowClear:true
    });
})