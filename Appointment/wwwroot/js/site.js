// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#btnAddMeeting').click(function () {
    $('#MeetingModel').modal('show');
})
$('#close_btn').click(function () {
    $('#MeetingModel').modal('hide');
})
$('#close_btn1').click(function () {
    $('#MeetingModel').modal('hide');
})

$('#MeetingT').click(function () {
    $('#MeetingDelete').modal('show');
})

$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');  
    $('button[data-toggle="ajax-model"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })
    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendaData = form.serialize();
        $.post(actionUrl, sendaData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
            window.location.reload();
        })
    })
})
