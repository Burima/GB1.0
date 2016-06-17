$('btn-show-add-driver').click(function () {
    $.ajax({
        url: AddDriverDetailUrl,
        type: 'POST',     
        dataType: 'JSON',
        
    });
});