
console.log("loading scripts");

function ShowDeleteComfirmationModal() {
    console.log("showing panel");
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteComfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}

window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, 'Operation Successful', {timeOut:5000});
    }
    if (type === "error") {
        toastr.success(message, 'Operation Failed', { timeOut: 5000 });
    }
}

