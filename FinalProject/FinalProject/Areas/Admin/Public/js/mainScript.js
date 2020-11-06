$(document).ready(function () {
    if ($(".datepicker").length > 0) {
        $('.datepicker').datepicker({

        })
    }
    if ($("#ckEditor1").length > 0) {
        CKEDITOR.replace('ckEditor1');

    }

})