function confirmDelete(uniqueId, isDeleteClicked,event) {
    event.preventDefault();
    var deleteSpan = 'deleteSpan' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();

    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}
